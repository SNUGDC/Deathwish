﻿using UnityEngine;
using System.Collections;
using Enums;

public class Floatable : MonoBehaviour {

	private Collider2D coll;
	private float initGravityScale;

	// Use this for initialization
	void Start () {
		coll = gameObject.GetComponent<Collider2D>();
		initGravityScale = GetComponent<Rigidbody2D>().gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		Collider2D waterCollider = GetWaterCollider();

		if (waterCollider == null)
		{
			GetComponent<Rigidbody2D>().gravityScale = initGravityScale;
			return;
		}

		float upperBoundOfWaterCollider = waterCollider.bounds.max.y;
		if (gameObject.transform.position.y > upperBoundOfWaterCollider)
		{
			GetComponent<Rigidbody2D>().gravityScale = initGravityScale;
			return;
		}

		// Delete shake at surface.
		if (Mathf.Abs(gameObject.transform.position.y - upperBoundOfWaterCollider) < 0.1f)
		{
			transform.position = new Vector2(transform.position.x, upperBoundOfWaterCollider);
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		}
		else if (Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Light)
		{
			GetComponent<Rigidbody2D>().gravityScale = 0;
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
		}	
	}

	Collider2D GetWaterCollider()
	{
		Collider2D[] otherColliders = Physics2D.OverlapAreaAll(coll.bounds.max, coll.bounds.min);
		foreach (Collider2D otherCollider in otherColliders)
		{
			if (otherCollider.gameObject.tag == "Water")
				return otherCollider;
		}
		return null;
	}
}
