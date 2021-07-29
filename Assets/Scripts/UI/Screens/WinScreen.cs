using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreen : ScreenBase
{
    [SerializeField] TextMeshProUGUI winnerText;

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
        App.gameManager.SetGameState(GameState.menu);
        App.screenManager.Show<MenuScreen>();
        Hide();
    }

    public void SetWinnerText(int winner)
    {
        winnerText.text = "Winner: " + winner;
    }
}