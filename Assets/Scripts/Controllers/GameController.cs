using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Character possesedCharacter;

    public virtual void Accelerate(float accelerateStrength)
    {
        possesedCharacter.Accelerate(accelerateStrength);
    }

    public virtual void Brake(float brakeStrength)
    {
        possesedCharacter.Brake(brakeStrength);
    }

    public virtual void Turn(float turnAmount)
    {
        possesedCharacter.Turn(turnAmount);
    }

    public virtual void Drift(bool isDrifting)
    {
        possesedCharacter.Drift(isDrifting);
    }

    public virtual void UseItem()
    {
        possesedCharacter.UseItem();
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
