using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class DynamicWaterDisabler : MonoBehaviour {

	[SerializeField]
	private GameObject WaterRenderer;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update()
	{
		if (Global.ingame.isDark == IsDark.Light)
		{
			WaterRenderer.SetActive(true);
		}
		else if (Global.ingame.isDark == IsDark.Dark)
		{
			WaterRenderer.SetActive(false);
		}
	}
}
