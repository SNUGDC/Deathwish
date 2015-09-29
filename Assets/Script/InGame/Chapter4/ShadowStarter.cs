using UnityEngine;
using System.Collections;
using System;

public class ShadowStarter : MonoBehaviour, IRestartable {

	// Use this for initialization
	void Awake () {
		Global.ingame.isDark = Enums.IsDark.Dark;
	}

	// Player code do these things.
    void IRestartable.Restart()
    {
    }

    void IRestartable.Save()
    {
    }
}
