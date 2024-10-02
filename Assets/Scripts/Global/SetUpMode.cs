using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpMode : GameMode
{
    public Vector3 playerStartLocation;
    public GameObject playerPrefab;
    
    public override void Setup()
    {
        StartCoroutine(Load());
        
        base.Setup();
    }

    public override void TearDown()
    {
        
        
        base.TearDown();
    }

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(0.1f);
        SpawnPlayerAt(playerStartLocation);
        GameManager.Instance.SwitchToGameMode("FreeRoam");
    }
}
