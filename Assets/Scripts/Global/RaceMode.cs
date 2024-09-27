using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Start Race


        base.Setup();
    }

    public override void TearDown()
    {
        DespawnAllCharacters();

        base.TearDown();
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
