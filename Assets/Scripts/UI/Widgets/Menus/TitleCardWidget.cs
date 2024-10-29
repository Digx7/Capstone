using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCardWidget : MenuWidget
{
    public void OnClickStart()
    {
        UI_WidgetManager.Instance.TryUnloadWidget("TitleCard");
        UI_WidgetManager.Instance.TryLoadWidget("MainMenu","MainMenu");
    }
}
