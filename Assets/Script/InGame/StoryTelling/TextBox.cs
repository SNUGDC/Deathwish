using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour
{
    public Text text;
    private string[] textLines;
    int currentLine = 0;
    public bool showingText = false;
    private Action onDialogueEnd;

    // Use this for initialization
    void Start()
    {
        if (textLines == null)
        {
            textLines = new string[0];
        }

        if (onDialogueEnd == null)
        {
            onDialogueEnd = () => { };
        }
    }

	// ShowDialogue may be called before Start method
    public void ShowDialogue(string dialogue, Action onDialogueEnd)
    {
        gameObject.SetActive(true);
        textLines = dialogue.Split('\n');
        currentLine = 0;
        showingText = true;
        text.text = textLines[currentLine];
        this.onDialogueEnd = onDialogueEnd;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentLine >= textLines.Length - 1)
            {
                textLines = new string[0];
                currentLine = 0;
                gameObject.SetActive(false);
                showingText = false;
                onDialogueEnd();
                onDialogueEnd = () => { };
                return;
            }

            currentLine += 1;
            text.text = textLines[currentLine];
        }
    }
}
