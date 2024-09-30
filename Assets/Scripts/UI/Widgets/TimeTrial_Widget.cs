using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTrial_Widget : Widget
{
    [SerializeField] private TextMeshProUGUI currentTime_TMP;
    [SerializeField] private TextMeshProUGUI goldTime_TMP;
    [SerializeField] private TextMeshProUGUI silverTime_TMP;
    [SerializeField] private TextMeshProUGUI bronzeTime_TMP;

    private const string currentTimeKey = "CurrentTime";
    private const string goldTimeKey = "GoldTime";
    private const string silverTimeKey = "SilverTime";
    private const string bronzeTimeKey = "BronzeTime";

    private void Start()
    {
        // UI_Blackboard.Instance.TryAdd<float>(currentTimeKey, 0.0f);
        // UI_Blackboard.Instance.TryAdd<float>(goldTimeKey, 0.0f);
        // UI_Blackboard.Instance.TryAdd<float>(silverTimeKey, 0.0f);
        // UI_Blackboard.Instance.TryAdd<float>(bronzeTimeKey, 0.0f);

        float currentTime = UI_Blackboard.Instance.TryGetValue<float>(currentTimeKey);
        float goldTime = UI_Blackboard.Instance.TryGetValue<float>(goldTimeKey);
        float silverTime = UI_Blackboard.Instance.TryGetValue<float>(silverTimeKey);
        float bronzeTime = UI_Blackboard.Instance.TryGetValue<float>(bronzeTimeKey);

        SetCurrentTime(currentTime);
        SetGoldTime(goldTime);
        SetSilverTime(silverTime);
        SetBronzeTime(bronzeTime);

        GenericBlackboardElement<float> currentTimeEntry = UI_Blackboard.Instance.TryGetEntry<float>(currentTimeKey);
        GenericBlackboardElement<float> goldTimeEntry = UI_Blackboard.Instance.TryGetEntry<float>(goldTimeKey);
        GenericBlackboardElement<float> silverTimeEntry = UI_Blackboard.Instance.TryGetEntry<float>(silverTimeKey);
        GenericBlackboardElement<float> bronzeTimeEntry = UI_Blackboard.Instance.TryGetEntry<float>(bronzeTimeKey);

        currentTimeEntry.OnValueChanged += OnCurrentTimeUpdate;
        goldTimeEntry.OnValueChanged += OnGoldTimeUpdate;
        silverTimeEntry.OnValueChanged += OnSilverTimeUpdate;
        bronzeTimeEntry.OnValueChanged += OnBronzeTimeUpdate;
    }

    public void OnCurrentTimeUpdate(object? sender, CustomArgs<float> arg)
    {
        SetCurrentTime(arg.Data);
    }

    public void OnGoldTimeUpdate(object? sender, CustomArgs<float> arg)
    {
        SetGoldTime(arg.Data);
    }

    public void OnSilverTimeUpdate(object? sender, CustomArgs<float> arg)
    {
        SetSilverTime(arg.Data);
    }

    public void OnBronzeTimeUpdate(object? sender, CustomArgs<float> arg)
    {
        SetBronzeTime(arg.Data);
    }

    private void SetCurrentTime(float newTime)
    {
        currentTime_TMP.text = FloatToTime(newTime);
    }

    private void SetGoldTime(float newTime)
    {
        goldTime_TMP.text = FloatToTime(newTime);
    }

    private void SetSilverTime(float newTime)
    {
        silverTime_TMP.text = FloatToTime(newTime);
    }

    private void SetBronzeTime(float newTime)
    {
        bronzeTime_TMP.text = FloatToTime(newTime);
    }

    private string FloatToTime(float value)
    {
        // int min = (int) (value/60f);
        // float seconds = value%60f;
        // float milliseconds = seconds%60f;
        // string output = min.ToString().PadLeft(2, '0') + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("0000");

        TimeSpan timeSpan = TimeSpan.FromSeconds(value);

        string output = timeSpan.Minutes.ToString("00") + ":" + timeSpan.Seconds.ToString("00") + ":" + timeSpan.Milliseconds.ToString("00");

        return output;
    }
}
