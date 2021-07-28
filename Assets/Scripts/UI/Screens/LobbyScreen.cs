using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScreen : ScreenBase
{
    public void BackButtonClicked()
    {
        App.gameManager.SetGameState(GameState.menu);
        App.screenManager.Show<MenuScreen>();
        Hide();
    }

    public void PlayButtonClicked()
    {
        App.gameManager.SetGameState(GameState.game);
        App.screenManager.Show<InGameScreen>();
        Hide();
    }
}