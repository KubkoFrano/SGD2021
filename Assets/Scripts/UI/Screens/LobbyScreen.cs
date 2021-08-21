using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScreen : ScreenBase
{
    public override void Show()
    {
        base.Show();
        App.playerManager.SetJoining(true);
    }

    public override void Hide()
    {
        base.Hide();
        App.playerManager.SetJoining(false);
    }

    public void BackButtonClicked()
    {
        App.gameManager.SetGameState(GameState.menu);
        App.screenManager.Show<MenuScreen>();
        App.playerManager.DeleteAllPlayers();
        Hide();
    }

    public void PlayButtonClicked()
    {
        if (!(App.playerManager.GetPlayerCount() == 2 || App.playerManager.GetPlayerCount() == 4))
            return;

        App.screenManager.Show<LoadingScreen>();
        App.gameManager.StartSceneUnloading("MenuScene");
        Hide();
    }
}