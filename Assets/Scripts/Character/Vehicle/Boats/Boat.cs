using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Vehicle
{
    [SerializeField] private float maxVelocity;
    [SerializeField] private float motorForce;
    [SerializeField] private AnimationCurve motorForceCurve;
    [SerializeField] private float reverseForce;
    [SerializeField] private AnimationCurve reverseForceCurve;
    [SerializeField] private float breakForce;
    [SerializeField] private AnimationCurve breakForceCurve;
    [SerializeField] private float steeringTorque;
    [SerializeField] private AnimationCurve steeringTorqueCurve;
    [SerializeField] private float BoostForce;
    [SerializeField] private float acceptableRollAngle;
    [SerializeField] private float acceptablePitchAngle;

    private bool isSubmerged = false;
    public void SetSubmerged(bool value) => isSubmerged = value;


    // References
    [SerializeField] private GameObject visual;
    [SerializeField] private List<Floater> floaters;
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        KeepUp();
    }

    public override void Initialize()
    {
        base.Initialize();

        foreach (Floater floater in floaters)
        {
            floater.rigidbody = rb;
        }
    }

    public override void SwitchToVehicle()
    {
        // rb.useGravity = false;
        // rb.mass = mass;
        // rb.drag = drag;
    }

    public override void SwitchOffVehicle()
    {
        
    }
    
    public override void Accelerate(float accelerateStrength)
    {
        if(!isSubmerged) return;
        
        Vector3 force = new Vector3(0,0,1);

        // force = force * accelerateStrength * motorForce * motorForceCurve.Evaluate(Mathf.InverseLerp(0f, maxVelocity, speed));
        force = force * accelerateStrength * motorForce;

        rb.AddRelativeForce(force);
    }

    public override void Brake(float brakeStrength)
    {
        if(!isSubmerged) return;

        base.Brake(brakeStrength);

        Vector3 force = new Vector3(0,0,-1);

        // force = force * brakeStrength * breakForce * breakForceCurve.Evaluate(Mathf.InverseLerp(maxVelocity, 0f, speed));
        force = force * brakeStrength * breakForce;

        rb.AddRelativeForce(force);
    }

    public override void Reverse(float reverseStrength)
    {
        if(!isSubmerged) return;
        
        Vector3 force = new Vector3(0,0,-1);

        // force = force * reverseStrength * reverseForce * reverseForceCurve.Evaluate(Mathf.InverseLerp(0f, maxVelocity, speed));
        force = force * reverseStrength * reverseForce;

        rb.AddRelativeForce(force);
    }

    public override void Turn(float turnAmount)
    {
        if(!isSubmerged) return;
        
        Vector3 torque = new Vector3(0,1,0);

        // torque = torque * steeringTorque * turnAmount * steeringTorqueCurve.Evaluate(Mathf.InverseLerp(0f, maxVelocity, speed));
        torque = torque * steeringTorque * turnAmount;

        rb.AddRelativeTorque(torque);
    }

    public override void Drift()
    {
        if(!isSubmerged) return;
        
        base.Drift();
    }

    protected override void StartDrifting()
    {
        if(!isSubmerged) return;
        
        base.StartDrifting();
        Vector3 currentEulerAngles = new Vector3();

        if(driftDirection == DriftDirection.Right)
        {
            currentEulerAngles.y = 180 + 15;
            visual.transform.localEulerAngles = currentEulerAngles;
        }
        else
        {
            currentEulerAngles.y = 180 - 15;
            visual.transform.localEulerAngles = currentEulerAngles;
        }
    }

    protected override void StopDrifting()
    {
        base.StopDrifting();
        visual.transform.localEulerAngles = new Vector3(0,180,0);
    }

    protected override void ApplyBoost()
    {
        Vector3 force = new Vector3(0, 0, BoostForce * boostLevel);
        rb.AddRelativeForce(force, ForceMode.Impulse);
        base.ApplyBoost();
    }

    private void KeepUp()
    {
        Vector3 boatEularAngles = transform.parent.localRotation.eulerAngles;

        // Debug.Log("boat rot: " + boatEularAngles);

        if(boatEularAngles.x < 180 && boatEularAngles.x > acceptablePitchAngle) boatEularAngles.x = acceptablePitchAngle;
        if(boatEularAngles.x > 180 && boatEularAngles.x < (360 - acceptablePitchAngle)) boatEularAngles.x = (360 - acceptablePitchAngle);

        if(boatEularAngles.z < 180 && boatEularAngles.z > acceptableRollAngle) boatEularAngles.z = acceptableRollAngle;
        if(boatEularAngles.z > 180 && boatEularAngles.z < (360 - acceptableRollAngle)) boatEularAngles.z = (360 - acceptableRollAngle);

        transform.parent.localRotation = Quaternion.Euler(boatEularAngles);
    }
}
