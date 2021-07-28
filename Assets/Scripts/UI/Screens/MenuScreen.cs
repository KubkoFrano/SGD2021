using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : ScreenBase
{
    public void PlayButtonClicked()
    {
        App.gameManager.SetGameState(GameState.lobby);
        App.screenManager.Show<LobbyScreen>();
        Hide();
    }

    public void SettingsButtonClicked()
    {
        App.screenManager.Show<SettingsScreen>();
        Hide();
    }

    public void AboutButtonClicked()
    {
        App.screenManager.Show<AboutScreen>();
        Hide();
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
    }
}