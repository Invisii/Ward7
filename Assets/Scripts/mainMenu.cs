using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public static GameObject startB, continueB, sceneMusic;
    
    public static mainMenu M;

    private void Awake()
    {
        if (M == null)
        {
            M = this;
        }
    }

    public void startGame()
    {
        SceneManager.LoadScene("Intro-01");
    }

    public void continueGame()
    {
        SceneManager.UnloadSceneAsync("Menu");
        sceneMusic.GetComponent<AudioSource>().Play();
    }
    
    public void quitGame()
    {
        Application.Quit();
    }

    public void settings()
    {
        Vector3 startBPos = startB.transform.position;
        startB.transform.position = new Vector3(startBPos.x, startBPos.y + 1.3f, startBPos.z);
        continueB.SetActive(true);
        sceneMusic.GetComponent<AudioSource>().Pause();
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }
    
    
}
