using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class POIScript : MonoBehaviour
{
    public TextAsset inkAsset;

    private void OnMouseDown()
    {
        InteractionManagerScript.S.interaction = new Story(inkAsset.text); //make Unity obj out of our Ink file
        InteractionManagerScript.S.BeginDialogue();
    }
    
}
