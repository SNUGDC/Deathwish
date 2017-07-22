using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class BlockingStorytellerTrigger : MonoBehaviour
{
    public string textFileName = "";
    public bool lightMode;
    public bool darkMode;

    void OnTriggerStay2D(Collider2D other)
    {
        IsDark currentDark = Global.ingame.GetIsDarkInPosition(gameObject);

        if (currentDark == IsDark.Dark && !darkMode)
        {
            return;
        }

        if (currentDark == IsDark.Light && !lightMode)
        {
            return;
        }

        TextAsset textAsset = Resources.Load<TextAsset>("Text/" + textFileName);
        BlockingStoryteller.Instance.Show(textAsset);

        Destroy(gameObject);
    }
}
