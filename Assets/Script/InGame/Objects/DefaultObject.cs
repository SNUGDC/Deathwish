﻿using UnityEngine;
using System.Collections;

public class DefaultObject : ObjectMonoBehaviour {

	// Update is called once per frame
	public override void UpdateByParent () {
		GetComponent<SpriteRenderer>().enabled = true;
		GetComponent<Collider2D>().enabled = true;
	}

	public override void HideObject()
	{
		GetComponent<SpriteRenderer>().enabled = false;
		GetComponent<Collider2D>().enabled = false;
	}
}
