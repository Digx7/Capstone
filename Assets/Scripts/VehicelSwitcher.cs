using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicelSwitcher : MonoBehaviour
{
    // public GameObject newVehicle;
    public VehicleType newVehicleType;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // other.gameObject.GetComponent<Character>().SetVehicle(newVehicle);
            other.gameObject.GetComponent<Character>().SwtichVehicle(newVehicleType);
        }
    }
}
