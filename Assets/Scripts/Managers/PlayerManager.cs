using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    PlayerInputManager inputManager;

    public GameObject[] players;
    int playerCount;

    private void Start()
    {
        App.playerManager = this;
        inputManager = GetComponent<PlayerInputManager>();
        SetJoining(false);

        players = new GameObject[] { null, null, null, null };
    }

    public void SetJoining(bool value)
    {
        if (value)
            inputManager.EnableJoining();
        else
            inputManager.DisableJoining();
    }

    public void JoinPlayer(GameObject player)
    {
        for (int i = 0; i < 4; i++)
        {
            if (players[i] == null)
            {
                players[i] = player;
                playerCount++;
                break;
            }
        }
    }

    public void RemovePlayer(GameObject player)
    {
        for (int i = 0; i < 4; i++)
        {
            if (players[i] == player)
            {
                players[i] = null;
                playerCount--;
                break;
            }
        }
    }

    public void InitPlayers()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<PlayerLobbyBehaviour>().InitPlayer();
        }
    }
}