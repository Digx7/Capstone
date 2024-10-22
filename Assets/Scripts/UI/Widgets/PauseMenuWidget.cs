using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuWidget : Widget
{
    public void OnClickResume()
    {
        UI_WidgetManager.Instance.TryUnloadWidget("PauseMenu");
        Cursor.visible = false;
    }

    public void OnClickSettings()
    {
        UI_WidgetManager.Instance.TryLoadWidget("SettingsMenu","SettingsMenu");
    }

    public void OnClickQuit()
    {
        UI_WidgetManager.Instance.TryLoadWidget("QuitToMainMenuConfirmation","QuitToMainMenuConfirmation");
    }

    public override void SetUp()
    {
        Cursor.visible = true;
    }
}
