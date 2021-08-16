using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class PlayerLobbyBehaviour : MonoBehaviour
{
    [SerializeField] GameObject[] characters;

    [Header("Do not touch")]
    public GameObject camera;
    public GameObject cinemachine;
    public ThirdPersonMovement playerMovement;

    [SerializeField] GameObject[] hands;
    [SerializeField] GameObject hammer;
    [SerializeField] GameObject pistol;

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

    public void SetCharacter(int index)
    {
        characters[index].SetActive(true);

        Animator anim = characters[index].GetComponent<Animator>();
        playerMovement.SetMovementAnim(anim);

        pistol.transform.parent = hands[index].transform;
        pistol.transform.localPosition = new Vector3(-1.28f, 1, -0.63f);
        pistol.transform.localRotation = Quaternion.Euler(94.31f, 0, 120);

        hammer.transform.parent = hands[index].transform;
        hammer.transform.localPosition = new Vector3(-1.28f, 1, -0.63f);
        hammer.transform.localRotation = Quaternion.Euler(94.31f, 0, 120);
    }
}