using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfTheHill : MonoBehaviour
{
    [SerializeField] float timeSlowPercentage;

    PlayerScore[] playerScores;

    private void Awake()
    {
        App.kingOfTheHill = this;
    }

    private void Start()
    {
        CreatePlayerArray();
        StartWatchingPlayers();
    }

    public void EndMinigame()
    {
        StopCoroutine(WatchPlayers());
        App.gameManager.SetGameState(GameState.menu);
        App.gameManager.StartSceneUnloading(App.gameManager.GetLevelScene());
        App.screenManager.Hide<InGameScreen>();
        App.screenManager.Show<WinScreen>();
        App.playerManager.DeleteAllPlayers();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        App.winScreen.SetWinnerText(GetWinner());
    }

    public void UpdateScore(int index, int score)
    {
        App.inGameScreen.UpdateScore(index, score);
    }

    public void StartWatchingPlayers()
    {
        StartCoroutine(WatchPlayers());
    }

    void CreatePlayerArray()
    {
        List<GameObject> tempList = App.playerManager.GetPlayerList();

        playerScores = new PlayerScore[tempList.Count];

        for (int i = 0; i < tempList.Count; i++)
        {
            playerScores[i] = tempList[i].GetComponent<PlayerScore>();
            playerScores[i].SetScoreIndex(i);
        }
    }

    IEnumerator WatchPlayers()
    {
        int index = 0;
        PlayerScore highestPlayer = playerScores[1];
        highestPlayer.StartScoring();

        while (true)
        {
            if (playerScores[index].transform.position.y > highestPlayer.transform.position.y && playerScores[index] != highestPlayer)
            {
                highestPlayer.StopAllCoroutines();
                highestPlayer = playerScores[index];
                highestPlayer.StartScoring();
            }

            index++;
            if (index == playerScores.Length)
                index = 0;

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SlowDownTime()
    {
        while (Time.timeScale > 0.01f)
        {
            Time.timeScale = Mathf.Lerp(1, 0, timeSlowPercentage / 100);

            new WaitForEndOfFrame();
        }

        Time.timeScale = 0;
        yield return 0;
    }

    int GetWinner()
    {
        PlayerScore tempWinner = playerScores[0];

        foreach (PlayerScore player in playerScores)
        {
            if (player.GetScore() > tempWinner.GetScore())
            {
                tempWinner = player;
            }
        }

        return tempWinner.GetScoreIndex();
    }
}