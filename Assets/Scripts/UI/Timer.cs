using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    [SerializeField] int timeInSeconds;

    int timer;

    TextMeshProUGUI text;

    private void Awake()
    {
        App.timer = this;
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        timer = timeInSeconds;
        RefreshTimer();
    }

    IEnumerator GameTimer()
    {
        timer = timeInSeconds;
        RefreshTimer();

        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
            RefreshTimer();
        }

        App.kingOfTheHill.EndMinigame();
    }

    void RefreshTimer()
    {
        int minutes = timer / 60;
        int seconds = timer % 60;

        text.text = FixTime(minutes) + " : " + FixTime(seconds);
    }

    string FixTime(int time)
    {
        if (time < 10)
            return "0" + time;
        else
            return time.ToString();
    }

    public void StartTimer()
    {
        StartCoroutine(GameTimer());
    }

    public void StopTimer()
    {
        StopCoroutine(GameTimer());
    }
}