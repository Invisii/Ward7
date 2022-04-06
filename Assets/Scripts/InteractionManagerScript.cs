using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class InteractionManagerScript : MonoBehaviour
{
    public Text displayText;
    public Story interaction;
    public GameObject dialogueBox, focus, fader, closeButton;
    public GameObject leftChar, rightChar;
    public Animator boxAnim;
    public bool activeStory = false;
    public bool focused = false;
    public Sprite[] chars;

    private bool typing = false;
    private string txt;
    private bool diaLeft = true;
    private float typeSpeed = 0.03f;

    private Vector3 leftBox, leftText, rightBox, rightText;
    private float boxOffset = 0;

    public static InteractionManagerScript S;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
    }

    private void Start()
    {
        leftText = displayText.transform.position;
        rightText = new Vector3(leftText.x - 150, leftText.y, leftText.z);

        leftBox = dialogueBox.transform.position;
        rightBox = new Vector3(leftBox.x + boxOffset, leftBox.y, leftBox.z);
    }

    public void checkClick()
    {
        if (activeStory && typing)
        {
            typing = false;
            finishSentence();
        }
        else if (activeStory)
        {
            DisplayNextSentence();
        }
    }

    public void BeginDialogue()
    {
        interaction.ResetState(); //make sure we're at the beginning of the dialogue
        dialogueBox.SetActive(true); //show our dialogue box
        activeStory = true;
        DisplayNextSentence();
    }
    
    /*
     * =======LIST OF TAGS=========
     *[global tags] are on the first line in a file, but apply to the entire file
     * 
     * #char - indicates this line is spoken, check for which character speaks.
     *     #Bram, #Eiko, #Eli, #Finn, #Indra, #Lani, #Marlowe, #Nurse, #Nurse2, #Orly, #youngBram
     * #dia - [global tag] indicates that this file is a dialogue rather than plain text.
     * #desc - this line of text is description rather than dialogue
     * #ignorePeriod - do not pause on the period(s) in this sentence.
     * #softReturn - tack this onto the previous text without erasing.
     * #switch - dialogue is changing speakers, animate textbox
     */

    public void DisplayNextSentence()
    {
        if (interaction.canContinue) //if there's more story
        {
            RectTransform textRect = (RectTransform) displayText.transform;
            txt = interaction.Continue();
            List<string> tags = interaction.currentTags;
            if (tags.Contains("dia"))
            {
                boxAnim.SetBool("Dialogue", true);
                leftChar.SetActive(true);
                rightChar.SetActive(true);
                leftChar.GetComponent<Animator>().SetBool("Shown", true);
            }
            if (tags.Contains("char")) setSpeakerSprite(tags);
            if (tags.Contains("switch")) //if speakers have changed
            {
                if (diaLeft)
                {
                    boxAnim.SetBool("RightSpeaker", true);
                    leftChar.GetComponent<Animator>().SetBool("Shown", false);
                    rightChar.GetComponent<Animator>().SetBool("Shown", true);
                    textRect.position = rightText;
                    dialogueBox.transform.position = rightBox;
                    diaLeft = false;
                }
                else
                {
                    boxAnim.SetBool("RightSpeaker", false);
                    leftChar.GetComponent<Animator>().SetBool("Shown", true);
                    rightChar.GetComponent<Animator>().SetBool("Shown", false);
                    textRect.position = leftText;
                    dialogueBox.transform.position = leftBox;
                    diaLeft = true;
                }
            }
            StopAllCoroutines();
            StartCoroutine(TypeSentence(txt, tags));
        }
        else
        {
            dialogueBox.SetActive(false); //hide our dialogue box
            leftChar.SetActive(false); //hide characters
            rightChar.SetActive(false);
            activeStory = false;
        }
    }

    private void setSpeakerSprite(List<string> tags)
    {
        int num = -1;
        if (tags.Contains("Bram")) num = 0;
        else if (tags.Contains("Eiko")) num = 1;
        else if (tags.Contains("Eli")) num = 2;
        else if (tags.Contains("Finn")) num = 3;
        else if (tags.Contains("Indra")) num = 4;
        else if (tags.Contains("Lani")) num = 5;
        else if (tags.Contains("Marlowe")) num = 6;
        else if (tags.Contains("Nurse")) num = 7;
        else if (tags.Contains("nurse2")) num = 8;
        else if (tags.Contains("Orly")) num = 9;
        else if (tags.Contains("youngBram")) num = 10;

        if (num > -1)
        {
            if (tags.Contains("dia")) leftChar.GetComponent<Image>().sprite = chars[num];
            else
            {
                if (diaLeft) rightChar.GetComponent<Image>().sprite = chars[num]; //apply them to the correct side
                else leftChar.GetComponent<Image>().sprite = chars[num];
            }
        }
        else throw new NotImplementedException(); //or tell me if I messed up
    }

    private void finishSentence()
    {
        StopAllCoroutines();
        displayText.text = txt;
    }

    IEnumerator TypeSentence(string sentence, List<string> tags)
    {
        displayText.text = "";
        if (tags.Contains("switch") || tags.Contains("dia")) yield return new WaitForSeconds(1f);
        displayText.fontStyle = tags.Contains("desc") ? FontStyle.Italic : FontStyle.Normal;
        typing = true;
        if (tags.Contains("softReturn")) displayText.text += "\r";
        else displayText.text = "";
        foreach (char letter in sentence)
        {
            displayText.text += letter;
            if ((letter.Equals('.') || letter.Equals('?') || letter.Equals('!'))
                 && !tags.Contains("ignorePeriod")) yield return new WaitForSeconds(10*typeSpeed);
            else yield return new WaitForSeconds(typeSpeed);
        }

        typing = false;
    }

    public void focusObj()
    {
        fader.SetActive(true);
        focus.SetActive(true);
        closeButton.SetActive(true);
        focused = true;
    }

    public void unFocusObj()
    {
        focus.SetActive(false);
        fader.SetActive(false);
        closeButton.SetActive(false);
        focus = null;
        focused = false;
    }
}
