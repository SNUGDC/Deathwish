﻿using UnityEngine;
using System.Collections;
using Enums;

public class Damage : MonoBehaviour
{
	public bool isActiveAtLight = false;
	public bool isActiveAtDark = false;

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if ((isActiveAtLight && (Global.ingame.isDark == IsDark.Light)) ||
				(isActiveAtDark && (Global.ingame.isDark == IsDark.Dark)))
			Restarter.RestartAll();
		}
	}

	void OnTriggerEnter2D (Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			if ((isActiveAtLight && (Global.ingame.isDark == IsDark.Light)) ||
				(isActiveAtDark && (Global.ingame.isDark == IsDark.Dark)))
			Restarter.RestartAll();
		}
	}
}