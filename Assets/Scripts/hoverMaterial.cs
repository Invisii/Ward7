using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoverMaterial : MonoBehaviour
{
    public Material hoverMat;
    [Range(0f, 200f)]
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
        if (!InteractionManagerScript.S.focused && !InteractionManagerScript.S.activeStory)
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
}
