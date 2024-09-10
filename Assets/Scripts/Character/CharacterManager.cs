using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterManager : MonoBehaviour
{
    public List<CharacterCameraFacade> characters;
    // public GameObject playerPrefab;
    // private int numberOfCurrentPlayers = 1;

    public void Start()
    {
        // OnPlayerJoin();
    }

    public void OnPlayerJoin(PlayerInput playerInput)
    {
        Debug.Log("A player joined");
        // numberOfCurrentPlayers++;
        
        CharacterCameraFacade newFacade = playerInput.gameObject.GetComponentInParent<CharacterCameraFacade>();
        
        newFacade.Intialize(GetSplitScreenMode(characters.Count + 1), characters.Count + 1);

        characters.Add(newFacade);

        Refresh(characters.Count);
    }

    public void OnPlayerLeave(PlayerInput playerInput)
    {    
        Debug.Log("A player left");
        // numberOfCurrentPlayers--;
        Destroy(characters[characters.Count - 1].gameObject);

        Refresh(characters.Count);
    }

    private void Refresh(int numberOfPlayers)
    {
        foreach (CharacterCameraFacade item in characters)
        {
            item.Refresh(GetSplitScreenMode(numberOfPlayers));
            Debug.Log("Refreshesd with " + numberOfPlayers + " and set splitscreen mode to " + GetSplitScreenMode(numberOfPlayers));
        }
    }

    private SplitScreenMode GetSplitScreenMode(int numberOfPlayers)
    {
        SplitScreenMode splitScreenMode = SplitScreenMode.OnePlayer;
        if(numberOfPlayers == 2) splitScreenMode = SplitScreenMode.TwoPlayer;
        if(numberOfPlayers == 3) splitScreenMode = SplitScreenMode.ThreePlayer;
        if(numberOfPlayers == 4) splitScreenMode = SplitScreenMode.FourPlayer;
        return splitScreenMode;
    }
}

// A comment
