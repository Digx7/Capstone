using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : GameController
{

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Accelerate(1.0f);
        }
        else if(context.canceled)
        {
            Accelerate(0.0f);
        }
    }

    public void OnBrake(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Brake(1.0f);
        }
        else if(context.canceled)
        {
            Brake(0.0f);
        }
    }

    public void OnTurn(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Turn(context.ReadValue<float>());
        }
        else if(context.canceled)
        {
            Turn(0.0f);
        }
    }

    public void OnDrift(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Drift(true);
        }
        else if(context.canceled)
        {
            Drift(false);
        }
    }

    public void OnUseItem(InputAction.CallbackContext context)
    {
        UseItem();
    }
}
