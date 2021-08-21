using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutScreen : ScreenBase
{
    public void BackButtonClicked()
    {
        App.screenManager.Show<MenuScreen>();
        App.menuPostProcessing.SwitchPostProcessing(false);
        Hide();
    }
}
