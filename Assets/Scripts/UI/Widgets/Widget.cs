using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Widget : MonoBehaviour
{
    public string ID {get; private set;}
    public void SetID(string newID) => ID = newID;

    public virtual void SetUp(){}

    public virtual void Refresh(){}
}
