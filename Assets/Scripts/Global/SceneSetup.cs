using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    [SerializeField] private string gameModeToLoadOnStart = "MainMenu";
    
    void Start()
    {
        Debug.Log("Scene Setup");

        
        GameManager.Instance.SwitchToGameMode(gameModeToLoadOnStart);
        Destroy(this);
    }
}
