using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RaceMode : GameMode
{
    public RaceData raceData;
    public Vector3 playerStartLocation;
    public GameObject playerPrefab;
    
    public override void Setup()
    {
        // Read Race data and load relevant info
        raceData = RetrieveRaceData();

        SpawnCharacterAt(playerPrefab, playerStartLocation);

        // Play Cutscene
        PlayableDirector playableDirector = GameObject.FindObjectOfType<PlayableDirector>();
        playableDirector.Play();

        // Start Race


        base.Setup();
    }

    public override void TearDown()
    {
        DespawnAllCharacters();

        base.TearDown();
    }

    public void EndRace()
    {
        // Play Race Outro
        // Switch back to FreeRoam
        
        GameManager.Instance.SwitchToGameMode("FreeRoam");
    }

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
