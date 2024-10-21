using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UIVersionNumberHelper : MonoBehaviour
{
    public void Start()
    {
        this.GetComponent<TextMeshProUGUI>().text = Application.version;
    }
}
