using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        InteractionManagerScript.S.unFocusObj();
    }
}
