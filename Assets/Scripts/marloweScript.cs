using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class marloweScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        CutSceneManager.C.marloweClicked = true;
    }
}
