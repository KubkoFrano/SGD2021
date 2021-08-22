using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class LaunchScreen : ScreenBase
{
    [SerializeField] VideoPlayer videoPlayer;

    public void Launch()
    {
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();

        StartCoroutine(DelayLaunch());
    }

    IEnumerator DelayLaunch()
    {
        yield return new WaitForSeconds((float) videoPlayer.clip.length);
        App.screenManager.Show<MenuScreen>();
        Hide();
    }
}