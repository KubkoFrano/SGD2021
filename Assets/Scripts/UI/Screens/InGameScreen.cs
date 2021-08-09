using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameScreen : ScreenBase
{
    [SerializeField] TextMeshProUGUI[] texts;
    [SerializeField] Slider[] sliders;
    [SerializeField] Slider[] birdSliders;

    private void Awake()
    {
        App.inGameScreen = this;
    }

    public void ToggleBirdSlider(int index, bool value)
    {
        birdSliders[index].gameObject.SetActive(value);
    }

    public void UpdateBird(int index, float value)
    {
        birdSliders[index].value = value;
    }

    public void ResetBirds()
    {
        foreach (Slider slider in birdSliders)
        {
            slider.value = 0;
            slider.gameObject.SetActive(false);
        }
    }

    public void UpdateBaloon(int index, float value)
    {
        sliders[index].value = value;
    }

    public void UpdateScore(int index, int score)
    {
        texts[index].text = score.ToString();
    }

    public void ResetScores()
    {
        foreach (TextMeshProUGUI text in texts)
        {
            text.text = "0";
        }
    }

    public void SetPlayerNumber(int num)
    {
        if (num == 2)
        {
            texts[2].gameObject.SetActive(false);
            texts[3].gameObject.SetActive(false);
            sliders[2].gameObject.SetActive(false);
            sliders[3].gameObject.SetActive(false);

        }
        else
        {
            texts[2].gameObject.SetActive(true);
            texts[3].gameObject.SetActive(true);
            sliders[2].gameObject.SetActive(true);
            sliders[3].gameObject.SetActive(true);
        }
    }
}