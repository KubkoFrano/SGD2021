using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreen : ScreenBase
{
    public override void Show()
    {
        gameObject.SetActive(true);
    }

    private void Awake()
    {
        App.winScreen = this;
    }

    public void LobbyButttonClicked()
    {
        App.gameManager.SetGameState(GameState.lobby);
        App.screenManager.Show<LobbyScreen>();
        Hide();
    }

    public void MenuButtonClicked()
    {
        App.podium.DeleteNames();
        App.gameManager.SetGameState(GameState.menu);
        firstButton.SetActive(false);
        App.audioManager.Stop("WinMusic");
        App.screenManager.Show<MenuScreen>();
        App.playerManager.DeleteAllPlayers();
        App.gameManager.StartSceneUnloading("WinScene");
        App.gameManager.StartSceneLoading("MenuScene");
        Hide();
    }

    public void ShowButton()
    {
        if (firstButton.activeSelf == false)
        {
            firstButton.SetActive(true);
            SetSelectedButton();
            App.audioManager.Play("WinSound");
        }
    }
}