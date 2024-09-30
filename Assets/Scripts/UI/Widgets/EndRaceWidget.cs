using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndRaceWidget : Widget
{
    public TextMeshProUGUI positionUI;

    public void Start()
    {
        int position = UI_Blackboard.Instance.TryGetValue<int>("RaceResult");
        SetPosition(position);
    }

    public void SetPosition(int finalPosition)
    {
        string result;

        switch (finalPosition)
        {
            case 1:
                result = "1st";
                break;
            case 2:
                result = "2nd";
                break;
            case 3:
                result = "3rd";
                break;
            default:
                result = finalPosition.ToString() + "th";
                break;
        }

        positionUI.text = result;
    }

    public void OnClickContinue()
    {
        GameManager.Instance.SwitchToGameMode("FreeRoam");
        UI_WidgetManager.Instance.TryUnloadWidget("EndRace");
    }
}
