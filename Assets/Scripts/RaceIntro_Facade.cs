using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using UnityEngine.Timeline;

// Change 

public class RaceIntro_Facade : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;
    [SerializeField] private Camera introCamera;
    [SerializeField] private SignalReceiver _signalReciever;

    public Camera IntroCamera 
    {
        get {return introCamera;} 
        private set {introCamera = value;}
    }

    public SignalReceiver MainSignalReciever 
    {
        get {return _signalReciever;} 
        private set {_signalReciever = value;}
    }

    public void PlayIntro()
    {
        timeline.Play();
    }
}
