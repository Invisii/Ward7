using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class FocusedPOI : MonoBehaviour
{
    
    public TextAsset inkAsset;
    public Material hoverMat;
    [Range(0f, 100f)]
    public float thickness; 
        
    private Material defMat;
    private SpriteRenderer spr;
    
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        defMat = GetComponent<SpriteRenderer>().material;
    }
    private void OnMouseEnter()
    {
        if (!InteractionManagerScript.S.activeStory)
        {
            spr.material = hoverMat;
            spr.material.SetFloat("_Thicc", thickness);   
        }
    }
    
    private void OnMouseExit()
    {
        spr.material = defMat; 
    }
    
    private void OnMouseDown()
    {
        if (!InteractionManagerScript.S.activeStory)
        {
            InteractionManagerScript.S.interaction = new Story(inkAsset.text); //make Unity obj out of our Ink file
            InteractionManagerScript.S.BeginDialogue();   
        }
    }
}
