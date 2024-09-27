using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class CarCamerasFacade : MonoBehaviour
{
    public GameObject target;
    public SplitScreenMode splitScreenMode;
    public int playerNumber;

    [SerializeField] private Camera camera;
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private List<CinemachineCamera> cinemachineCameras;
    [SerializeField] private CinemachineStateDrivenCamera cinemachineStateDrivenCamera;

    public void Start()
    {
        Refresh();
    }
    
    public void Refresh()
    {
        // Set target
        SetTarget();

        // Set cinemachine channels
        SetCinemachineChannels();

        // Set camera splitscreen
        SetCameraSplitScreen();
    }

    private void SetTarget()
    {
        cinemachineStateDrivenCamera.Follow = target.transform;
        foreach (CinemachineCamera cinemachineCamera in cinemachineCameras)
        {
            cinemachineCamera.Follow = target.transform;
        }
    }

    private void SetCinemachineChannels()
    {
        OutputChannels outputChannels = new OutputChannels();
        if(playerNumber == 1) outputChannels = OutputChannels.Channel01;
        if(playerNumber == 2) outputChannels = OutputChannels.Channel02;
        if(playerNumber == 3) outputChannels = OutputChannels.Channel03;
        if(playerNumber == 4) outputChannels = OutputChannels.Channel04;

        cinemachineBrain.ChannelMask = outputChannels;
        cinemachineStateDrivenCamera.OutputChannel = outputChannels;

        foreach (CinemachineCamera cinemachineCamera in cinemachineCameras)
        {
            cinemachineCamera.OutputChannel = outputChannels;
        }
    }

    private void SetCameraSplitScreen()
    {
        float x = 0f;
        float y = 0f;
        float width = 0f;
        float height = 0f;
        
        switch (splitScreenMode)
        {
            case SplitScreenMode.OnePlayer:
                width = 1f;
                height = 1f;
                break;
            case SplitScreenMode.TwoPlayer:
                switch (playerNumber)
                {
                    case 1:
                        x = 0f;
                        y = 0.5f;
                        break;
                    case 2:
                        x = 0f;
                        y = 0f;
                        break;
                    default:
                        break;
                }
                
                width = 1f;
                height = 0.5f;
                break;
            case SplitScreenMode.ThreePlayer:
                switch (playerNumber)
                {
                    case 1:
                        x = 0f;
                        y = 0.5f;
                        break;
                    case 2:
                        x = 0.5f;
                        y = 0.5f;
                        break;
                    case 3:
                        x = 0f;
                        y = 0f;
                        break;
                    default:
                        break;
                }

                width = 0.5f;
                height = 0.5f;
                break;
            case SplitScreenMode.FourPlayer:
                switch (playerNumber)
                {
                    case 1:
                        x = 0f;
                        y = 0.5f;
                        break;
                    case 2:
                        x = 0.5f;
                        y = 0.5f;
                        break;
                    case 3:
                        x = 0f;
                        y = 0f;
                        break;
                    case 4:
                        x = 0.5f;
                        y = 0f;
                        break;
                    default:
                        break;
                }

                width = 0.5f;
                height = 0.5f;
                break;
            default:
                break;
        }

        camera.rect = new Rect(x,y,width,height);
    }
}
