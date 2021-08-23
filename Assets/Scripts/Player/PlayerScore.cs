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
    [SerializeField] float coinParticlesInterval;

    [Header("Do not touch")]
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] ParticleSystem scoreParticles;

    int score;
    int scoreIndex;
    bool isScoring;

    public void AddScore()
    {
        score += coinValue;
        scoreParticles.Play();
        App.kingOfTheHill.UpdateScore(scoreIndex, score);
        App.audioManager.Play("Coin");
    }

    public bool SubtractScore()
    {
        if (score >= 0 + coinValue)
        {
            score -= coinValue;
            App.kingOfTheHill.UpdateScore(scoreIndex, score);
            return true;
        }
        else
            return false;
    }

    public void AddParticularScore(int score)
    {
        StartCoroutine(AddMoreCoins(score));
    }

    IEnumerator AddMoreCoins(int scoreToAdd)
    {
        for (; scoreToAdd > 0; scoreToAdd--)
        {
            score++;
            scoreParticles.Play();
            App.kingOfTheHill.UpdateScore(scoreIndex, this.score);
            App.audioManager.Play("Coin");
            yield return new WaitForSeconds(coinParticlesInterval);
        }
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