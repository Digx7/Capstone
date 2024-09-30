using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRing : MonoBehaviour
{
    [SerializeField] private string ID;

    [SerializeField] private bool isActiveFinishLine;

    private void Start()
    {
        GameManager.Instance.OnSwitchGameMode.AddListener(CheckIfIsActiveFinishLine);
    }

    private void CheckIfIsActiveFinishLine()
    {
        if(GameMode.Instance is RaceMode)
        {
            RaceMode raceMode = GameMode.Instance as RaceMode;
            if(raceMode.raceData.endingLoaction == ID) isActiveFinishLine = true;
            else isActiveFinishLine = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && isActiveFinishLine)
        {
            isActiveFinishLine = false;

            RaceMode raceMode = GameMode.Instance as RaceMode;
            raceMode.EndRace();
        }
    }
}
