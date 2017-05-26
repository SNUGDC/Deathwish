using UnityEngine;
using System.Collections;
using Enums;

public class NewWaterChanger : MonoBehaviour {

	[SerializeField]
	private MeshRenderer waterMesh;
	/*
	[SerializeField]
	private new Sprite light;
	[SerializeField]
	private Sprite dark;
	*/
	// Use this for initialization
	void Start ()
	{
		waterMesh = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

		IsDark isDarkNow = Global.ingame.GetIsDarkInPosition(gameObject);
		if (isDarkNow == IsDark.Light)
		{
			waterMesh.enabled = true;
		}
		else if (isDarkNow == IsDark.Dark)
		{
			waterMesh.enabled = false;

		}

	}
}
