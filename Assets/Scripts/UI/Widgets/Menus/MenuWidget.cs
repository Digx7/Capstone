using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuWidget : Widget
{
    public override void SetUp()
    {
        Cursor.visible = true;
    }

    public override void Teardown()
    {
        Cursor.visible = false;
    }
}