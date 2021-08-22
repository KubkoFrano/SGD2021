using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LaunchScreen : ScreenBase
{
    public void Launch()
    {
        App.screenManager.Show<MenuScreen>();
        Hide();
    }
}