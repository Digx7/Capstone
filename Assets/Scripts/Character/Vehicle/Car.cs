using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Vehicle
{
    private enum driveTrain {frontWheelDrive, rearWheelDrive}
    
    private float currentBreakForce;
    private float currentSteerAngle;
    
    [SerializeField] private driveTrain driveTrainType;
    [SerializeField] private float motorForce;
    [SerializeField] private float maxVelocity;
    [SerializeField] private AnimationCurve motorCurve;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private AnimationCurve steeringCurve;
    [SerializeField] private float BoostForce;
    
    [SerializeField] private GameObject visual;
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;
    
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;

    [SerializeField] private List<ParticleSystem> driftParticleSystems;

    [SerializeField] private List<Color> driftLevelColors;
    
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        UpdateWheels();
    }

    public override void Initialize()
    {
        base.Initialize();

        OnStartChargeBoost.AddListener(DisplayDriftParticles);
        OnChargeBoostLevelChanged.AddListener(ChangeDriftParticleLevel);
    }
    
    public override void Accelerate(float accelerateStrength)
    {
        
        switch(driveTrainType)
        {
            case driveTrain.frontWheelDrive:
                UpdateMotor(frontLeftWheelCollider, accelerateStrength);
                UpdateMotor(frontRightWheelCollider, accelerateStrength);
                break;  
            case driveTrain.rearWheelDrive:
                UpdateMotor(backLeftWheelCollider, accelerateStrength);
                UpdateMotor(backRightWheelCollider, accelerateStrength);
                break;
            default:
                Debug.LogError("A car somehow has no drive train type");
                break;
        }
    }

    private void UpdateMotor(WheelCollider wheelCollidor, float accelerateStrength)
    {
        if(speed <= maxVelocity)
            wheelCollidor.motorTorque = accelerateStrength * motorForce * motorCurve.Evaluate(Mathf.InverseLerp(0f, maxVelocity, speed));
        else wheelCollidor.motorTorque = 0f;
    }

    public override void Brake(float brakeStrength)
    {
        base.Brake(brakeStrength);


        currentBreakForce = brakeStrength * breakForce;

        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        backLeftWheelCollider.brakeTorque = currentBreakForce;
        backRightWheelCollider.brakeTorque = currentBreakForce;
    }

    public override void Reverse(float reverseStrength)
    {
        base.Reverse(reverseStrength);
        switch(driveTrainType)
        {
            case driveTrain.frontWheelDrive:
                UpdateMotor(frontLeftWheelCollider, -reverseStrength);
                UpdateMotor(frontRightWheelCollider, -reverseStrength);
                break;  
            case driveTrain.rearWheelDrive:
                UpdateMotor(backLeftWheelCollider, -reverseStrength);
                UpdateMotor(backRightWheelCollider, -reverseStrength);
                break;
            default:
                Debug.LogError("A car somehow has no drive train type");
                break;
        }
    }

    public override void Turn(float turnAmount)
    {

        currentSteerAngle = maxSteerAngle * turnAmount * steeringCurve.Evaluate(Mathf.InverseLerp(0f, maxVelocity, speed));
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    public override void Drift()
    {
        base.Drift();
        // Debug.Log("Drift");
    }

    protected override void StartDrifting()
    {
        base.StartDrifting();
        Vector3 currentEulerAngles = new Vector3();

        if(driftDirection == DriftDirection.Right)
        {
            currentEulerAngles.y = 90 + 15;
            visual.transform.localEulerAngles = currentEulerAngles;
        }
        else
        {
            currentEulerAngles.y = 90 - 15;
            visual.transform.localEulerAngles = currentEulerAngles;
        }
    }

    protected override void StopDrifting()
    {
        base.StopDrifting();
        visual.transform.localEulerAngles = new Vector3(0,90,0);
    }

    protected override void ApplyBoost()
    {
        Vector3 force = new Vector3(0, 0, BoostForce * boostLevel);
        rb.AddRelativeForce(force, ForceMode.Impulse);
        base.ApplyBoost();
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        if(!shouldDrift)
        {
            UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
            UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
        }
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        // wheelTransform.position = pos;
    }

    private void DisplayDriftParticles(bool state)
    {
        if(state)
        {
            foreach (ParticleSystem particleSystem in driftParticleSystems)
            {
                particleSystem.Play();
            }
        }
        else
        {
            foreach (ParticleSystem particleSystem in driftParticleSystems)
            {
                particleSystem.Stop();
            }
        }
        

        Debug.Log("Drift Particle State = " + state);
    }

    private void ChangeDriftParticleLevel(int level)
    {
        if(level >= driftLevelColors.Count) return;
        
        foreach (ParticleSystem particleSystem in driftParticleSystems)
        {
            var main = particleSystem.main;
            main.startColor = driftLevelColors[level];
        }

        
    }
}
