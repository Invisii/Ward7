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
            displayText.text = interaction.Continue();
        }

        if (interaction.currentChoices.Count > 0)
        {
            foreach (var choice in interaction.currentChoices)
            {
                displayText.text += choice.text;
            }
        }
        else
        {
            dialogueBox.SetActive(false); //hide our dialogue box
        }
    }
}
