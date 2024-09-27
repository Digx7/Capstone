using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRing : MonoBehaviour
{
    [SerializeField] private int ID;
    [SerializeField] private RaceData raceData;

    public bool isRaised {get; private set;} = false;

    private bool hasBeenUsed = false;

    public RaceData GetRaceData()
    {
        isRaised = false;
        return raceData;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && hasBeenUsed == false)
        {
            isRaised = true;
            hasBeenUsed = true;
            GameManager.Instance.SwitchToGameMode("Race");
        }
    }
}
