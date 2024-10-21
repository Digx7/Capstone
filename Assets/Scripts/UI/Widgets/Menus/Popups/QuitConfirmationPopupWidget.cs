using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitConfirmationPopupWidget : Widget
{
    public void OnClickYes()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OnClickNo()
    {
        UI_WidgetManager.Instance.TryUnloadWidget("QuitConfirmation");
    }
}
