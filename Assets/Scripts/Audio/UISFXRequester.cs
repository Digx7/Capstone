using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UISFXRequester : MonoBehaviour
{
    public SFXRequest onHoverRequest;
    public SFXRequest onSelectRequest;

    public void RequestOnHover()
    {
        GlobalSFXSystem.Instance.Request(onHoverRequest);
    }

    public void RequestOnSelect()
    {
        GlobalSFXSystem.Instance.Request(onSelectRequest);
    }
}
