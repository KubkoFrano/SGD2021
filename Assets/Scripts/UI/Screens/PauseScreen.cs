using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : ScreenBase
{
    public override void Show()
    {
        base.Show();
        App.gameManager.SetGameState(GameState.pause);
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public override void Hide()
    {
        App.gameManager.SetGameState(GameState.game);
        base.Hide();
    }

    public void BackButtonClicked()
    {
        App.gameManager.SetGameState(GameState.game);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        App.screenManager.Hide<InGameScreen>();
        App.gameManager.StartSceneUnloading(App.gameManager.GetLevelScene());
        App.playerManager.DeleteAllPlayers();
        App.kingOfTheHill.DeleteCoins();
        Hide();
    }
}