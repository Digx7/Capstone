using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILabelHelper : MonoBehaviour
{
    [SerializeField]private Sprite normalSprite;
    [SerializeField]private Sprite highlightedSprite;
    [SerializeField]private Image backgroundImage;

    public void OnSelect()
    {
        backgroundImage.sprite = highlightedSprite;
    }

    public void OnDeselect()
    {
        backgroundImage.sprite = normalSprite;
    }
}
