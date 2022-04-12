using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorScript : MonoBehaviour
{
    public string targetScene;
    const string hallway = "Hallway";

    private void OnMouseDown()
    {
        SceneManager.LoadScene(sceneName: targetScene);
    }

    public void hallwayButton()
    {
        SceneManager.LoadScene(sceneName: hallway);
    }
}
