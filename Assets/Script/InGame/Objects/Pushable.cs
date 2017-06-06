using UnityEngine;
using System.Collections;
using Enums;

public class Pushable : MonoBehaviour, IRestartable
{
	private Vector3 originalPosition;
	public bool isLamp;
	bool onAir = true;
	int fallenCount = 0;

	// Use this for initialization
	void Start()
	{
		originalPosition = gameObject.transform.position;
	}

	// Update is called once per frame
	void Update ()
	{
		if(isLamp == true)
		{
			if(GetComponent<Lamp>().lampProperty == LampProperty.DarkLamp)
			{
				gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
				CheckLandingForSoundEffect ();
				return;
			}
		}

		Rigidbody2D rigidbody2d = GetComponent<Rigidbody2D>();
		if (Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Light)
		{
			// Setting isKinematic every frame cause physics bug
			if (rigidbody2d.isKinematic == true)
			{
				rigidbody2d.isKinematic = false;
			}
			CheckLandingForSoundEffect ();
		}
		else if (Global.ingame.GetIsDarkInPosition(gameObject) == IsDark.Dark)
		{
			// Setting isKinematic every frame cause physics bug
			
			if (rigidbody2d.isKinematic == false)
			{
				//testing purposes
				//GetComponent<Rigidbody2D>().mass = 1000000000;
				rigidbody2d.velocity = Vector2.zero;
				rigidbody2d.isKinematic = true;
			}
		}
	}



	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Ground" && onAir)
		{
			// does not play sound when map starts.
			if (fallenCount != 0) {
				SoundEffectController soundEffectController
					= GameObject.FindObjectOfType(typeof(SoundEffectController)) as SoundEffectController;
				soundEffectController.Play(SoundType.BoxFalling);
			}
			onAir = false;
			fallenCount += 1;
		}
	}

	void CheckLandingForSoundEffect()
	{
		if(!onAir)
			if(gameObject.GetComponent<Rigidbody2D>().velocity.y != 0)
				onAir=true;
	}

	void IRestartable.Restart()
	{
		gameObject.transform.position = originalPosition;
		gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
		onAir = true;
		SpriteSwitch spriteSwitch = gameObject.GetComponent<SpriteSwitch>();
		fallenCount = 0;
		if(spriteSwitch != null)
			gameObject.GetComponent<SpriteRenderer>().sprite = spriteSwitch.light;
	}
}
