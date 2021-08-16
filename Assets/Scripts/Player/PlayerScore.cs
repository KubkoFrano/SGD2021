using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [Header("Altitude scoring")]
    [SerializeField] int scoreToAdd;
    [SerializeField] int secondsBetweenScores;

    [Header("Coin scoring")]
    [SerializeField] int coinValue;

    [Header("Do not touch")]
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] ParticleSystem scoreParticles;

    int score;
    int scoreIndex;
    bool isScoring;

    //Coin scoring

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            AddScore(coinValue);
        }
    }

    void AddScore(int scoreToAdd)
    {
        score += scoreToAdd; 
        scoreParticles.Play();
        App.kingOfTheHill.UpdateScore(scoreIndex, score);
    }

    //Altitude scoring

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
            score += scoreToAdd;
            scoreParticles.Play();
            App.kingOfTheHill.UpdateScore(scoreIndex, score);
        }
    }
}