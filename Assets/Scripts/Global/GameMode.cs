using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMode : GenericSingleton<GameMode>
{
    public UnityEvent OnSetupEnd;
    public UnityEvent OnTearDownEnd;

    private List<NamedGameObject> characters;
    
    public void OnDestroy()
    {
        Debug.Log("GameMode is being destroyed");
    }

    public override void Awake()
    {
        base.Awake();
        characters = new List<NamedGameObject>();
    }

    private void Start()
    {
        // Setup();
    }

    public virtual void Setup()
    {
        OnSetupEnd.Invoke();
    }

    public virtual void TearDown()
    {
        OnTearDownEnd.Invoke();
    }

    protected virtual void EnableAllControls()
    {
        // enable all controllers
    }

    protected virtual void DisableAllContorls()
    {
        // disable all controllers
    }

    protected virtual void SpawnCharacterAt(GameObject prefab)
    {
        SpawnCharacterAt(prefab, Vector3.zero, Quaternion.identity);
    }

    protected virtual void SpawnCharacterAt(GameObject prefab, Vector3 location)
    {
        SpawnCharacterAt(prefab, location, Quaternion.identity);
    }

    protected virtual void SpawnCharacterAt(GameObject prefab, Vector3 location, Quaternion rotation)
    {
        // Spawns character object
        GameObject newCharacter = Instantiate(prefab, location, rotation);

        newCharacter.GetComponent<CharacterCameraFacade>().Intialize(SplitScreenMode.OnePlayer, 1);


        // Adds spawned character to list
        NamedGameObject namedCharacter = new NamedGameObject(characters.Count.ToString(), newCharacter);
        characters.Add(namedCharacter);
    }

    protected virtual void DespawnAllCharacters()
    {
        foreach (NamedGameObject _character in characters)
        {
            Destroy(_character.obj);
        }

        characters.Clear();
    }

    protected virtual void DespawnCharacter(string name)
    {
        NamedGameObject characterToDestroy = Utils.FindNamedGameObjectAndRemove(name, ref characters);
        Destroy(characterToDestroy.obj);
    }
}
