using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownScreen : ScreenBase
{
    [SerializeField] int seconds;
    [SerializeField] GameObject twoPlayers;
    [SerializeField] GameObject fourPlayers;
    [SerializeField] TextMeshProUGUI counter;

    public override void Show()
    {
        base.Show();
        if (App.playerManager.GetPlayerCount() == 2)
            twoPlayers.SetActive(true);
        else
            fourPlayers.SetActive(true);

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        counter.gameObject.SetActive(true);

        for (int i = seconds; i > 0; i--)
        {
            counter.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        counter.text = "Go!";
        twoPlayers.SetActive(false);
        fourPlayers.SetActive(false);

        App.screenManager.Show<InGameScreen>();
        App.inGameScreen.ResetScores();
        App.inGameScreen.SetPlayerNumber(App.playerManager.GetPlayerCount());
        App.inGameScreen.ResetBirds();
        App.timer.StartTimer();
        App.gameManager.SetGameState(GameState.game);

        yield return new WaitForSeconds(1);

        counter.gameObject.SetActive(false);
        Hide();
    }
}