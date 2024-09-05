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
    
    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;
    
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;
    
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        UpdateWheels();
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

        currentBreakForce = brakeStrength * breakForce;

        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        backLeftWheelCollider.brakeTorque = currentBreakForce;
        backRightWheelCollider.brakeTorque = currentBreakForce;
    }

    public override void Turn(float turnAmount)
    {

        currentSteerAngle = maxSteerAngle * turnAmount * steeringCurve.Evaluate(Mathf.InverseLerp(0f, maxVelocity, speed));
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    public override void Drift()
    {
        Debug.Log("Drift");
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
