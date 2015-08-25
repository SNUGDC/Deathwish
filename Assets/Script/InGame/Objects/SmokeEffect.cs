﻿using UnityEngine;
using System.Collections;
using Enums;

public class SmokeEffect : MonoBehaviour, IRestartable
{
	public GameObject smokeEffect_Light;
	public GameObject smokeEffect_Dark;
	
	Vector3 position;
	
	void Start ()
	{
		smokeEffect_Light.SetActive(true);
		smokeEffect_Dark.SetActive(false);
		position = GetComponent<Transform> ().position;
	}
	
	void Update()
	{
		if (Global.ingame.GetIsDarkInPosition (position) == IsDark.Light)
		{
			smokeEffect_Light.SetActive(true);
			smokeEffect_Dark.SetActive(false);
		}
		else if (Global.ingame.GetIsDarkInPosition (position) == IsDark.Dark)
		{
			smokeEffect_Light.SetActive(false);
			smokeEffect_Dark.SetActive(true);
		}
	}
	
	void IRestartable.Restart()
	{
		smokeEffect_Light.SetActive(true);
		smokeEffect_Dark.SetActive(false);
	}
}
