using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Timeline;

public class GameMode : GenericSingleton<GameMode>
{
    public UnityEvent OnSetupEnd;
    public UnityEvent OnTearDownEnd;

    public SignalAsset OnEnableControlsSignal;
    public SignalAsset OnDisableControlsSignal;

    public GameObject playerCharacterPrefab;
    public GameObject playerControllerPrefab;
    public GameObject playerCameraPrefab;

    protected List<NamedPlayerObject> characters;
    
    public void OnDestroy()
    {
        Debug.Log("GameMode is being destroyed");
    }

    public override void Awake()
    {
        base.Awake();
        characters = new List<NamedPlayerObject>();
    }

    private void Start()
    {
        // Setup();
    }

    public virtual void Setup()
    {
        SignalReceiver signalReceiver = GameObject.FindObjectOfType<SignalReceiver>();
        signalReceiver.GetReaction(OnEnableControlsSignal).AddListener(EnableAllControls);
        signalReceiver.GetReaction(OnDisableControlsSignal).AddListener(DisableAllControls);
        
        OnSetupEnd.Invoke();
    }

    public virtual void TearDown()
    {
        SignalReceiver signalReceiver = GameObject.FindObjectOfType<SignalReceiver>();
        signalReceiver.GetReaction(OnEnableControlsSignal).RemoveListener(EnableAllControls);
        signalReceiver.GetReaction(OnDisableControlsSignal).RemoveListener(DisableAllControls);

        OnTearDownEnd.Invoke();
    }

    protected virtual void EnableAllControls()
    {
        Debug.Log("Enable All Controls");
        
        foreach (NamedPlayerObject _character in characters)
        {
            Debug.Log("Enable All Controls | characters count: " + characters.Count);
            _character.controller.GetComponent<GameController>().SetEnabled(true);
        }
    }

    protected virtual void DisableAllControls()
    {
        Debug.Log("Disabble All Controls");
        
        foreach (NamedPlayerObject _character in characters)
        {
            Debug.Log("Disable All Controls | characters count: " + characters.Count);
            _character.controller.GetComponent<GameController>().SetEnabled(false);
        }
    }

    protected virtual void SpawnPlayerAt()
    {
        SpawnPlayerAt(Vector3.zero, Quaternion.identity);
    }

    protected virtual void SpawnPlayerAt(Vector3 location)
    {
        SpawnPlayerAt(location, Quaternion.identity);
    }

    protected virtual void SpawnPlayerAt(Vector3 location, Quaternion rotation)
    {
        // Spawns character object
        GameObject newCharacter = Instantiate(playerCharacterPrefab, location, rotation);
        GameObject newCamera = Instantiate(playerCameraPrefab, location, rotation);
        GameObject newController = Instantiate(playerControllerPrefab, location, rotation);

        // Setup Player
        // Setup the Cameras
        CarCamerasFacade carCamerasFacade = newCamera.GetComponent<CarCamerasFacade>();
        carCamerasFacade.target = newCharacter;
        carCamerasFacade.splitScreenMode = SplitScreenMode.OnePlayer;
        carCamerasFacade.playerNumber = 1;
        carCamerasFacade.Refresh();

        // Setup the controler
        newController.GetComponent<GameController>().TryToPossesCharacter(newCharacter.GetComponent<Character>());

        // newCharacter.GetComponent<CharacterCameraFacade>().Intialize(SplitScreenMode.OnePlayer, 1);


        // Adds spawned character to list
        NamedPlayerObject namedPlayerObject = new NamedPlayerObject(characters.Count.ToString(), newCharacter, newCamera, newController);
        characters.Add(namedPlayerObject);
    }

    protected virtual void DespawnAllCharacters()
    {
        foreach (NamedPlayerObject _character in characters)
        {
            // Destroy(_character.obj);
            DestroyNamedPlayerObject(_character);
        }

        characters.Clear();
    }

    protected virtual void DespawnCharacter(string name)
    {
        NamedPlayerObject characterToDestroy = Utils.FindNamedPlayerObjectAndRemove(name, ref characters);
        // Destroy(characterToDestroy.obj);
        DestroyNamedPlayerObject(characterToDestroy);
    }

    private void DestroyNamedPlayerObject(NamedPlayerObject namedPlayerObject)
    {
        Destroy(namedPlayerObject.vehical);
        Destroy(namedPlayerObject.controller);
        Destroy(namedPlayerObject.camera);
    }
}
