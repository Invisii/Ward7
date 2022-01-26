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

    public static InteractionManagerScript S;

    private float typeSpeed = 0.03f;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }

    public void BeginDialogue()
    {
        interaction.ResetState(); //make sure we're at the beginning of the dialogue
        dialogueBox.SetActive(true); //show our dialogue box
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (interaction.canContinue) //if there's more story
        {
            string txt = interaction.Continue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(txt));
        }
        else
        {
            dialogueBox.SetActive(false); //hide our dialogue box
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        displayText.text = "";
        foreach (char letter in sentence)
        {
            displayText.text += letter;
            if (letter.Equals('.')) yield return new WaitForSeconds(10*typeSpeed);
            else yield return new WaitForSeconds(typeSpeed);
        }
    }
}
