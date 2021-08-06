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

    Transform[] playerTransforms;

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
            player?.GetComponent<PlayerLobbyBehaviour>().InitPlayer();
        }
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
        List<GameObject> tempPlayerList = GetPlayerList();

        for (int i = 0; i < tempPlayerList.Count; i++)
        {
            tempPlayerList[i]?.GetComponent<PlayerLobbyBehaviour>().SetMasks(cameraMasks[i], cameraLayers[i]);

            if (playerCount == 2)
            {
                tempPlayerList[i]?.GetComponent<PlayerLobbyBehaviour>().SetViewport(viewports2[i]);
            }
            else if (playerCount == 4)
            {
                tempPlayerList[i]?.GetComponent<PlayerLobbyBehaviour>().SetViewport(viewports4[i]);
            }
        }
    }

    public void DeleteAllPlayers()
    {
        foreach (GameObject player in players)
        {
            player?.GetComponent<PlayerLobbyBehaviour>().Leave();
        }
    }

    public List<GameObject> GetPlayerList()
    {
        List<GameObject> tempList = new List<GameObject>();
        foreach (GameObject player in players)
        {
            if (player)
                tempList.Add(player);
        }

        return tempList;
    }

    public void CreatePlayerTransforms()
    {
        List<GameObject> playerList = GetPlayerList();
        playerTransforms = new Transform[playerList.Count];

        for (int i = 0; i < playerList.Count; i++)
        {
            playerTransforms[i] = playerList[i].transform;
        }
    }

    public Transform[] GetPlayerTransforms()
    {
        return playerTransforms;
    }

    public void SetSpawnPositions(Vector3[] positions)
    {
        spawnPositions = positions;
        //spawnPositions = new Vector3[] { new Vector3(2, 20, 0), new Vector3(-2, 20, 0), new Vector3(0, 20, 2), new Vector3(0, 20, -2) };
    }

    public Vector3[] GetSpawnPositions()
    {
        return spawnPositions;
    }
}