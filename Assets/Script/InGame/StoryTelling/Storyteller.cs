using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTeller
{
    public static bool ShouldStop()
    {
        TextBoxManager textBoxManager = GameObject.FindObjectOfType<TextBoxManager>();

        if (textBoxManager == null)
        {
            return false;
        }

        return textBoxManager.isActive == true;
    }
}
