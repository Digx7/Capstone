using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBlackboardElement<T>
{
    public T Value {get; private set;}

    public event EventHandler<CustomArgs<T>> OnValueChanged;

    public void SetValue(T _value)
    {
        Value = _value;

        CustomArgs<T> arg = new CustomArgs<T>(Value);
        OnValueChanged?.Invoke(this, arg);
    }
}

public class CustomArgs<T> : EventArgs
{
    // Property variable
    private readonly T p_EventData;

    // Constructor
    public CustomArgs(T data)
    {
        p_EventData = data;
    }

    // Property for EventArgs argument
    public T Data
    {
        get { return p_EventData; }
    }
}
