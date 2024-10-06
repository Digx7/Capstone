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
        UI_WidgetManager.Instance.TryLoadWidget("MiniMap","MiniMap");
        
        SpawnPlayerAt(pos);

        base.Setup();
    }
    
    public override void TearDown()
    {
        DespawnAllCharacters();

        UI_WidgetManager.Instance.TryUnloadWidget("MiniMap");
        
        base.TearDown();
    }

    public void TryToStartRace()
    {
        TriggerRace.Invoke();
    }
}
