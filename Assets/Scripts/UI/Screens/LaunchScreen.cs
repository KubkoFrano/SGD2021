using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LaunchScreen : ScreenBase
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Image image;

    public void Launch()
    {
        //image.enabled = false;
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