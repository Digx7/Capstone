using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuWidget : MenuWidget
{
    public void OnClickPlay()
    {
        Cursor.visible = false;
        UI_WidgetManager.Instance.TryUnloadWidget("MainMenu");
        SceneManager.LoadScene("TestIsland");
    }

    public void OnClickSettings()
    {
        UI_WidgetManager.Instance.TryUnloadWidget("MainMenu");
        UI_WidgetManager.Instance.TryLoadWidget("SettingsMenu","SettingsMenu");
    }

    public void OnClickQuit()
    {
        UI_WidgetManager.Instance.TryLoadWidget("QuitGameConfirmation","QuitGameConfirmation");
    }
}
