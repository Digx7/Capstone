using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeRoamMode : GameMode
{
    public Vector3 playerStartLocation;
    public GameObject playerPrefab;
    
    public override void Setup()
    {
        //TODO: get playerStartLocation from savedata
        SpawnCharacterAt(playerPrefab, playerStartLocation);

        base.Setup();
    }
    
    public override void TearDown()
    {
        DespawnAllCharacters();
        
        base.TearDown();
    }
}
