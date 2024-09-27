using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStructs : MonoBehaviour
{
}

[System.Serializable]
public struct NamedGameObject
{
    public string name;
    public GameObject obj;

    public NamedGameObject(string _name = "", GameObject _obj = null)
    {
        name = _name;
        obj = _obj;
    }
}

[System.Serializable]
public struct Wave
{
    public float direction_x;
    public float direction_y;
    public float steepness;
    public float wavelength;
}
