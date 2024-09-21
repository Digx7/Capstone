using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Floater : MonoBehaviour
{
    public UnityEvent<bool> OnSubmerged;
    [SerializeField] private float depthBeforeSubmerged = 1f;
    [SerializeField] private float displacementAmount = 3f;
    [SerializeField] private int floaterCount = 1;
    [SerializeField] private float waterDrag = 0.99f;
    [SerializeField] private float waterAngularDrag = 0.5f;

    private bool isSubmerged = false;

    // References
    public Rigidbody rigidbody;
    private WaveManager waveManager;
    
    private void Start()
    {
        waveManager = WaveManager.Instance;
    }

    private void FixedUpdate()
    {
        if(rigidbody == null) return;
        if(waveManager == null) return;


        // rigidbody.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
        
        float waveHeight = waveManager.GetWaveHeightAtLocation(transform.position);
        if (transform.position.y < waveHeight)
        {
            if(!isSubmerged)
            {
                isSubmerged = true;
                OnSubmerged.Invoke(true);
            }
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rigidbody.AddForce(displacementMultiplier * -rigidbody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigidbody.AddTorque(displacementMultiplier * -rigidbody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        else if (isSubmerged)
        {
            isSubmerged = false;
            OnSubmerged.Invoke(false);
        }
    }
}
