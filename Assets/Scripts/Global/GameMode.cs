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
        // // Spawns character object
        // GameObject newCharacter = Instantiate(playerCharacterPrefab, location, rotation);
        // GameObject newCamera = Instantiate(playerCameraPrefab, location, rotation);
        // GameObject newController = Instantiate(playerControllerPrefab, location, rotation);

        // // Setup Player
        // // Setup the Cameras
        // CarCamerasFacade carCamerasFacade = newCamera.GetComponent<CarCamerasFacade>();
        // carCamerasFacade.target = newCharacter;
        // carCamerasFacade.splitScreenMode = SplitScreenMode.OnePlayer;
        // carCamerasFacade.playerNumber = 1;
        // carCamerasFacade.Refresh();

        // // Setup the controler
        // newController.GetComponent<GameController>().TryToPossesCharacter(newCharacter.GetComponent<Character>());


        // // Adds spawned character to list
        // NamedPlayerObject namedPlayerObject = new NamedPlayerObject(characters.Count.ToString(), newCharacter, newCamera, newController);
        // characters.Add(namedPlayerObject);
        
        GameObject newCharacter = SpawnCharacterOnlyAt(location, rotation);
        GameObject newCamera = SpawnCameraOnlyAt(location, rotation);
        GameObject newController = SpawnControllerOnlyAt(location, rotation);

        SetupCamera(newCamera, newCharacter);

        SetupController(newController, newCharacter);

        RegisterCharacter(newCharacter, newCamera, newController);
    }

    protected virtual GameObject SpawnCharacterOnlyAt()
    {
        return SpawnCharacterOnlyAt(Vector3.zero, Quaternion.identity);
    }
    
    protected virtual GameObject SpawnCharacterOnlyAt(Vector3 location)
    {
        return SpawnCharacterOnlyAt(location, Quaternion.identity);
    }
    
    protected virtual GameObject SpawnCharacterOnlyAt(Vector3 location, Quaternion rotation)
    {
        // Spawns character object
        GameObject newCharacter = Instantiate(playerCharacterPrefab, location, rotation);
        return newCharacter;
    }

    protected virtual GameObject SpawnControllerOnlyAt()
    {
        return SpawnControllerOnlyAt(Vector3.zero, Quaternion.identity);
    }

    protected virtual GameObject SpawnControllerOnlyAt(Vector3 location)
    {
        return SpawnControllerOnlyAt(location, Quaternion.identity);
    }

    protected virtual GameObject SpawnControllerOnlyAt(Vector3 location, Quaternion rotation)
    {
        GameObject newController = Instantiate(playerControllerPrefab, location, rotation);
        return newController;
    }

    protected virtual void SetupController(GameObject controller, GameObject character)
    {
        controller.GetComponent<GameController>().TryToPossesCharacter(character.GetComponent<Character>());
    }

    protected virtual GameObject SpawnCameraOnlyAt()
    {
        return SpawnCameraOnlyAt(Vector3.zero, Quaternion.identity);
    }
    
    protected virtual GameObject SpawnCameraOnlyAt(Vector3 location)
    {
        return SpawnCameraOnlyAt(location, Quaternion.identity);
    }
    
    protected virtual GameObject SpawnCameraOnlyAt(Vector3 location, Quaternion rotation)
    {
        GameObject newCamera = Instantiate(playerCameraPrefab, location, rotation);
        return newCamera;
    }

    protected virtual void SetupCamera(GameObject camera, GameObject character)
    {
        CarCamerasFacade carCamerasFacade = camera.GetComponent<CarCamerasFacade>();
        carCamerasFacade.target = character;
        carCamerasFacade.splitScreenMode = SplitScreenMode.OnePlayer;
        carCamerasFacade.playerNumber = 1;
        carCamerasFacade.Refresh();
    }

    protected virtual void RegisterCharacter(GameObject character, GameObject camera, GameObject controller)
    {
        NamedPlayerObject namedPlayerObject = new NamedPlayerObject(characters.Count.ToString(), character, camera, controller);
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
