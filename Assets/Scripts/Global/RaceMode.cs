using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class RaceMode : GameMode
{
    public RaceData raceData;
    public Vector3 playerStartLocation;
    public GameObject playerPrefab;
    public SignalAsset OnRaceStartSignal;

    // Timetrial variables
    private bool stopWatchIsGoing = false;
    private float currentTime = 0.0f;
    private float playersFinalTime = 0.0f;
    
    public override void Setup()
    {
        // Read Race data and load relevant info
        raceData = RetrieveRaceData();

        // Add hooks into Timeline
        SignalReceiver signalReceiver = GameObject.FindObjectOfType<SignalReceiver>();
        signalReceiver.GetReaction(OnRaceStartSignal).AddListener(StartRace);

        // TODO: Somehow get playerStartLocation from raceData
        SpawnCharacterAt(playerPrefab, playerStartLocation);

        // Play Cutscene
        PlayableDirector playableDirector = GameObject.FindObjectOfType<PlayableDirector>();
        playableDirector.Play();

        // Start Race


        base.Setup();
    }

    public override void TearDown()
    {
        // TODO: Save current location
        
        // DespawnAllCharacters();

        base.TearDown();
    }

    public void StartRace()
    {
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
            if(_startRing.isRaised)
            {
                return _startRing.GetRaceData();
            }
        }

        Debug.LogError("RaceMode failed to find any valid Race Data");

        return null;
    }
}
