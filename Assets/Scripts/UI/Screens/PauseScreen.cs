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
        App.audioManager.PauseSoundTrack();
    }

    public override void Hide()
    {
        App.audioManager.ContinueSoundtrack();
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
        App.gameManager.SetGameState(GameState.settings);
        Hide();
    }

    public void MenuButtonClicked()
    {
        App.audioManager.StopSoundTrack();
        App.gameManager.StartSceneLoading("MenuScene");
        App.gameManager.SetGameState(GameState.menu);
        App.screenManager.Show<MenuScreen>();
        App.screenManager.Hide<InGameScreen>();
        App.gameManager.StartSceneUnloading(App.gameManager.GetLevelScene());
        App.playerManager.DeleteAllPlayers();
        App.kingOfTheHill.DeleteCoins();
        Hide();
    }
}