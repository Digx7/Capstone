using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaveManager : GenericSingleton<WaveManager>
{
    public Wave waveAdata;
    public Wave waveBdata;
    public Wave waveCdata;

    private float time = 0f;
    
    private float PI = 3.14159f;

    private void Awake()
    {
        SetShaderGlobalVariables();
    }

    private void Update()
    {
        UpdateShaderTime();
    }

    private void SetShaderGlobalVariables()
    {
        //Set globla variables in shader

        Vector4 waveA = new Vector4(waveAdata.direction_x, waveAdata.direction_y, waveAdata.steepness, waveAdata.wavelength);
        Vector4 waveB = new Vector4(waveBdata.direction_x, waveBdata.direction_y, waveBdata.steepness, waveBdata.wavelength);
        Vector4 waveC = new Vector4(waveCdata.direction_x, waveCdata.direction_y, waveCdata.steepness, waveCdata.wavelength);

        Shader.SetGlobalVector("_WaveA", waveA);
        Shader.SetGlobalVector("_WaveB", waveB);
        Shader.SetGlobalVector("_WaveC", waveC);
        Shader.SetGlobalFloat("_TimeStamp", time);
    }

    private void UpdateShaderTime()
    {
        //update the global variable time in the shader
        //this is needed to keep the wave calulations for height made by this script 
        //and the rendering in synce 

        // time += Time.deltaTime;
        time = Time.time;
        Shader.SetGlobalFloat("_TimeStamp", time);
    }

    private Vector3 CalculateGerstnerWavePointGivenLocation(Wave waveData, Vector3 location)
    {
        float steepness = waveData.steepness;
        float wavelength = waveData.wavelength;
        float k = 2 * PI / wavelength;
        float c = Mathf.Sqrt(9.8f / k);
        Vector2 d = new Vector2();
        d.x = waveData.direction_x;
        d.y = waveData.direction_y;
        d.Normalize();
        float f = k * (Vector2.Dot(d, new Vector2(location.x, location.z)) - c * time);
        float a = steepness / k;

        return new Vector3(
            d.x * (a * Mathf.Cos(f)),
            a * Mathf.Sin(f),
            d.y * (a * Mathf.Cos(f))
        );
    }


    public float GetWaveHeightAtLocation(Vector3 location)
    {
        float height = 0;
        height += CalculateGerstnerWavePointGivenLocation(waveAdata, location).y;
        height += CalculateGerstnerWavePointGivenLocation(waveBdata, location).y;
        height += CalculateGerstnerWavePointGivenLocation(waveCdata, location).y;
        return height;
    }
}
