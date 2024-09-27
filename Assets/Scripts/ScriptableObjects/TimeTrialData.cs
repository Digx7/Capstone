using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTimeTrialData", menuName = "ScriptableObjects/Race/TimeTrial", order = 1)]
public class TimeTrialData : RaceData
{
    public float goldMedalTime;
    public float silverMedalTime;
    public float bronzeMedalTime;
}
