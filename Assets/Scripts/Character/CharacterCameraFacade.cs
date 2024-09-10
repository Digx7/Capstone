using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCameraFacade : MonoBehaviour
{
    [SerializeField] private CarCamerasFacade carCamerasFacade;
    public SplitScreenMode splitScreenMode;
    public int playerNumber;

    public void Intialize(SplitScreenMode startingSplitScreenMode, int _playerNumber)
    {
        splitScreenMode = startingSplitScreenMode;
        playerNumber = _playerNumber;

        carCamerasFacade.splitScreenMode = splitScreenMode;
        carCamerasFacade.playerNumber = playerNumber;

        carCamerasFacade.Refresh();
    }

    public void Refresh(SplitScreenMode startingSplitScreenMode)
    {
        splitScreenMode = startingSplitScreenMode;
        carCamerasFacade.splitScreenMode = splitScreenMode;
        carCamerasFacade.Refresh();
    }
}
