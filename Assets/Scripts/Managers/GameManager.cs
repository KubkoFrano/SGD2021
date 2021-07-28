using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameState gameState = GameState.menu;

    private void Start()
    {
        App.gameManager = this;
        StartCoroutine(LoadSelectedScene("UIScene"));
    }

    IEnumerator LoadSelectedScene(string sceneName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!loading.isDone)
        {
            yield return null;
        }

    }

    IEnumerator UnloadSelectedScene(string sceneName)
    {
        AsyncOperation loading = SceneManager.UnloadSceneAsync(sceneName);
        while (!loading.isDone)
        {
            yield return null;
        }
    }

    public void StartSceneLoading(string sceneName)
    {
        StartCoroutine(LoadSelectedScene(sceneName));
    }

    public void StartSceneUnloading(string sceneName)
    {
        StartCoroutine(UnloadSelectedScene(sceneName));
    }

    public bool CompareGameState(GameState gameState)
    {
        return this.gameState == gameState;
    }

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;
    }
}