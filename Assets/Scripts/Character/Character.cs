using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    private DriftDirection lastDriftDirection;
    
    public GameObject startingVehicle;
    
    [SerializeField] private int ID;
    [SerializeField] private Vehicle vehicle;
    [SerializeField] private DriverModelController driverModelController;
    [SerializeField] private List<GameObject> allPossibleItemPrefabs;
    [SerializeField] private GameObject currentItemPrefab = null;

    public UnityEvent OnBoost;

    private float lastAccelerateStrength = 0.0f;
    private float lastBrakeStrength = 0.0f;
    private float lastTurnAmount = 0.0f;
    private bool lastDriftState = false;
    private int chargedDriftLevel = 0;
    private float currentChargeTime = 0;
    [SerializeField] private float timeBetweenBoostCharges;
    [SerializeField] private int maxNumberOfBoostLevelsPerDrift;

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
        vehicle.TryDriftInDirection(lastDriftState,lastDriftDirection);
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
            if(lastTurnAmount < 0.1 && lastTurnAmount > -0.1 && lastDriftState == false) return;
            vehicle.TryDrift(_isDrifting);
            if(lastDriftState == false && _isDrifting == true) 
            {
                if(lastTurnAmount > 0.1) lastDriftDirection = DriftDirection.Right;
                else if(lastTurnAmount < -0.1) lastDriftDirection = DriftDirection.Left;
                else lastDriftDirection = DriftDirection.Neither;
                StartDrift();
            }
            if(lastDriftState == true && _isDrifting == false) 
            {
                lastDriftDirection = DriftDirection.Neither;
                StopDrift();
            }

            lastDriftState = _isDrifting;
        }
    }

    private void StartDrift()
    {
        Debug.Log("Start Drifting");
        StartCoroutine(ChargeDrift());
    }

    private void StopDrift()
    {
        // StopCoroutine(ChargeDrift());
    }

    private IEnumerator ChargeDrift()
    {
        currentChargeTime = 0;
        // Debug.Log("Coroutine Started");
        vehicle.OnChargeBoostLevelChanged.Invoke(chargedDriftLevel);
        vehicle.OnStartChargeBoost.Invoke(true);

        yield return null;
        
        while(lastDriftState)
        {
            currentChargeTime += Time.deltaTime;
            if(currentChargeTime >= timeBetweenBoostCharges)
            {
                if(chargedDriftLevel < maxNumberOfBoostLevelsPerDrift) 
                {
                    chargedDriftLevel++;
                    vehicle.OnChargeBoostLevelChanged.Invoke(chargedDriftLevel);    
                }
                currentChargeTime = 0;
                // Debug.Log("DriftLevel " + chargedDriftLevel);
            }

            yield return null;
        }  

        if(chargedDriftLevel > 0)
        {
            Boost(chargedDriftLevel);   
        }

        currentChargeTime = 0;
        chargedDriftLevel = 0;
        vehicle.OnChargeBoostLevelChanged.Invoke(chargedDriftLevel);
        vehicle.OnStartChargeBoost.Invoke(false);

        // Debug.Log("CoroutineEnded");
    }

    public void Boost(int level = 1)
    {
        if(vehicle != null)
        {
            vehicle.Boost(level);
            OnBoost.Invoke();
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
