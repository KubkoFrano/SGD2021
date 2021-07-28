using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] LayerMask[] cameraMasks;
    [SerializeField] int[] cameraLayers;
    [SerializeField] Viewport[] viewports2 = new Viewport[2];
    [SerializeField] Viewport[] viewports4 = new Viewport[4];

    PlayerInputManager inputManager;

    GameObject[] players;
    int playerCount = 0;

    Vector3[] spawnPositions;

    private void Start()
    {
        App.playerManager = this;
        inputManager = GetComponent<PlayerInputManager>();
        SetJoining(false);

        players = new GameObject[] { null, null, null, null };

        //Temp
        spawnPositions = new Vector3[] { new Vector3(2, 2, 0), new Vector3(-2, 2, 0), new Vector3(0, 2, 2), new Vector3(0, 2, -2) };
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
            player?.GetComponent<PlayerLobbyBehaviour>().InitPlayer();
        }

        SetPlayerPositions();
    }

    public void CreatePlayerPositions(Vector3[] positions)
    {
        spawnPositions = positions;
    }

    public void SetPlayerPositions()
    {
        for (int i = 0; i < playerCount; i++)
        {
            players[i].transform.position = spawnPositions[i];
        }
    }

    public int GetPlayerCount()
    {
        return playerCount;
    }

    public void SetupCameras()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i]?.GetComponent<PlayerLobbyBehaviour>().SetMasks(cameraMasks[i], cameraLayers[i]);

            if (playerCount == 2)
            {
                players[i]?.GetComponent<PlayerLobbyBehaviour>().SetViewport(viewports2[i]);
            }
            else if (playerCount == 4)
            {
                players[i]?.GetComponent<PlayerLobbyBehaviour>().SetViewport(viewports4[i]);
            }
        }
    }

    public void DeleteAllPlayers()
    {
        foreach (GameObject player in players)
        {
            GetComponent<PlayerLobbyBehaviour>().Leave();
        }
    }
}