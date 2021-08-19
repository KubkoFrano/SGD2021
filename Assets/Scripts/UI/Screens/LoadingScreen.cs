using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : ScreenBase
{
    [SerializeField] float timeBetweenScreens;
    [SerializeField] Sprite[] sprites;

    Image image;

    private void Awake()
    {
        image = GetComponentInChildren<Image>();
    }

    private void OnEnable()
    {
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        if (sprites.Length > 0)
        {
            foreach (Sprite sprite in sprites)
            {
                image.sprite = sprite;
                yield return new WaitForSeconds(timeBetweenScreens);
            }
        }
        
        Time.timeScale = 1;
        App.gameManager.StartSceneLoading(App.gameManager.GetLevelScene());
        App.screenManager.Show<InGameScreen>();
        App.playerManager.InitPlayers();
        App.playerManager.SetupCameras();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        App.timer.StartTimer();
        App.inGameScreen.ResetScores();
        App.inGameScreen.SetPlayerNumber(App.playerManager.GetPlayerCount());
        App.playerManager.CreatePlayerTransforms();
        App.playerManager.CreatePlayerScores();
        App.gameManager.SetGameState(GameState.game);
        App.inGameScreen.ResetBirds();
        Hide();
    }
}