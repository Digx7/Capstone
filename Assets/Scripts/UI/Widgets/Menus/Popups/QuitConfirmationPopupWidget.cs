using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitConfirmationPopupWidget : PopupWidget
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
        UI_WidgetManager.Instance.TryUnloadWidget("QuitGameConfirmation");
    }
}
