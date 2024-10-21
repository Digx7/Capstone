using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCardWidget : Widget
{
    public void OnClickStart()
    {
        UI_WidgetManager.Instance.TryLoadWidget("MainMenu","MainMenu");
        UI_WidgetManager.Instance.TryUnloadWidget("TitleCard");
    }
}
