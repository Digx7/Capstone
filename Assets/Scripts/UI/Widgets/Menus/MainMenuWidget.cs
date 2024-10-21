using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuWidget : Widget
{
    public void OnClickPlay()
    {
        SceneManager.LoadScene("TestIsland");
        UI_WidgetManager.Instance.TryUnloadWidget("MainMenu");
    }

    public void OnClickSettings()
    {
        UI_WidgetManager.Instance.TryLoadWidget("SettingsMenu","SettingsMenu");
        UI_WidgetManager.Instance.TryUnloadWidget("MainMenu");
    }

    public void OnClickQuit()
    {
        UI_WidgetManager.Instance.TryLoadWidget("QuitConfirmation","QuitConfirmation");
    }
}
