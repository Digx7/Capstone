using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStateFacade : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Idle()
    {
        animator.SetTrigger("Idle");
    }

    public void Drive()
    {
        animator.SetTrigger("Drive");
    }

    public void Boost()
    {
        animator.SetTrigger("Boost");
    }

    public void Reverse()
    {
        animator.SetTrigger("Reverse");
    }

    public void Win()
    {
        animator.SetTrigger("Win");
    }
}
