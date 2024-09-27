using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Character possesedCharacter;
    [SerializeField] private bool Enabled = true;
    public void SetEnabled(bool value) => Enabled = value;

    public virtual void Accelerate(float accelerateStrength)
    {
        if(Enabled) possesedCharacter.Accelerate(accelerateStrength);
    }

    public virtual void Brake(float brakeStrength)
    {
        if(Enabled) possesedCharacter.Brake(brakeStrength);
    }

    public virtual void Turn(float turnAmount)
    {
        if(Enabled) possesedCharacter.Turn(turnAmount);
    }

    public virtual void Drift(bool isDrifting)
    {
        if(Enabled) possesedCharacter.Drift(isDrifting);
    }

    public virtual void UseItem()
    {
        if(Enabled) possesedCharacter.UseItem();
    }

    public bool TryToPossesCharacter(Character character)
    {
        if (character.IsValidCharacter())
        {
            possesedCharacter = character;
            return true;
        }
        else return false;
    }

    
}
