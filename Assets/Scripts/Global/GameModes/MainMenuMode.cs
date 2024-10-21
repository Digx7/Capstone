using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMode : GameMode
{
    public override void Setup()
    {
        UI_WidgetManager.Instance.TryLoadWidget("TitleCard","TitleCard");

        base.Setup();
    }

    public override void TearDown()
    {


        base.TearDown();
    }
}
