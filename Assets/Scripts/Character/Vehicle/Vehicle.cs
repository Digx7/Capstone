using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    private bool shouldDrift = false;
    private bool shouldBoost = false;

    protected Rigidbody rb;
    protected float speed;

    private float accelerateStrength;
    private float brakeStrength;
    private float turnAmount;
    
    public void Initialize()
    {
        rb = gameObject.GetComponentInParent<Rigidbody>();
        rb.useGravity = true;
    }

    public virtual void FixedUpdate()
    {
        CalcualteSpeed();

        Accelerate(accelerateStrength);
        if(shouldBoost) ApplyBoost();
        Brake(brakeStrength);
        if(!shouldDrift) Turn(turnAmount);

        if(shouldDrift) Drift();
    }

    private void CalcualteSpeed()
    {
        speed = rb.velocity.magnitude;
    }

    public virtual void Accelerate(float accelerateStrength)
    {
        
    }

    public virtual void Brake(float brakeStrength)
    {
        
    }

    public virtual void Turn(float turnAmount)
    {
        
    }

    public virtual void Drift()
    {
        float driftAmount = turnAmount;
        driftAmount = driftAmount.Remap(-1, 0, 1, 1);
        Debug.Log("Trying To Drift | drift: " + driftAmount + " Turn: " + turnAmount);
        Turn(driftAmount);
    }
    
    public virtual void Boost()
    {
        shouldBoost = true;
    }

    protected virtual void ApplyBoost()
    {
        shouldBoost = false;
    }

    public void TryAccelerate(float _accelerateStrength)
    {
        accelerateStrength = _accelerateStrength;
    }

    public void TryBrake(float _brakeStrength)
    {
        brakeStrength = _brakeStrength;
    }

    public void TryTurn(float _turnAmount)
    {
        turnAmount = _turnAmount;
    }

    public void TryDrift(bool isDrifting)
    {
        shouldDrift = isDrifting;
    }
}
