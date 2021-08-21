using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Podium : MonoBehaviour
{
    [SerializeField] Winner[] players;

    private void Start()
    {
        AssignSkins();
        StartCoroutine(ManageRising());
    }

    IEnumerator ManageRising()
    {
        yield return 0;
    }

    void AssignSkins()
    {
        PlayerScore[] scores = App.playerManager.GetPlayerScores();
        List<PlayerScore> scoreList;
        List<PlayerScore> finalScores = new List<PlayerScore>();

        scoreList = scores.ToList<PlayerScore>();

        while (scoreList.Count > 0)
        {
            PlayerScore best = scoreList[0];

            foreach (PlayerScore s in scoreList)
            {
                if (s.GetScore() > best.GetScore())
                    best = s;
            }

            RefreshList(scoreList, best);
            finalScores.Add(best);
        }

        for (int i = 0; i < finalScores.Count; i++)
        {
            players[i].SetPlayer(finalScores[i].GetScoreIndex());

            int hexToRise = i;

            foreach (PlayerScore p in finalScores)
            {
                if (finalScores[i].GetScore() == p.GetScore())
                {
                    hexToRise = finalScores.IndexOf(p);
                    break;
                }    
            }

            players[i].RiseHex(hexToRise);
        }
    }

    List<PlayerScore> RefreshList(List<PlayerScore> list, PlayerScore toRemove)
    {
        list.Remove(toRemove);

        List<PlayerScore> temp = new List<PlayerScore>();

        foreach (PlayerScore s in list)
        {
            if (s)
                temp.Add(s);
        }

        return temp;
    }
}