using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum VehicleType {Car, Boat};

public class Character : MonoBehaviour
{
    private DriftDirection lastDriftDirection;
    
    public GameObject characterPrefab;
    public GameObject carPrefab;
    public GameObject boatPrefab;

    [SerializeField] private VehicleType startingVehicleType;
    
    private int ID;
    private Vehicle vehicle;
    private GameObject characterModel;
    private Car car;
    private Boat boat;
    private DriverModelController driverModelController;
    [SerializeField] private List<GameObject> allPossibleItemPrefabs;
    private GameObject currentItemPrefab = null;

    public UnityEvent OnIdle;
    public UnityEvent OnDrive;
    public UnityEvent OnReverse;
    public UnityEvent OnBoost;
    public UnityEvent OnDrift;
    public UnityEvent OnWin;
    public UnityEvent OnGetItem;
    public UnityEvent OnUseItem;

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
        LoadVehicles();
        LoadCharacter();
    }

    private void LoadVehicles()
    {
        // GameObject spawnedCar = Instantiate(carPrefab, this.transform);
        // GameObject spawnedBoat = Instantiate(boatPrefab, this.transform);

        // car = spawnedCar.GetComponent<Car>();
        // boat = spawnedBoat.GetComponent<Boat>();

        // car.Initialize();
        // boat.Initialize();

        // car.OnStartReversing.AddListener(StartReversing);
        // boat.OnStartReversing.AddListener(StartReversing);
        // car.OnStopReversing.AddListener(StopReversing);
        // boat.OnStopReversing.AddListener(StopReversing);
        // car.OnIdle.AddListener(Idle);
        // boat.OnIdle.AddListener(Idle);

        // boat.SwitchOffVehicle();
        // car.SwitchToVehicle();

        // car.gameObject.SetActive(true);
        // boat.gameObject.SetActive(false);

        // vehicle = car;

        car = SpawnVehicle(carPrefab).GetComponent<Car>();
        boat = SpawnVehicle(boatPrefab).GetComponent<Boat>();

        SetUpVehicle(car);
        SetUpVehicle(boat);

        SwtichVehicle(startingVehicleType);
    }

    private GameObject SpawnVehicle(GameObject prefab) => Instantiate(prefab, this.transform);

    private void SetUpVehicle(Vehicle vehicle)
    {
        vehicle.Initialize();

        vehicle.OnStartReversing.AddListener(StartReversing);
        vehicle.OnStopReversing.AddListener(StopReversing);
        vehicle.OnIdle.AddListener(Idle);
    }

    private void LoadCharacter()
    {
        characterModel = Instantiate(characterPrefab, this.transform);
    }

    public void SwtichVehicle(VehicleType newVehicleType)
    {
        switch (newVehicleType)
        {
            case VehicleType.Car:
                boat.SwitchOffVehicle();
                car.SwitchToVehicle();
                
                car.gameObject.SetActive(true);
                boat.gameObject.SetActive(false);
                vehicle = car;
                break;
            case VehicleType.Boat:
                car.SwitchOffVehicle();
                boat.SwitchToVehicle();

                boat.gameObject.SetActive(true);
                car.gameObject.SetActive(false);
                vehicle = boat;
                break;
            default:
                break;
        }

        Accelerate(lastAccelerateStrength);
        Brake(lastBrakeStrength);
        Turn(lastTurnAmount);
        vehicle.TryDriftInDirection(lastDriftState,lastDriftDirection);
    }

    private void Idle()
    {
        OnIdle.Invoke();
    }

    public void Accelerate(float _accelerateStrength)
    {
        if(vehicle != null)
        { 
            vehicle.TryAccelerate(_accelerateStrength);
            lastAccelerateStrength = _accelerateStrength;
            OnDrive.Invoke();
        }
    }

    private void StartReversing()
    {
        OnReverse.Invoke();
    }

    private void StopReversing()
    {
        
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
        OnDrift.Invoke();
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
            OnGetItem.Invoke();
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
            OnUseItem.Invoke();
        }
    }

    public bool IsValidCharacter()
    {
        if(vehicle != null) return true;
        else return false;
    }
}
