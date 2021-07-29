using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameScreen : ScreenBase
{
    [SerializeField] TextMeshProUGUI[] texts;

    public void UpdateScore(int index, int score)
    {
        texts[index].text = score.ToString();
    }
}