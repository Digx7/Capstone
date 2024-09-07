using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject startingVehicle;
    
    [SerializeField] private int ID;
    [SerializeField] private Vehicle vehicle;
    [SerializeField] private DriverModelController driverModelController;
    [SerializeField] private List<GameObject> allPossibleItemPrefabs;
    [SerializeField] private GameObject currentItemPrefab = null;

    private float lastAccelerateStrength = 0.0f;
    private float lastBrakeStrength = 0.0f;
    private float lastTurnAmount = 0.0f;
    private bool lastDriftState = false;

    public void Start()
    {
        SetVehicle(startingVehicle);
        SetVehicle(startingVehicle);
    }

    public void SetVehicle(GameObject newVehicle)
    {
        if (vehicle != null) Destroy(vehicle.gameObject);

        gameObject.GetComponent<Rigidbody>().useGravity = false;
        Vector3 pos = transform.position;
        pos.y += 0.1f;
        transform.position = pos;

        GameObject spawnedVehicle = Instantiate(newVehicle, this.transform);
        vehicle = spawnedVehicle.GetComponent<Vehicle>();
        vehicle.Initialize();
        Accelerate(lastAccelerateStrength);
        Brake(lastBrakeStrength);
        Turn(lastTurnAmount);
        Drift(lastDriftState);

        Debug.Log("" + lastAccelerateStrength + " " + lastBrakeStrength + " " + lastTurnAmount + " " + lastDriftState);
    }

    public void Accelerate(float _accelerateStrength)
    {
        if(vehicle != null)
        { 
            vehicle.TryAccelerate(_accelerateStrength);
            lastAccelerateStrength = _accelerateStrength;
        }
    }

    public void Brake(float _brakeStrength)
    {
        if(vehicle != null)
        { 
            vehicle.TryBrake(_brakeStrength);
            lastBrakeStrength = _brakeStrength;
        }
    }

    public void Turn(float _turnAmount)
    {
        if(vehicle != null)
        { 
            vehicle.TryTurn(_turnAmount);
            lastTurnAmount = _turnAmount;
        }
    }

    public void Drift(bool _isDrifting)
    {
        if(vehicle != null)
        { 
            vehicle.TryDrift(_isDrifting);
            lastDriftState = _isDrifting;
        }
    }

    public void Boost()
    {
        if(vehicle != null)
        {
            vehicle.Boost();
        }
    }

    public void GetItem()
    {
        if(allPossibleItemPrefabs.Count > 0)
        {
            int choice = Random.Range( 0, allPossibleItemPrefabs.Count);
            currentItemPrefab = allPossibleItemPrefabs[choice];
            Debug.Log("You Got A " + currentItemPrefab.GetComponent<Item>().name);
        }
    }

    public void UseItem()
    {
        if (currentItemPrefab != null)
        {
            Debug.Log("UseItem");

            Vector3 pos = transform.position;
            pos.z += 1.0f;
            Quaternion rot = transform.rotation;

            GameObject spawnedItem = Instantiate(currentItemPrefab, pos, rot);
            spawnedItem.GetComponent<Item>().Use();

            currentItemPrefab = null;
        }
    }

    public bool IsValidCharacter()
    {
        if(vehicle != null) return true;
        else return false;
    }
}
