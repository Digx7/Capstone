using System;
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

    [SerializeField] private List<double> WaveRatios;
    [SerializeField] private double largestWaveLength;
    private double largestRatio = Mathf.NegativeInfinity;
    private double smallestRatio = Mathf.Infinity;
    private List<double> WavePreQValues;
    private List<double> WaveLengths;
    private int largestWaveIndex = 0;

    private float time = 0f;
    private double loopTime = 0;

    private void Awake()
    {    
        SetLoopTime();
        SetShaderGlobalVariables();
    }

    private void Update()
    {
        UpdateShaderTime();
    }

    private void SetLoopTime()
    {
        WavePreQValues = new List<double>();
        WaveLengths = new List<double>();
        
        // Get largest Wave and smallest Wave (largest Ratio)
        for (int i = 0; i < WaveRatios.Count; i++)
        {
            if(WaveRatios[i] > largestRatio) largestRatio = WaveRatios[i];
            if(WaveRatios[i] < smallestRatio) 
            {
                smallestRatio = WaveRatios[i];
                largestWaveIndex = i;
            }
        }

        // Compute WavePreQValues and WaveLengths
        double largestPreQ = Math.Sqrt(largestWaveLength);
        for (int i = 0; i < WaveRatios.Count; i++)
        {
            WavePreQValues.Add(largestPreQ / WaveRatios[i]);
            WaveLengths.Add(Math.Pow(WavePreQValues[i], 2f));
        }

        // Compute loop time
        loopTime = WavePreQValues[largestWaveIndex] / (Math.Sqrt( 9.8f / (2f * Mathf.PI) ));

        // Set wave and shader variables
        waveAdata.wavelength = (float) WaveLengths[0];
        waveBdata.wavelength = (float) WaveLengths[1];
        waveCdata.wavelength = (float) WaveLengths[2];
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
    }

    private void UpdateShaderTime()
    {
        //update the global variable time in the shader
        //this is needed to keep the wave calulations for height made by this script 
        //and the rendering in synce 

        time += Time.deltaTime;
        if(time >= loopTime) time = 0;

        Shader.SetGlobalFloat("_CustomTIme", time);
    }

    private Vector3 CalculateGerstnerWavePointGivenLocation(Wave waveData, Vector3 location)
    {
        float steepness = waveData.steepness;
        float wavelength = waveData.wavelength;
        float k = 2 * Mathf.PI / wavelength;
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
