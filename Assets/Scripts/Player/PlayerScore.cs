using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] int scoreToAdd;
    [SerializeField] int secondsBetweenScores;
    [SerializeField] Color32 baseColor;
    [SerializeField] Color32 scoringColor;


    [Header("Do not touch")]
    [SerializeField] MeshRenderer meshRenderer;

    int score;
    int scoreIndex;
    bool isScoring;

    private void Start()
    {
        meshRenderer.material.color = baseColor;
    }

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
        if (!isScoring)
        {
            StartCoroutine(Scoring());
            isScoring = true;
        }
    }

    public void StopScoring()
    {
        isScoring = false;
        meshRenderer.material.color = baseColor;
        StopAllCoroutines();
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
            meshRenderer.material.color = scoringColor;
            score += scoreToAdd;
            App.kingOfTheHill.UpdateScore(scoreIndex, score);
        }
    }
}