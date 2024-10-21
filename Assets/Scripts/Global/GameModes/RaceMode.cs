using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering.Universal;


public class RaceMode : GameMode
{
    public RaceData raceData;
    private Vector3 playerStartLocation;
    public GameObject playerPrefab;
    public SignalAsset OnRaceStartSignal;
    public GameObject baseCamera;

    // Timetrial variables
    private bool stopWatchIsGoing = false;
    private float currentTime = 0.0f;
    private float playersFinalTime = 0.0f;
    
    private List<GameObject> tempCharacters;

    private GameObject introBaseCameraObject;
    private GameObject introFacadeObject;

    public override void Setup()
    {
        // Read Race data and load relevant info
        raceData = RetrieveRaceData();
        playerStartLocation = raceData.startingLocation;

        // Spawn Character
        tempCharacters = new List<GameObject>();
        tempCharacters.Add(SpawnCharacterOnlyAt(playerStartLocation));

        // Spawn base camera
        introBaseCameraObject = Instantiate(baseCamera, playerStartLocation, Quaternion.identity);
        Camera camera = introBaseCameraObject.GetComponentInChildren<Camera>();

        base.Setup();

        // Load race intro
        introFacadeObject = Instantiate(raceData.intro);
        RaceIntro_Facade raceIntro_Facade = introFacadeObject.GetComponent<RaceIntro_Facade>();

        // Setup overlay camera
        Camera overlayCamera = raceIntro_Facade.IntroCamera;
        var cameraData = camera.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(overlayCamera);

        // Setup signal reciever
        SignalReceiver signalReciever = raceIntro_Facade.MainSignalReciever;
        signalReciever.GetReaction(OnRaceStartSignal).AddListener(StartRace);

        // Play race intro
        raceIntro_Facade.PlayIntro();
    }

    public override void TearDown()
    {
        
        // Saves the players current position
        Vector3 playerPos = characters[0].vehical.transform.position;
        SaveManager.Instance.loadedSave.TryAdd<Vector3>("PlayerPosition", playerPos);
        SaveManager.Instance.Save();

        
        DespawnAllCharacters();

        base.TearDown();
    }

    public void StartRace()
    {
        foreach (GameObject character in tempCharacters)
        {
            GameObject controller = SpawnControllerOnlyAt(playerStartLocation);
            GameObject cameras = SpawnCameraOnlyAt(playerStartLocation);

            SetupController(controller, character);
            SetupCamera(cameras, character);

            RegisterCharacter(character, cameras, controller);
        }

        Destroy(introBaseCameraObject);
        Destroy(introFacadeObject);

        tempCharacters.Clear();
        
        if(raceData is TimeTrialData)
        {
            StartTimeTrial();
        } 
        else if(raceData is ClassicData)
        {
            StartClassicRace();
        }
    }

    private void StartTimeTrial()
    {
        TimeTrialData timeTrialDate = raceData as TimeTrialData;
        
        // Update UI
        UI_Blackboard.Instance.TryAdd<float>("CurrentTime", currentTime);
        UI_Blackboard.Instance.TryAdd<float>("GoldTime", timeTrialDate.goldMedalTime);
        UI_Blackboard.Instance.TryAdd<float>("SilverTime", timeTrialDate.silverMedalTime);
        UI_Blackboard.Instance.TryAdd<float>("BronzeTime", timeTrialDate.bronzeMedalTime);

        UI_WidgetManager.Instance.TryLoadWidget("TimeTrial","TimeTrial");
        UI_WidgetManager.Instance.TryLoadWidget("MiniMap","MiniMap");

        // Start Clock
        Debug.Log("Starting Stopwatch");
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        if(stopWatchIsGoing) yield break;

        stopWatchIsGoing = true;

        while (stopWatchIsGoing)
        {
            currentTime += Time.deltaTime;
            // Update UI
            UI_Blackboard.Instance.SetValue<float>("CurrentTime", currentTime);
            Debug.Log("Stopwatch: " + currentTime);
            yield return null;
        }

        currentTime = 0.0f;
        stopWatchIsGoing = false;
        Debug.Log("Stoping Stopwatch");
    }

    private void StartClassicRace()
    {

    }

    private void EndTimeTrial()
    {
        TimeTrialData timeTrialDate = raceData as TimeTrialData;
        
        // Stop Clock
        stopWatchIsGoing = false;
        playersFinalTime = currentTime;
        UI_WidgetManager.Instance.TryUnloadWidget("TimeTrial");
        UI_WidgetManager.Instance.TryUnloadWidget("MiniMap");

        int finalPosition;

        if(playersFinalTime <= timeTrialDate.goldMedalTime) finalPosition = 1;
        else if(playersFinalTime <= timeTrialDate.silverMedalTime) finalPosition = 2;
        else if(playersFinalTime <= timeTrialDate.bronzeMedalTime) finalPosition = 3;
        else finalPosition = 4;

        UI_Blackboard.Instance.TryAdd<int>("RaceResult", finalPosition);
        UI_WidgetManager.Instance.TryLoadWidget("EndRace","EndRace");
    }

    private void EndClassicRace()
    {

    }

    public void EndRace()
    {
        if(raceData is TimeTrialData)
        {
            EndTimeTrial();
        } 
        else if(raceData is ClassicData)
        {
            EndClassicRace();
        }
        
        // TODO: Play Race Outro
        DisableAllControls();
        // TODO: Show UI
        // TODO: Show Correct Camera
    }

    // public void ExitRace()
    // {
    //     // Switch back to FreeRoam
    //     GameManager.Instance.SwitchToGameMode("FreeRoam");
    // }

    private RaceData RetrieveRaceData()
    {
        StartRing[] allStartRings = FindObjectsOfType<StartRing>();

        foreach (StartRing _startRing in allStartRings)
        {
            if(_startRing.shouldGetRaceDataFrom)
            {
                return _startRing.GetRaceData();
            }
        }

        Debug.LogError("RaceMode failed to find any valid Race Data");

        return null;
    }
}
