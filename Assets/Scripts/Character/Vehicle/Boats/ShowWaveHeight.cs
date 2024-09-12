using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWaveHeight : MonoBehaviour
{
    public WaveManager waveManager;

    public void Update()
    {
        Vector3 pos = transform.position;
        pos.y = waveManager.GetWaveHeightAtLocation(pos);
        transform.position = pos;
    }
}
