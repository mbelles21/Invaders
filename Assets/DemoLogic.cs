using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoLogic : MonoBehaviour
{
    // public TextMeshProUGUI titleText; // will lose reference upon entering new scene
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // will preserve this object between scenes
    }

    private void Start()
    {
        Player.OnPlayerDied += PlayerOnOnPlayerDied;
        EnemyMovement.OnAllDied += PlayerOnOnPlayerDied;
    }

    private void OnDestroy()
    {
        Player.OnPlayerDied -= PlayerOnOnPlayerDied;
        EnemyMovement.OnAllDied -= PlayerOnOnPlayerDied;
    }

    void PlayerOnOnPlayerDied()
    {
        Invoke("StartCredits", 2f);
    }

    public void StartGame()
    {
        StartCoroutine(FindPlayer()); // find player object when game starts 
    }

    IEnumerator FindPlayer()
    {
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game");
        while (!asyncOperation.isDone) // while waiting for LoadScene to finish
        {
            yield return null; // wait until next frame
        }
        
        GameObject playerObj = GameObject.Find("Player");
        Debug.Log(playerObj);
    }

    public void StartCredits()
    {
        StartCoroutine(GetCredits());
    }

    IEnumerator GetCredits()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Credits");
        while (!asyncOperation.isDone)
            yield return null;

        Invoke("StartMenu", 5f);
    }

    public void StartMenu()
    {
        StartCoroutine(GetMainMenu());
    }

    IEnumerator GetMainMenu()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainMenu");
        while (!asyncOperation.isDone)
            yield return null;
    }
}
