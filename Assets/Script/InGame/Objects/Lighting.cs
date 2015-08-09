﻿using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System;
using Enums;

public class Lighting: MonoBehaviour, IRestartable
{
	public new Sprite light;
	public Sprite dark;
	public LightingChecker lightingChecker;

	private int length;
	
	GameObject[] effectGameObjects;

	bool isLighting;
	bool isEffectLighting;

	void Start()
	{
		isLighting = false;
		repeat ();
	}

	void Update()
	{
		effectGameObjects = GetComponentInChildren<LightingChecker> ().lightingEffectObjects;
		length = effectGameObjects.Length;

		if (lightingChecker.IsEffectLighting () && Global.ingame.isDark == IsDark.Dark && isLighting == true)
		{
			for(int i = 0; i < length; i++)
			{
				//  Temporary disable lightning.
				//  if (effectGameObjects[i].GetComponent<SpriteSwitch>() != null)
				//  	effectGameObjects[i].GetComponent<SpriteSwitch>().isDark = IsDark.Light;
			}
		}
	}

	void changeLight()
	{
		isLighting = true;
		GetComponent<SpriteRenderer> ().sprite = light;
	}
	void changeDark()
	{
		isLighting = false;
		GetComponent<SpriteRenderer> ().sprite = dark;
	}

	void repeat()
	{
		InvokeRepeating ("changeDark", 0, 10);
		InvokeRepeating ("changeLight", 5, 10);
	}

	void IRestartable.Restart()
	{
		Global.ingame.isDark = IsDark.Light;
	}

}
