﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{

	public GameObject textBox;

	public Text theText;

	public TextAsset textFiles;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	public Player player;

	public bool isActive;

	public bool stopPlayerMovement;

	public float WaitTime;

	// Use this for initialization
	protected virtual void Start()
	{
		player = FindObjectOfType<Player>();

		if (textFiles != null)
		{
			textLines = (textFiles.text.Split('\n'));
		}
		if (endAtLine == 0)
		{
			endAtLine = textLines.Length - 1;
		}

		if (isActive)
		{
			EnableTextBox();
		}

		else
		{
			DisableTextBox();
		}
	}

	// Update is called once per frame
	protected virtual void Update()
	{
		if (!isActive)
		{
			return;
		}

		theText.text = textLines[currentLine];

		if (Input.GetKeyDown(KeyCode.Space))
		{
			currentLine += 1;
		}


		if (currentLine > endAtLine)
		{
			DisableTextBox();
		}
	}

	public void EnableTextBox()
	{
		Debug.Log("Enable Text Box");
		textBox.SetActive(true);
		isActive = true;
		if (isActive)
		{
			//  StartCoroutine (AutoContinue (WaitTime));
		}
		if (stopPlayerMovement)
		{
			Debug.Log("Stop player movement");
			StartCoroutine(StopPlayerMovement());
		}
	}
	public virtual void DisableTextBox()
	{
		textBox.SetActive(false);
		isActive = false;
		StartCoroutine(EnablePlayerMovement());

	}

	public void ReloadScript(TextAsset theText)
	{
		if (theText != null)
		{
			textLines = new string[1];
			textLines = (theText.text.Split('\n'));

		}
	}

	IEnumerator StopPlayerMovement()
	{
		Debug.Log("Stop Player movement");
		yield return new WaitForEndOfFrame();
		player.canMove = false;
	}


	IEnumerator EnablePlayerMovement()
	{
		Debug.Log("Start Player movement");
		yield return new WaitForEndOfFrame();
		player.canMove = true;
	}


	//  	IEnumerator AutoContinue(float WaitTime)
	//  {
	//  	for (var f = 1.0; f >= 0; f -= 0.1)
	//  	{
	//  		currentLine +=1;
	//  		yield return new WaitForSeconds(WaitTime);
	//  	}
	//  }
}
