using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoLogic : MonoBehaviour
{
    public TextMeshProUGUI titleText; // will lose reference upon entering new scene
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // will preserve this object between scenes
    }

    public void ConsoleTest()
    {
        Debug.Log("ConsoleTest Invoked");
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
}
