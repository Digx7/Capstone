using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfGravity : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]private Transform centerOfGravity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (centerOfGravity)
        {
            rb.centerOfMass = centerOfGravity.localPosition;
        }
    }
}
