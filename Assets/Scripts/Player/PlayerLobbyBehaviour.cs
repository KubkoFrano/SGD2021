using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLobbyBehaviour : MonoBehaviour
{
    [Header("Do not touch")]
    public GameObject camera;
    public GameObject cinemachine;
    public ThirdPersonMovement playerMovement;

    int characterIndex;

    private void Start()
    {
        App.playerManager.JoinPlayer(this.gameObject);
        characterIndex = App.characterManager.AssignCharacter();
    }

    public void Leave()
    {
        App.playerManager.RemovePlayer(this.gameObject);
        App.characterManager.FreeCharacter(characterIndex);
        Destroy(this.gameObject);
    }

    public void InitPlayer()
    {
        playerMovement.enabled = true;
        camera.SetActive(true);
        cinemachine.SetActive(true);
    }

    public void SetCharasterIndex(int index)
    {
        this.characterIndex = index;
    }

    public int GetCharacterIndex()
    {
        return characterIndex;
    }
}