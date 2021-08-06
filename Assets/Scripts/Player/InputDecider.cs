using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDecider : MonoBehaviour
{
    ThirdPersonMovement playerMovement;
    PlayerLobbyBehaviour lobbyBehaviour;
    PlayerAbilities playerAbilities;
    Hammer hammer;

    bool hasHammer = false;

    private void Start()
    {
        playerMovement = GetComponent<ThirdPersonMovement>();
        lobbyBehaviour = GetComponent<PlayerLobbyBehaviour>();
        playerAbilities = GetComponent<PlayerAbilities>();
        hammer = GetComponent<Hammer>();
    }

    public void JumpButton(InputAction.CallbackContext context)
    {
        if (App.gameManager.CompareGameState(GameState.game))
        {
            playerMovement.Jump(context);
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (context.canceled || context.performed)
            return;

        if (App.gameManager.CompareGameState(GameState.game))
        {
            App.screenManager.Show<PauseScreen>();
            App.gameManager.SetCurrentController(this.gameObject);
            Cursor.lockState = CursorLockMode.None;
        }
        else if (App.gameManager.CompareGameState(GameState.pause) && App.gameManager.CompareCurrentController(this.gameObject))
        {
            App.screenManager.Hide<PauseScreen>();
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ShootButton(InputAction.CallbackContext context)
    {
        if (context.canceled || context.performed)
            return;

        if (hasHammer)
            hammer.Activate(context);
        else
            playerAbilities.Punch(context);
    }

    public void SetHammer(bool value)
    {
        hasHammer = value;
    }
}