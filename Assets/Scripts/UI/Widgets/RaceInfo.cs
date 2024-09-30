using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaceInfo : Widget
{
    public TextMeshProUGUI raceTypeUI;
    public TextMeshProUGUI finishDesitinationUI;

    private const string raceTypeKey = "RaceType";
    private const string finishDestinationKey = "FinishDestination";

    public void Start()
    {
        string raceType = UI_Blackboard.Instance.TryGetValue<string>(raceTypeKey);
        string finishDestination = UI_Blackboard.Instance.TryGetValue<string>(finishDestinationKey);

        SetRaceType(raceType);
        SetFinishDesintation(finishDestination);

        GenericBlackboardElement<string> raceTypeEntry = UI_Blackboard.Instance.TryGetEntry<string>(raceTypeKey);
        GenericBlackboardElement<string> finishDestinationEntry = UI_Blackboard.Instance.TryGetEntry<string>(finishDestinationKey);

        raceTypeEntry.OnValueChanged += OnRaceTypeUpdated;
        finishDestinationEntry.OnValueChanged += OnFinishDestinationUpdated;
    }

    private void OnDestroy()
    {
        GenericBlackboardElement<string> raceTypeEntry = UI_Blackboard.Instance.TryGetEntry<string>(raceTypeKey);
        GenericBlackboardElement<string> finishDestinationEntry = UI_Blackboard.Instance.TryGetEntry<string>(finishDestinationKey);

        raceTypeEntry.OnValueChanged -= OnRaceTypeUpdated;
        finishDestinationEntry.OnValueChanged -= OnFinishDestinationUpdated;
    }

    public void OnRaceTypeUpdated(object? sender, CustomArgs<string> arg)
    {
        SetRaceType(arg.Data);
    }

    public void OnFinishDestinationUpdated(object? sender, CustomArgs<string> arg)
    {
        SetFinishDesintation(arg.Data);
    }

    private void SetRaceType(string _raceType)
    {
        raceTypeUI.text = _raceType;
    }

    private void SetFinishDesintation(string _finishDesination)
    {
        finishDesitinationUI.text = _finishDesination;
    }
}
