using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRing : MonoBehaviour
{
    [SerializeField] private int ID;
    [SerializeField] private RaceData raceData;

    public bool shouldGetRaceDataFrom {get; private set;} = false;

    private bool isActive = true;
    private bool playerIsInside = false;

    private void Start()
    {
        GameManager.Instance.OnSwitchGameMode.AddListener(OnSwitchGameMode);
        // OnSwitchGameMode();
    }

    public RaceData GetRaceData()
    {
        shouldGetRaceDataFrom = false;
        return raceData;
    }

    public void OnSwitchGameMode()
    {
        GameMode currentGameMode = GameMode.Instance;
        if(currentGameMode is FreeRoamMode)
        {
            isActive = true;
            FreeRoamMode freeRoamMode = currentGameMode as FreeRoamMode;
            freeRoamMode.TriggerRace.AddListener(OnTriggerRace);
        }
    }

    public void OnTriggerRace()
    {
        isActive = false;
        StopRenderingRacePreviewInfo();

        if(playerIsInside) 
        {
            playerIsInside = false;
            shouldGetRaceDataFrom = true;
            GameManager.Instance.SwitchToGameMode("Race");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && isActive == true)
        {
            playerIsInside = true;
            RenderRacePreviewInfo();       
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" && isActive == true)
        {
            playerIsInside = false;
            StopRenderingRacePreviewInfo();
        }
    }

    private void RenderRacePreviewInfo()
    {
        if(raceData is TimeTrialData)
            {
                UI_Blackboard.Instance.TryAdd<string>("RaceType", "Time Trial");
            }
            else if(raceData is ClassicData)
            {
                UI_Blackboard.Instance.TryAdd<string>("RaceType", "Classic Race");
            }

            UI_Blackboard.Instance.TryAdd<string>("FinishDestination", raceData.endingLoaction);

            UI_WidgetManager.Instance.TryLoadWidget("RaceInfo","RaceInfo");
    }

    private void StopRenderingRacePreviewInfo()
    {
        Debug.Log("Stop Rendering Race Preview");
        UI_WidgetManager.Instance.TryUnloadWidget("RaceInfo");
    }
}
