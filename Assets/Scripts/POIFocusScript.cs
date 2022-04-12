using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class POIFocusScript : MonoBehaviour
{
    public TextAsset inkAsset;
    public GameObject focus;
    public Material hoverMat;
    [Range(0f, 100f)]
    public float thickness; 
        
    private Material defMat;
    private SpriteRenderer spr;
    
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        defMat = GetComponent<SpriteRenderer>().material;
        if (hoverMat == null)
        {
            hoverMat = Resources.Load<Material>("/Assets/Renderer/Shine_Material.mat");
        }
    }
    
    private void OnMouseEnter()
    {
        if (!InteractionManagerScript.S.focused)
        {
            spr.material = hoverMat;
            spr.material.SetFloat("_Thicc", thickness);
        }
    }

    private void OnMouseExit()
    {
        if (!InteractionManagerScript.S.focused)
        {
            spr.material = defMat;
        }
    }

    private void OnMouseDown()
    {
        if (!InteractionManagerScript.S.focused && !InteractionManagerScript.S.activeStory)
        {
            if (inkAsset != null) InteractionManagerScript.S.interaction = new Story(inkAsset.text);
            else InteractionManagerScript.S.interaction = null; //if we don't have a story: mark that

            InteractionManagerScript.S.focus = focus;
            InteractionManagerScript.S.focusObj();
            spr.material = defMat;
        }
    }
}
