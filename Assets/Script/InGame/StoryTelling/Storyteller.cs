using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryTeller
{
    public static bool ShouldStop()
    {
		return IsTextBoxActive() || IsBlockingStoryTellerActive();
    }

    private static bool IsBlockingStoryTellerActive()
    {
      if (BlockingStoryteller.Instance == null) return false;
		return BlockingStoryteller.Instance.IsShowingText();
    }

    private static bool IsTextBoxActive()
	{
        TextBoxManager textBoxManager = GameObject.FindObjectOfType<TextBoxManager>();

        if (textBoxManager == null)
        {
            return false;
        }

        return textBoxManager.isActive == true;
	}
}
