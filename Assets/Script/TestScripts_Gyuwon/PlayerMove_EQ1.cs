﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Enums;

public class PlayerMove_EQ1 : MonoBehaviour
{
	public float moveSpeed;
	public float jumpPower;
	public float climbSpeed;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	public Transform ladderCheck1;
	public Transform ladderCheck2;
	public Transform ladderCheck3;
	public Transform ladderCheck4;
	public Transform ladderCheck5;
	public float ladderCheckRadius;
	public LayerMask whatIsLadder;
	public GameObject Ladder;
	
	private Vector3 startPoint;
	private bool grounded;
	private bool laddered;
	private bool climbing;
	
	new Collider2D ladderToClimb;
	
	float gravityScale;
	
	void Start ()
	{
		startPoint = gameObject.transform.position;
		gravityScale = GetComponent<Rigidbody2D> ().gravityScale;
		ladderToClimb = null;
	}
	
	void Update ()
	{
		if(gameObject.transform.position.y <= -100 || Input.GetKeyDown(KeyCode.R))
		{
			Restarter.RestartAll();
		}
	}
	
	void FixedUpdate()
	{
		GetComponent<Rigidbody2D> ().gravityScale = gravityScale;
		
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
		laddered = (Physics2D.OverlapCircle (ladderCheck1.position, ladderCheckRadius, whatIsLadder)
		            || Physics2D.OverlapCircle (ladderCheck2.position, ladderCheckRadius, whatIsLadder)
		            || Physics2D.OverlapCircle (ladderCheck3.position, ladderCheckRadius, whatIsLadder)
		            || Physics2D.OverlapCircle (ladderCheck4.position, ladderCheckRadius, whatIsLadder)
		            || Physics2D.OverlapCircle (ladderCheck5.position, ladderCheckRadius, whatIsLadder));
		
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
		
		if (Input.GetKey (KeyCode.LeftArrow))
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
		}
	}
	
	void Jump ()
	{
		if (Input.GetKeyDown (KeyCode.Space) && grounded)
		{
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpPower);
		}
	}
	
	void Climb ()
	{
		if (laddered)
		{
			if (Input.GetKey (KeyCode.UpArrow))
			{
				whileClimbing();
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, climbSpeed);
			}
			else if (Input.GetKey (KeyCode.DownArrow))
			{
				whileClimbing();
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, -climbSpeed);
			}
			else if (grounded != true && climbing == true)
			{
				GetComponent<Rigidbody2D>().gravityScale = 0;
				GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
			}
		}
		else
		{
			climbing = false;
			if (ladderToClimb == null)
			{return;}
			ladderToClimb.gameObject.GetComponent<CeilingColliderController>().EnableCeiling();
		}
	}
	
	void whileClimbing ()
	{
		GetComponent<Rigidbody2D>().gravityScale = 0;
		SetPositionXAtCenterOfLadder(ladderToClimb);
		ladderToClimb.gameObject.GetComponent<CeilingColliderController>().DisableCeiling();
		climbing = true;
	}
	
	void OnTriggerStay2D (Collider2D coll)
	{
		if (coll.gameObject.tag == "Ladder")
		{
			ladderToClimb = coll.GetComponent<BoxCollider2D>();
		}
	}
	
	void OnCollisionEnter2D (Collision2D collision)
	{
		if(collision.gameObject.tag == "Fire" && Global.ingame.isDark == IsDark.Light)
		{
			Restarter.RestartAll();
		}
		
		if(collision.gameObject.tag == "Grass" && Global.ingame.isDark == IsDark.Dark)
		{
			Restarter.RestartAll();
		}
	}
	
	void SetPositionXAtCenterOfLadder(Collider2D coll)
	{
		float ladderX = coll.gameObject.transform.position.x;
		Vector3 playerPosition = gameObject.transform.position;
		playerPosition.x = ladderX;
		playerPosition.z = -0.000001f;
		gameObject.transform.position = playerPosition;
	}
	
	//void IRestartable.Restart()
	//{
	//	gameObject.transform.position = startPoint;
	//	ladderToClimb = null;
		//Temporarily reset isDark in Player.cs, but it should be moved to other script.
	//	Global.ingame.isDark = IsDark.Light;
	//}
}