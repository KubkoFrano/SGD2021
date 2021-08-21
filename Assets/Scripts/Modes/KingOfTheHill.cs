using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingOfTheHill : MonoBehaviour
{
    [SerializeField] float timeSlowPercentage;

    PlayerScore[] playerScores;
    ThirdPersonMovement[] movements;
    List<CoinSpawnBehaviour> coins = new List<CoinSpawnBehaviour>();

    private void Awake()
    {
        App.kingOfTheHill = this;
    }

    private void Start()
    {
        CreatePlayerArray();
        //StartWatchingPlayers();
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
        App.gameManager.StartSceneLoading("WinScene");
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
        movements = new ThirdPersonMovement[tempList.Count];

        for (int i = 0; i < tempList.Count; i++)
        {
            playerScores[i] = tempList[i].GetComponent<PlayerScore>();
            playerScores[i].SetScoreIndex(i);

            movements[i] = tempList[i].GetComponent<ThirdPersonMovement>();
            movements[i].SetBaloonIndex(i);
        }
    }

    IEnumerator WatchPlayers()
    {
        int index = 0;
        PlayerScore highestPlayer = playerScores[1];
        highestPlayer.StartScoring();

        while (true)
        {
            for (index = 0; index < playerScores.Length; index++)
            {
                if (Mathf.RoundToInt(playerScores[index].transform.position.y) > Mathf.RoundToInt(highestPlayer.transform.position.y) && playerScores[index] != highestPlayer)
                {
                    highestPlayer.StopScoring();
                    highestPlayer = playerScores[index];
                    highestPlayer.StartScoring();
                }
                else if (Mathf.RoundToInt(playerScores[index].transform.position.y) == Mathf.RoundToInt(highestPlayer.transform.position.y) && playerScores[index] != highestPlayer)
                {
                    highestPlayer.StopScoring();
                }
            }

            highestPlayer.StartScoring();
            /*
            index++;
            if (index == playerScores.Length)
                index = 0;*/

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

    public void AddCoin(CoinSpawnBehaviour coin)
    {
        coins.Add(coin);
    }

    public void RemoveCoin(CoinSpawnBehaviour coin)
    {
        coins.Remove(coin);
    }

    public void DeleteCoins()
    {
        foreach (CoinSpawnBehaviour coin in coins)
        {
            Destroy(coin.gameObject);
        }
    }
}