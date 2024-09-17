using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vehicle : MonoBehaviour
{
    
    // Events
    public UnityEvent OnIdle;
    public UnityEvent OnBoost;
    public UnityEvent<int> OnChargeBoostLevelChanged;
    public UnityEvent<bool> OnStartChargeBoost;
    public UnityEvent OnStartReversing;
    public UnityEvent OnStopReversing;
    


    // References
    private Rigidbody _rb;
    protected Rigidbody rb
    {
        get
        {
            if(_rb == null) 
            {
                ErrorNullRigidbody();
                return null;
            }
            return _rb;
        }
        set => _rb = value;
    }



    // Vehicle State Variables
    protected bool shouldDrift = false;
    protected bool shouldBoost = false;
    protected int boostLevel = 1;
    protected bool isReversing = false;
    protected float speed;  // the vehicles current speed, calculated every frame
    protected DriftDirection driftDirection;  // the current direction the vehicle is drifting
    private bool idleTimerIsGoing = false;
    private bool isIdle = false;

    // Affected by controller input then passed to Vehicle type
    private float accelerateStrength;
    private float brakeStrength;
    private float turnAmount;

    public virtual void Initialize()
    {
        
        if(transform.parent.gameObject.TryGetComponent<Rigidbody>(out _rb))
        {
            rb.useGravity = true; 
        }
        else
        {
            ErrorNullRigidbody();
        }
        
        
    }

    public virtual void Telemetry()
    {
        Debug.Log("DriftDirection = " + driftDirection + "ShouldDrift = " + shouldDrift);
    }

    public virtual void FixedUpdate()
    {
        CalcualteSpeed();

        if(isReversing)
        {
            Brake(0f);
            Reverse(brakeStrength);
            Turn(turnAmount);
        }
        else
        {
            Accelerate(accelerateStrength);
            Brake(brakeStrength);

            if(shouldBoost) ApplyBoost();

            if(shouldDrift)
            {
                Drift();
            }
            else
            {
                Turn(turnAmount);
            }
        }
    }

    private void CalcualteSpeed()
    {
        if(rb == null)
        {
            ErrorNullRigidbody();
            return;
        }

        speed = rb.velocity.magnitude;

        if(!isIdle)
        {
            if(!idleTimerIsGoing)
            {
                if(speed < 0.1) StartCoroutine("IdleWaitTimer");
            }
            else if(speed > 0.1)
            {
                Debug.Log("Stopting Idle Timer");
                StopCoroutine("IdleWaitTimer");
                idleTimerIsGoing = false;
                isIdle = false;
            }
        }
        else if(speed > 0.1)
        {
            isIdle = false;
        }

        // Debug.Log("Speed: " + speed + " IdleTimerIsGoing: " + idleTimerIsGoing + " IsIdle: " + isIdle);
    }

    private IEnumerator IdleWaitTimer()
    {
        Debug.Log("IdleWaitTimer Started");
        idleTimerIsGoing = true;
        yield return new WaitForSeconds(1);
        idleTimerIsGoing = false;
        Debug.Log("OnIdle.Invoke");
        OnIdle.Invoke();
        isIdle = true;
    }

    public virtual void Accelerate(float accelerateStrength)
    {
        
    }

    public virtual void Brake(float brakeStrength)
    {
        if(speed < 0.1 && brakeStrength > 0.1)
        {
            isReversing = true;
            OnStartReversing.Invoke();
            return;
        }
    }

    public virtual void Reverse(float reverseStrength)
    {
        // Debug.Log("Reverse");
        isReversing = true;
    }

    public virtual void Turn(float turnAmount)
    {
        
    }

    public void SetDriftDirection(DriftDirection newDriftDirection)
    {
        driftDirection = newDriftDirection;
    }

    public virtual void Drift()
    {
        float driftAmount = turnAmount;
        
        if(driftDirection == DriftDirection.Right)
            driftAmount = map(driftAmount, -1, 1, 0.25f, 1);
        else 
        {
            driftAmount *= -1;
            driftAmount = map(driftAmount, -1, 1, -0.25f, -1);
        }

        // Debug.Log("Trying To Drift | drift: " + driftAmount + " Turn: " + turnAmount);
        Turn(driftAmount);
    }

    public float map(float value, float start1, float stop1, float start2, float stop2) {
        float outgoing = start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
        string badness = null;

        if (outgoing != outgoing) 
        {
            badness = "NaN (not a number)";

        }
        
        return outgoing;
  }
    
    public virtual void Boost(int level = 1)
    {
        shouldBoost = true;
        boostLevel = level;
    }

    protected virtual void ApplyBoost()
    {
        OnBoost.Invoke();
        shouldBoost = false;
    }

    public void TryAccelerate(float _accelerateStrength)
    {
        accelerateStrength = _accelerateStrength;
        if(accelerateStrength > 0.1) 
        {
            isReversing = false;
            OnStopReversing.Invoke();
        }
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
        // if(turnAmount < 0.1 && turnAmount > -0.1) return;
        if(shouldDrift == false && isDrifting == true) 
        {
            if(turnAmount > 0.1) driftDirection = DriftDirection.Right;
            else if(turnAmount < -0.1) driftDirection = DriftDirection.Left;
            else driftDirection = DriftDirection.Neither;
            StartDrifting();
        }
        if(shouldDrift == true && isDrifting == false) 
        {
            driftDirection = DriftDirection.Neither;
            StopDrifting();
        }
        shouldDrift = isDrifting;
    }

    public void TryDriftInDirection(bool isDrifting, DriftDirection direction)
    {
        if(isDrifting) DriftInDirection(isDrifting, direction);
    }

    protected virtual void StartDrifting()
    {
        
    }

    protected virtual void StopDrifting()
    {
        
    }

    protected virtual void DriftInDirection(bool isDrifting, DriftDirection direction)
    {
        shouldDrift = isDrifting;
        driftDirection = direction;
        StartDrifting();
    }

    protected virtual void ErrorNullRigidbody()
    {
        Debug.Log("A vehicle has no reference to a rigidbody component.\nAdd the Rigidbody to its parent.\nDeleting Vehicle.");
        Destroy(gameObject);
    } 
}
