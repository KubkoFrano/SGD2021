using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameScreen : ScreenBase
{
    [SerializeField] TextMeshProUGUI[] texts;

    private void Awake()
    {
        App.inGameScreen = this;
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

        }
        else
        {
            texts[2].gameObject.SetActive(true);
            texts[3].gameObject.SetActive(true);
        }
    }
}