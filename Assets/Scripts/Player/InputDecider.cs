using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDecider : MonoBehaviour
{
    ThirdPersonMovement playerMovement;
    PlayerLobbyBehaviour lobbyBehaviour;

    private void Start()
    {
        playerMovement = GetComponent<ThirdPersonMovement>();
        lobbyBehaviour = GetComponent<PlayerLobbyBehaviour>();
    }

    public void JumpButton(InputAction.CallbackContext context)
    {
        if (context.canceled || context.performed)
            return;

        if (App.gameManager.CompareGameState(GameState.lobby))
        {
            lobbyBehaviour.Leave();
        }
        else if (App.gameManager.CompareGameState(GameState.game))
        {
            playerMovement.Move(context);
        }
    }
}