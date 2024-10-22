using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMainMenuConfirmationPopupWidget : Widget
{
    public void OnClickYes()
    {
        UI_WidgetManager.Instance.TryUnloadWidget("PauseMenu");
        UI_WidgetManager.Instance.TryUnloadWidget("QuitToMainMenuConfirmation");

        SceneManager.LoadScene("Main Menu");
    }

    public void OnClickNo()
    {
        UI_WidgetManager.Instance.TryUnloadWidget("QuitToMainMenuConfirmation");
    }
}
