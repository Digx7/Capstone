using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : Vehicle
{
    public override void Accelerate(float accelerateStrength)
    {
        Debug.Log("Vroom");
    }

    public override void Brake(float brakeStrength)
    {
        
    }

    public override void Turn(float turnAmount)
    {
        
    }

    public override void Drift()
    {
        
    }
}
