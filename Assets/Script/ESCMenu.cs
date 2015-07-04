﻿using UnityEngine;
using System.Collections;

public class ESCMenu : MonoBehaviour {

	public GameObject PopupMenu;

	void Awake () {
		PopupMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyDown("escape")) && (PopupMenu.activeInHierarchy == false))
		{
			PopupMenu.SetActive(true);
		}
		// Using test.
		else if ((Input.GetKeyDown("escape")) && (PopupMenu.activeInHierarchy == true))
		{
			PopupMenu.SetActive(false);
		}
	}
	
	public void ClosePopup()
	{
		if (PopupMenu.activeInHierarchy == true)
		{
			PopupMenu.SetActive(false);
		}
	}
}