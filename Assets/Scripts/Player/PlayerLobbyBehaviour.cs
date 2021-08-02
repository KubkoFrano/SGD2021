using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class PlayerLobbyBehaviour : MonoBehaviour
{
    [Header("Do not touch")]
    public GameObject camera;
    public GameObject cinemachine;
    public ThirdPersonMovement playerMovement;

    PlayerInput playerInput;

    int characterIndex;

    private void Start()
    {
        App.playerManager.JoinPlayer(this.gameObject);
        characterIndex = App.characterManager.AssignCharacter();
        playerInput = GetComponent<PlayerInput>();
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
        cinemachine.GetComponent<CinemachineInputProvider>().PlayerIndex = playerInput.playerIndex;
    }

    public void SetCharasterIndex(int index)
    {
        this.characterIndex = index;
    }

    public int GetCharacterIndex()
    {
        return characterIndex;
    }

    public void SetMasks(LayerMask mask, int layer)
    {
        cinemachine.layer = layer;
        camera.GetComponent<Camera>().cullingMask = mask;
    }

    public void SetViewport(Viewport viewport)
    {
        camera.GetComponent<Camera>().rect = new Rect(viewport.X, viewport.Y, viewport.W, viewport.H);
    }
}