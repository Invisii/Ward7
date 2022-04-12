using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Sticker
{
    public GameObject stick;
    public int page;
    public Story flavorText;
    public bool unlocked = false;
}

public class stickerBook : MonoBehaviour
{
    public static stickerBook S;

    public GameObject book;
    private void Start()
    {
        S = this;
    }

    //Something to keep track of stickers    
        //Each sticker has a gameObject (original location), sprite, page, location, ink file, and active/inactive state
        //This could be helper class, but maybe more likely a script?
        //This for sure is a Singleton
        
    //Add Sticker to book
    //Open Book
    //Close Book
    //Interact with Stickers
    //Page Nav

    public void openStickerBook()
    {
        InteractionManagerScript.S.focus = book;
        InteractionManagerScript.S.focusObj();
    }
}
