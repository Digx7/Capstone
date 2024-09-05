using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode
{
    public abstract void Setup();
    public abstract void Start();
    public abstract void Finish();
    public abstract void Teardown();
}
