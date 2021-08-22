using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LoadingScreen : ScreenBase
{
    [SerializeField] VideoClip[] videos;

    VideoPlayer videoPlayer;

    int index = 0;

    private void Awake()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
    }

    private void OnEnable()
    {
        PlayNext();
    }

    public void PlayNext()
    {
        if (index == videos.Length)
        {
            index = 0;
            Load();
            return;
        }
        videoPlayer.clip = videos[index];
        videoPlayer.Play();
        index++;    
    }

    public void Load()
    {
        Time.timeScale = 1;
        App.gameManager.StartSceneLoading(App.gameManager.GetLevelScene());
        App.playerManager.InitPlayers();
        App.playerManager.SetupCameras();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


        App.playerManager.CreatePlayerTransforms();
        App.playerManager.CreatePlayerScores();

        App.screenManager.Show<CountDownScreen>();
        Hide();
    }
}