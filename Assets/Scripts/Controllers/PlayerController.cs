using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : GameController
{
    PlayerActions playerActions;

    float turn;

    void Awake()
    {
        playerActions = new PlayerActions();


        playerActions.Racing.Accelerate.performed += ctx => Accelerate(1.0f);
        playerActions.Racing.Accelerate.canceled += ctx => Accelerate(0.0f);

        playerActions.Racing.Brake.performed += ctx => Brake(1.0f);
        playerActions.Racing.Brake.canceled += ctx => Brake(0.0f);

        playerActions.Racing.Turn.performed += ctx => Turn(ctx.ReadValue<float>());
        playerActions.Racing.Turn.canceled += ctx => Turn(0.0f);

        playerActions.Racing.Drift.performed += ctx => Drift(true);
        playerActions.Racing.Drift.canceled += ctx => Drift(false);

        playerActions.Racing.UseItem.performed += ctx => UseItem();
    }

    void OnEnable()
    {
        playerActions.Racing.Enable();
    }

    void OnDisable()
    {
        playerActions.Racing.Disable();
    }
}
