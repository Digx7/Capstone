//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Controllers/PlayerActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Racing"",
            ""id"": ""e303d9c6-3e91-45cc-b60b-c6f6634e00ea"",
            ""actions"": [
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Button"",
                    ""id"": ""1181c4eb-08b7-41e1-b8a7-dfd4e04cf810"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Brake"",
                    ""type"": ""Button"",
                    ""id"": ""f67b3331-2a83-46ef-a502-b81fb31d046e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""Value"",
                    ""id"": ""6ab5da13-e934-44af-9758-afdbbd27a4e3"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Drift"",
                    ""type"": ""Button"",
                    ""id"": ""9507d7e0-8c60-4b52-baed-302298f882d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UseItem"",
                    ""type"": ""Button"",
                    ""id"": ""47ea952f-771b-4528-93a5-4b606c990f35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9ad4ad83-c039-4fdd-b1e7-38f474edc891"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a34a5daf-8120-4da1-ad20-ea6f1d787e03"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4ff1dd9-0419-4076-a1b6-631ec2b927ab"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b11b9e2-ef14-49a9-bcf8-6b375fb49476"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""e3f19514-5097-4542-9e3f-f0122d8e1ce1"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3f9dbed8-6ab6-400d-a5e5-9d25b373f5d4"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1ec0beb7-927d-4d36-8c67-494eee115e9d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5226bd6a-dd4b-469e-a878-b7d428c111e0"",
                    ""path"": ""<Gamepad>/leftStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24f7198b-3e84-4004-91c3-3f78986d595c"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ac7004e-6281-40d8-8760-c5073dc2eb92"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8915ec7d-1533-4007-aefa-5ef913231edf"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4cc7679-4c55-4771-af0d-1e6f4c000afc"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Racing
        m_Racing = asset.FindActionMap("Racing", throwIfNotFound: true);
        m_Racing_Accelerate = m_Racing.FindAction("Accelerate", throwIfNotFound: true);
        m_Racing_Brake = m_Racing.FindAction("Brake", throwIfNotFound: true);
        m_Racing_Turn = m_Racing.FindAction("Turn", throwIfNotFound: true);
        m_Racing_Drift = m_Racing.FindAction("Drift", throwIfNotFound: true);
        m_Racing_UseItem = m_Racing.FindAction("UseItem", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Racing
    private readonly InputActionMap m_Racing;
    private List<IRacingActions> m_RacingActionsCallbackInterfaces = new List<IRacingActions>();
    private readonly InputAction m_Racing_Accelerate;
    private readonly InputAction m_Racing_Brake;
    private readonly InputAction m_Racing_Turn;
    private readonly InputAction m_Racing_Drift;
    private readonly InputAction m_Racing_UseItem;
    public struct RacingActions
    {
        private @PlayerActions m_Wrapper;
        public RacingActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Accelerate => m_Wrapper.m_Racing_Accelerate;
        public InputAction @Brake => m_Wrapper.m_Racing_Brake;
        public InputAction @Turn => m_Wrapper.m_Racing_Turn;
        public InputAction @Drift => m_Wrapper.m_Racing_Drift;
        public InputAction @UseItem => m_Wrapper.m_Racing_UseItem;
        public InputActionMap Get() { return m_Wrapper.m_Racing; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RacingActions set) { return set.Get(); }
        public void AddCallbacks(IRacingActions instance)
        {
            if (instance == null || m_Wrapper.m_RacingActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_RacingActionsCallbackInterfaces.Add(instance);
            @Accelerate.started += instance.OnAccelerate;
            @Accelerate.performed += instance.OnAccelerate;
            @Accelerate.canceled += instance.OnAccelerate;
            @Brake.started += instance.OnBrake;
            @Brake.performed += instance.OnBrake;
            @Brake.canceled += instance.OnBrake;
            @Turn.started += instance.OnTurn;
            @Turn.performed += instance.OnTurn;
            @Turn.canceled += instance.OnTurn;
            @Drift.started += instance.OnDrift;
            @Drift.performed += instance.OnDrift;
            @Drift.canceled += instance.OnDrift;
            @UseItem.started += instance.OnUseItem;
            @UseItem.performed += instance.OnUseItem;
            @UseItem.canceled += instance.OnUseItem;
        }

        private void UnregisterCallbacks(IRacingActions instance)
        {
            @Accelerate.started -= instance.OnAccelerate;
            @Accelerate.performed -= instance.OnAccelerate;
            @Accelerate.canceled -= instance.OnAccelerate;
            @Brake.started -= instance.OnBrake;
            @Brake.performed -= instance.OnBrake;
            @Brake.canceled -= instance.OnBrake;
            @Turn.started -= instance.OnTurn;
            @Turn.performed -= instance.OnTurn;
            @Turn.canceled -= instance.OnTurn;
            @Drift.started -= instance.OnDrift;
            @Drift.performed -= instance.OnDrift;
            @Drift.canceled -= instance.OnDrift;
            @UseItem.started -= instance.OnUseItem;
            @UseItem.performed -= instance.OnUseItem;
            @UseItem.canceled -= instance.OnUseItem;
        }

        public void RemoveCallbacks(IRacingActions instance)
        {
            if (m_Wrapper.m_RacingActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IRacingActions instance)
        {
            foreach (var item in m_Wrapper.m_RacingActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_RacingActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public RacingActions @Racing => new RacingActions(this);
    public interface IRacingActions
    {
        void OnAccelerate(InputAction.CallbackContext context);
        void OnBrake(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
        void OnDrift(InputAction.CallbackContext context);
        void OnUseItem(InputAction.CallbackContext context);
    }
}