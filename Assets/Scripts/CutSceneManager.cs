using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneManager : MonoBehaviour
{
    public int sceneSelector;
    public TextAsset sceneDialogue, splashTextAsset;
    public GameObject splash, diaBox, nextButton, marl, logo;
    public Text splashTextBox;
    public bool marloweClicked = false;

    private float typeSpeed = 0.05f;
    private bool typing = false;
    private Animator splashAnim, textAnim, logoAnim;
    private Story splashText;
    private WaitForSeconds oneSec = new WaitForSeconds(1f);
    private WaitForSeconds twoSec = new WaitForSeconds(2f);
    private WaitForSeconds threeSec = new WaitForSeconds(3f);
    private WaitForSeconds fiveSec = new WaitForSeconds(5f);

    public static CutSceneManager C;

    private void Awake()
    {
        if (C == null)
        {
            C = this;
        }
        
        splashAnim = splash.GetComponent<Animator>();
        textAnim = splashTextBox.GetComponent<Animator>();
        logoAnim = logo.GetComponent<Animator>();
        splashText = new Story(splashTextAsset.text);
    }

    private void Start()
    {
        switch (sceneSelector)
        {
           case 1:
               StartCoroutine(hospitalCarryScene()); 
               break;
           default:
               Debug.Log("No Scene Selected");
               break;
        }
    }
    
    public void DisplayNextSentence()
    {
        if (splashText.canContinue) //if there's more story
        {
            string txt = splashText.Continue();
            txt = txt.TrimEnd('\r', '\n');
            List<string> t = splashText.currentTags;
            Debug.Log(txt);
            StartCoroutine(TypeSentence(txt, t));
        }
    }
    
    IEnumerator TypeSentence(string sentence, List<string> tags)
    {
        foreach (char letter in sentence)
        {
            splashTextBox.text += letter;
            if (letter.Equals('\\') || letter.Equals('\\')) continue;
            if (letter.Equals('.') && !tags.Contains("ignorePeriod")) yield return new WaitForSeconds(10*typeSpeed);
            yield return new WaitForSeconds(typeSpeed);
        }

        typing = false;
    }

    IEnumerator hospitalCarryScene()
    {
        yield return oneSec;
        splashTextBox.text = "";
        DisplayNextSentence();
        typing = true;
        yield return new WaitUntil(() => !typing);
        yield return twoSec;
        adjustAnims(true);
        splashTextBox.text = "";
        yield return twoSec; //wait for anim to finish
        InteractionManagerScript.S.interaction = new Story(sceneDialogue.text);
        InteractionManagerScript.S.BeginDialogue();
        yield return threeSec;
        nextButton.SetActive(true);
        yield return new WaitUntil(() => !diaBox.activeSelf);
        yield return twoSec;
        
        //dad.? please don't go
        adjustAnims(false);
        yield return twoSec;
        DisplayNextSentence();
        yield return threeSec;
        marl.SetActive(true);
        textAnim.SetBool("hidden", true);
        yield return twoSec;
        
        //reveal marlowe
        adjustAnims(true);
        yield return new WaitUntil(() => marloweClicked);
        splashTextBox.text = "";
        adjustAnims(false);
        yield return twoSec;
        
        //i'm right here
        splashTextBox.text = "";
        textAnim.SetBool("hidden", false);
        yield return twoSec;
        DisplayNextSentence();
        yield return threeSec;
        textAnim.SetBool("hidden", true);
        yield return twoSec;
        
        //logo
        logoAnim.SetBool("Play", true);
        yield return threeSec;
        
        //marlowe...
        splashTextBox.text = "";
        textAnim.SetBool("hidden", false);
        yield return twoSec;
        DisplayNextSentence();
        yield return threeSec;
        textAnim.SetBool("hidden", true);
        yield return twoSec;
        
        //marlowe it's time to wake up
        splashTextBox.text = "";
        textAnim.SetBool("hidden", false);
        yield return twoSec;
        DisplayNextSentence();
        yield return fiveSec;
        
        //change Scenes
        SceneManager.LoadScene("Marlowe");
    }

    private void adjustAnims(bool val)
    {
        splashAnim.SetBool("hidden", val);
        textAnim.SetBool("hidden", val);
    }

    IEnumerator enterMarlowe()
    {
        yield break;
    }
    
}
