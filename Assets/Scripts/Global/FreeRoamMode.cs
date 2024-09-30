using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FreeRoamMode : GameMode
{
    public Vector3 playerStartLocation;
    public GameObject playerPrefab;

    public UnityEvent TriggerRace;
    
    public override void Setup()
    {
        Vector3 pos = SaveManager.Instance.loadedSave.TryGetValue<Vector3>("PlayerPosition");
        
        SpawnCharacterAt(playerPrefab, pos);

        base.Setup();
    }
    
    public override void TearDown()
    {
        DespawnAllCharacters();
        
        base.TearDown();
    }

    public void TryToStartRace()
    {
        TriggerRace.Invoke();
    }
}
