using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] int scoreToAdd;
    [SerializeField] int secondsBetweenScores;

    int score;
    int scoreIndex;

    public int GetScore()
    {
        return score;
    }

    public int GetScoreIndex()
    {
        return scoreIndex;
    }

    public void StartScoring()
    {
        StartCoroutine(Scoring());
    }

    public void StopScoring()
    {
        StopCoroutine(Scoring()); Debug.Log(scoreIndex + " stopped");
    }

    public void SetScoreIndex(int index)
    {
        scoreIndex = index;
    }

    IEnumerator Scoring()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenScores);
            score += scoreToAdd; Debug.Log(scoreIndex + " score added");
            App.kingOfTheHill.UpdateScore(scoreIndex, score);
        }
    }
}