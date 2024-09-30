using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_WidgetManager : GenericSingleton<UI_WidgetManager>
{
    public Canvas canvas;
    public List<WidgetPrefab> allWidgetPrefabs;
    
    private Dictionary<string, GameObject> AllWidgets_Dict;
    private Dictionary<string, GameObject> allLoadedWidgets_Dict;

    public void Awake()
    {
        AllWidgets_Dict = new Dictionary<string, GameObject>();
        allLoadedWidgets_Dict = new Dictionary<string, GameObject>();
        
        foreach (WidgetPrefab item in allWidgetPrefabs)
        {
            AllWidgets_Dict.TryAdd(item.key, item.prefab);
        }
    }


    public bool TryLoadWidget(string keyToLoadFrom, string keyToLoadTo)
    {
        if(!AllWidgets_Dict.ContainsKey(keyToLoadFrom))
        {
            Debug.LogError("UI_WidgetManager tried to load from a key (" + keyToLoadFrom + ") that does not exist.  Double check the spelling of all keys involved");
            return false;
        }

        GameObject prefab = AllWidgets_Dict[keyToLoadFrom];

        GameObject loaded = Instantiate(prefab);
        loaded.GetComponent<Widget>().SetID(keyToLoadTo);

        if(!allLoadedWidgets_Dict.TryAdd(keyToLoadTo, loaded))
        {
            Debug.LogError("UI_WidgetManager tried to load a new widget with a key (" + keyToLoadTo + ") that already exists.  Please use a different key instead.");
            Destroy(loaded);
            return false;
        }

        loaded.transform.SetParent(canvas.transform, false);


        return true;
    }

    public bool TryUnloadWidget(string keyToUnload)
    {
        if(!allLoadedWidgets_Dict.ContainsKey(keyToUnload))
        {
            Debug.LogError("UI_WidgetManager tried to unload from a key (" + keyToUnload + ") that does not exist.  Double check the spelling of all keys involved");
            return false;
        }

        GameObject loaded = allLoadedWidgets_Dict[keyToUnload];

        Destroy(loaded);

        allLoadedWidgets_Dict.Remove(keyToUnload);

        return true;
    }
}

[System.Serializable]
public class WidgetPrefab
{
    public string key;
    public GameObject prefab;
}
