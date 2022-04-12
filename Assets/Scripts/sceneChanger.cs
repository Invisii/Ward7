using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{
    public string targetScene;

    private void OnMouseDown()
    {
        SceneManager.LoadScene(targetScene);
    }
}
