using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race : GameMode
{
    public int NumberOfLaps {private get; set;}
    
    public override void Setup()
    {
        // Spawn all characters
        
        throw new NotImplementedException();
    }

    public override void Start()
    {
        // countdown 
        // run race for NumberOfLaps laps
        
        throw new NotImplementedException();
    }

    public override void Finish()
    {
        // Once all characters finish
        // Take control from all characters
        // Show results
        
        throw new NotImplementedException();
    }

    public override void Teardown()
    {
        // Save results
        // Return to previous state
        
        throw new NotImplementedException();
    }
}
