using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTelematry : MonoBehaviour
{
    private CarController carController;
    private Rigidbody rb;

    private void Start()
    {
        carController = gameObject.GetComponent<CarController>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.Log("Velocity = " + rb.velocity.magnitude);
    }
}
