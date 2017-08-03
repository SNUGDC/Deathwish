using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockingStoryteller : MonoBehaviour
{
    private static BlockingStoryteller instance;
    public static BlockingStoryteller Instance
    {
        get
        {
            return instance;
        }
    }
    public TextBox textBox;
    bool showingText = false;
    private Player player;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();
    }

    public bool IsShowingText()
    {
        return textBox.showingText;
    }

    public void Show(TextAsset textAsset, Action onDialogueEnd)
    {
        Debug.Log("Show text" + textAsset.name);
        Debug.Log(textAsset.text);
        textBox.ShowDialogue(textAsset.text, onDialogueEnd: () => {
			player.canMove = true;
			onDialogueEnd();
		});

        player.canMove = false;
    }
}
