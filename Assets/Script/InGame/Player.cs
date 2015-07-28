﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class Player : MonoBehaviour, IRestartable
{
	public float moveSpeed;
	public float jumpPower;
	public float climbSpeed;

	private Vector3 startPoint;
	private bool climbing;
	private float yOfLowestObject;

	float gravityScale;

	public GroundChecker groundChecker;
	public LadderChecker ladderChecker;
	
	void Start ()
	{
		startPoint = gameObject.transform.position;
		gravityScale = GetComponent<Rigidbody2D> ().gravityScale;
		yOfLowestObject = LowestObjectFinder.Find ().position.y;

	}
	
	void Update ()
	{
		if(gameObject.transform.position.y - yOfLowestObject <= -10 || Input.GetKeyDown(KeyCode.R))
		{
			Restarter.RestartAll();
		}

		GetComponent<Rigidbody2D> ().gravityScale = gravityScale;
		Move ();
		Jump ();
		Climb ();
	}
	
	void Move ()
	{
		if (Input.GetKey (KeyCode.RightArrow))
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}
		else
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
		}
	}

	private void Jump()
	{
		if (Input.GetKeyDown (KeyCode.Space) && groundChecker.IsGrounded())
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower);
		}
	}
	
	void Climb ()
	{
		if (ladderChecker.IsLaddered())
		{
			if (Input.GetKey (KeyCode.UpArrow))
			{
				changeWhileClimbing();
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, climbSpeed);
			}
			else if (Input.GetKey (KeyCode.DownArrow))
			{
				changeWhileClimbing();
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, -climbSpeed);
			}
			else if (groundChecker.IsGrounded() != true && climbing == true)
			{
				GetComponent<Rigidbody2D>().gravityScale = 0;
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			}
		}
		else
		{
			climbing = false;
			Collider2D ladderToClimb = ladderChecker.GetLatestLadderCollider();
			if (ladderToClimb == null)
				return;
			ladderToClimb.gameObject.GetComponent<CeilingColliderController>().EnableCeiling();
		}
	}
	
	void changeWhileClimbing ()
	{
		GetComponent<Rigidbody2D>().gravityScale = 0;
		Collider2D ladderToClimb = ladderChecker.GetLadderCollider ();
		SetPositionXAtCenterOfLadder(ladderToClimb);
		ladderToClimb.gameObject.GetComponent<CeilingColliderController>().DisableCeiling();
		climbing = true;
	}
	
	void SetPositionXAtCenterOfLadder(Collider2D coll)
	{
		float ladderX = coll.gameObject.transform.position.x;
		Vector3 playerPosition = gameObject.transform.position;
		playerPosition.x = ladderX;
		gameObject.transform.position = playerPosition;
	}
	
	void IRestartable.Restart()
	{
		gameObject.transform.position = startPoint;
		//Temporarily reset isDark in Player.cs, but it should be moved to other script.
		Global.ingame.isDark = IsDark.Light;
	}
}