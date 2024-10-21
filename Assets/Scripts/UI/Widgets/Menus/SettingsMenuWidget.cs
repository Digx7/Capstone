using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuWidget : Widget
{
    [SerializeField] private GameObject gameplayPage;
    [SerializeField] private GameObject graphicsPage;
    [SerializeField] private GameObject soundPage;
    private List<GameObject> allPages;

    private void Awake()
    {
        allPages = new List<GameObject> {gameplayPage, graphicsPage, soundPage};
    }

    private void DisableAllPages()
    {
        foreach (GameObject page in allPages)
        {
            page.SetActive(false);
        }
    }

    public void OnClickGamePlayTab()
    {
        DisableAllPages();
        gameplayPage.SetActive(true);
    }

    public void OnClickGraphicsTab()
    {
        DisableAllPages();
        graphicsPage.SetActive(true);
    }

    public void OnClickSoundTab()
    {
        DisableAllPages();
        soundPage.SetActive(true);
    }

    public void OnClickBack()
    {
        UI_WidgetManager.Instance.TryLoadWidget("MainMenu","MainMenu");
        UI_WidgetManager.Instance.TryUnloadWidget("SettingsMenu");
    }
}
