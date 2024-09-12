using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStructs : MonoBehaviour
{
}

[System.Serializable]
public struct Wave
{
    public float direction_x;
    public float direction_y;
    public float steepness;
    public float wavelength;
}
