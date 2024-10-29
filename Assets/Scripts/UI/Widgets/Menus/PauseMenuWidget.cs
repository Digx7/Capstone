using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuWidget : MenuWidget
{
    public void OnClickResume()
    {
        UI_WidgetManager.Instance.TryUnloadWidget("PauseMenu");
        Cursor.visible = false;
    }

    public void OnClickReportABug()
    {
        Application.OpenURL("https://forms.gle/w8sx7VCD7P19xUXQ9");
    }

    public void OnClickSettings()
    {
        UI_WidgetManager.Instance.TryLoadWidget("SettingsMenu","SettingsMenu");
    }

    public void OnClickQuit()
    {
        UI_WidgetManager.Instance.TryLoadWidget("QuitToMainMenuConfirmation","QuitToMainMenuConfirmation");
    }

}
