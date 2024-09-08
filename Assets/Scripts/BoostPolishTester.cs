using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;

public class BoostPolishTester : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public Volume boostVolume;


    public float animDuration;
    public float animActiveTimeStamp;
    public float animCooldownTimeStamp;
    public float fovStartingValue;
    public float fovActiveValue;
    public float weightStartingValue;
    public float weightActiveValue;
    public AnimationCurve easeIn;
    public AnimationCurve easeOut;
    private float currentTime = 0.0f;
    private bool isRunning = false;

    public void Boost()
    {
        StartCoroutine(BoostAnimation());
    }

    private IEnumerator BoostAnimation()
    {
        // Setup
        if(isRunning) yield break;
        isRunning = true;

        // Play animation
        while (isRunning)
        {
            currentTime += Time.deltaTime;

            float fov = fovStartingValue;
            float weight = weightStartingValue;

            float fovDif = fovActiveValue - fovStartingValue;
            float weightDif = weightActiveValue - weightStartingValue;

            if(currentTime < animActiveTimeStamp)
            {
                // Startup
                fov = fovStartingValue + (fovDif * easeIn.Evaluate(Mathf.InverseLerp(0f, animActiveTimeStamp, currentTime)));
                weight = weightStartingValue + (weightDif * easeIn.Evaluate(Mathf.InverseLerp(0f, animActiveTimeStamp, currentTime)));


            }
            else if(currentTime < animCooldownTimeStamp)
            {
                // Active
                fov = fovActiveValue;
                weight = weightActiveValue;
            }
            else if(currentTime < animDuration)
            {
                // Cooldown
                fov = fovStartingValue + (fovDif * easeOut.Evaluate(Mathf.InverseLerp(animCooldownTimeStamp, animDuration, currentTime)));
                weight = weightStartingValue + (weightDif * easeOut.Evaluate(Mathf.InverseLerp(animCooldownTimeStamp, animDuration, currentTime)));
            }

            cinemachineVirtualCamera.m_Lens.FieldOfView = fov;
            boostVolume.weight = weight;

            if(currentTime < animDuration)
            {
                // repeat
                yield return null; 
            }
            else 
            {
                // End
                isRunning = false;
            }
        }

        // Teardown
        currentTime = 0f;

        
    }
}
