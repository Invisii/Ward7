using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class InteractionManagerScript : MonoBehaviour
{
    public Text displayText;
    public GameObject dialogueBox;
    public Story interaction;
    public GameObject focus;
    public GameObject fader;
    public bool activeStory = false;
    public bool focused = false;
    
    private float typeSpeed = 0.03f;

    public static InteractionManagerScript S;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) checkClick();
        if (Input.GetKeyDown(KeyCode.RightArrow) && activeStory) DisplayNextSentence();
    }

    public void checkClick()
    {
        if (activeStory) DisplayNextSentence();
    }
    
    
    public void BeginDialogue()
    {
        interaction.ResetState(); //make sure we're at the beginning of the dialogue
        dialogueBox.SetActive(true); //show our dialogue box
        activeStory = true;
    }

    public void DisplayNextSentence()
    {
        if (interaction.canContinue) //if there's more story
        {
            string txt = interaction.Continue();
            List<string> tags = interaction.currentTags;
            StopAllCoroutines();
            StartCoroutine(TypeSentence(txt, tags));
        }
        else
        {
            dialogueBox.SetActive(false); //hide our dialogue box
            activeStory = false;
        }
    }
    
    /*
     * =======LIST OF TAGS=========
     * #softReturn - tack this onto the previous text without erasing.
     * #ignorePeriod - do not pause on the period(s) in this sentence.
     */

    IEnumerator TypeSentence(string sentence, List<string> tags)
    {
        if (tags.Contains("softReturn"))
        {
            displayText.text += "\r";
        }
        else displayText.text = "";
        foreach (char letter in sentence)
        {
            displayText.text += letter;
            if (letter.Equals('.') && !tags.Contains("ignorePeriod")) yield return new WaitForSeconds(10*typeSpeed);
            else yield return new WaitForSeconds(typeSpeed);
        }
    }

    public void focusObj()
    {
        fader.SetActive(true);
        focus.SetActive(true);
        focused = true;
    }

    public void unFocusObj()
    {
        focus.SetActive(false);
        fader.SetActive(false);
        focus = null;
        focused = false;
    }
}
