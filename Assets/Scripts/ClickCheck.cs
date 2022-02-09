using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickCheck : MonoBehaviour
{
    public GameObject dialogue;
    public Physics2DRaycaster raycaster;
    
    private void Awake()
    {
        raycaster = Camera.main.GetComponent<Physics2DRaycaster>();
    }

    public void interactLogic(InputAction.CallbackContext context)
    {
        if (dialogue.activeSelf)
        {
            if (context.performed)
            {
              InteractionManagerScript.S.DisplayNextSentence();   
            }
            return;
        }
        if (!context.performed) return;
        checkHit();
    }
    
    public void checkHit()
    {
        //if (!context.performed) return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue())* new Vector2(-1, 1);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, mousePos);
        Debug.DrawRay(transform.position, mousePos, Color.cyan, 100, false);
        if (hit.collider != null)
        {
            Debug.Log(hit.transform.name);
            Debug.Log("Collider Detected");
            POIScript target = hit.collider.gameObject.GetComponent<POIScript>();
            if (target != null) target.targeted();
        }
    }
}
