using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    private bool shouldDrift;

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
        Brake(brakeStrength);
        Turn(turnAmount);

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