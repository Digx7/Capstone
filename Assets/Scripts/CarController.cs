using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";
    private enum driveTrain {frontWheelDrive, rearWheelDrive}
    
    private float horizontalInput;
    private float verticalInput;
    private float currentBreakForce;
    private bool isBreaking;
    private float currentSteerAngle;
    private Rigidbody rb;
    private float speed;

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
    

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        CalcualteSpeed();
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void CalcualteSpeed()
    {
        speed = rb.velocity.magnitude;
    }
    
    private void GetInput()
    {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        switch(driveTrainType)
        {
            case driveTrain.frontWheelDrive:
                UpdateMotor(frontLeftWheelCollider);
                UpdateMotor(frontRightWheelCollider);
                break;  
            case driveTrain.rearWheelDrive:
                UpdateMotor(backLeftWheelCollider);
                UpdateMotor(backRightWheelCollider);
                break;
            default:
                Debug.LogError("A car somehow has no drive train type");
                break;
        }
        
        
        
        ApplyBreaking();
    }

    private void UpdateMotor(WheelCollider wheelCollidor)
    {
        if(rb.velocity.magnitude <= maxVelocity)
            wheelCollidor.motorTorque = verticalInput * motorForce * motorCurve.Evaluate(Mathf.InverseLerp(0f, maxVelocity, speed));
        else wheelCollidor.motorTorque = 0f;
    }

    private void ApplyBreaking()
    {
        currentBreakForce = isBreaking ? breakForce : 0.0f;

        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        backLeftWheelCollider.brakeTorque = currentBreakForce;
        backRightWheelCollider.brakeTorque = currentBreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput * steeringCurve.Evaluate(Mathf.InverseLerp(0f, maxVelocity, speed));
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
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
