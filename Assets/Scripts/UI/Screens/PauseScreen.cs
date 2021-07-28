using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : ScreenBase
{
    public void BackButtonClicked()
    {
        App.gameManager.SetGameState(GameState.game);
        Hide();
    }

    public void SettingsButtonClicked()
    {
        App.screenManager.Show<SettingsScreen>();
        Hide();
    }

    public void MenuButtonClicked()
    {
        App.gameManager.SetGameState(GameState.menu);
        App.screenManager.Show<MenuScreen>();
        Hide();
    }
}