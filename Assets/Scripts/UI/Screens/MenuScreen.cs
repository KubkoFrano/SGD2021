using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : ScreenBase
{
    public void PlayButtonClicked()
    {
        App.gameManager.SetGameState(GameState.lobby);
        App.screenManager.Show<LobbyScreen>();
        App.menuPostProcessing.SwitchPostProcessing(true);
        App.characterManager.ResetCharacters();
        Hide();
    }

    public void SettingsButtonClicked()
    {
        App.screenManager.Show<SettingsScreen>();
        App.menuPostProcessing.SwitchPostProcessing(true);
        Hide();
    }

    public void AboutButtonClicked()
    {
        App.screenManager.Show<AboutScreen>();
        App.menuPostProcessing.SwitchPostProcessing(true);
        Hide();
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
    }
}