using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchScreen : ScreenBase
{ 
    public void Launch()
    {
        App.screenManager.Show<MenuScreen>();
        Hide();
    }
}