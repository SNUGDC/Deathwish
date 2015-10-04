using UnityEngine;
using System.Collections;
using Enums;
using UnityStandardAssets.ImageEffects;
using System;
using System.Linq;

public class SwitchDarkLight : MonoBehaviour, IRestartable
{
	private bool isGround;
	private bool isPlayer = false;
	private bool isItUsedNow = false;
	private Camera blurEffectCamera;
	private BlurOptimized blur;
	IEnumerator mirrorEffectCoroutine;

	void Start()
	{
		blur = FindObjectOfType<BlurOptimized>();
		blurEffectCamera = blur.gameObject.GetComponent<Camera>();
		blurEffectCamera.enabled = false;
	}

	void Update()
	{
		isGround = GameObject.FindObjectOfType<GroundChecker> ().IsGrounded ();

		if (isPlayer && Input.GetKeyDown(KeyCode.UpArrow) && isGround && (!isItUsedNow)) //changed for mirror disabling purposes when reading dialogue.
		{
			if ((FindObjectOfType<TextBoxManager>() != null) && (FindObjectOfType<TextBoxManager>().isActive))
				return;
			mirrorEffectCoroutine = PlayMirrorEffect();
			StartCoroutine(mirrorEffectCoroutine);
		}
	}

	IEnumerator PlayMirrorEffect()
	{
		//chapter 5 has 2 player.
		var players = FindObjectsOfType<Player>();

		isItUsedNow = true;
		foreach (var player in players) {
			player.canMove = false;
		} 
		blur.blurSize = 0;
		blurEffectCamera.enabled = true;
		
		// chapter 5 has seprated camera.
		// other chapter's camera is in player so only chapter 5 should be fixed.
		var chapter5Camera = FindObjectOfType<Chapter5Camera>();
		if (chapter5Camera != null)
		{
			blurEffectCamera.GetComponent<CameraController>().enabled = false;
			blurEffectCamera.transform.position = chapter5Camera.transform.position;
		}

		SoundEffectController soundEffectController
			= GameObject.FindObjectOfType(typeof(SoundEffectController)) as SoundEffectController;
		soundEffectController.Play(SoundType.Mirror);

		for (int i = 0; i < 50; i++)
		{
			blurEffectCamera.GetComponent<BlurOptimized>().blurSize += 0.2f;
			yield return new WaitForSeconds(0.5f / 50f);
		}

		Global.ingame.ChangeDarkLight();

		for (int i = 0; i < 50; i++)
		{
			blurEffectCamera.GetComponent<BlurOptimized>().blurSize -= 0.2f;
			yield return new WaitForSeconds(0.5f / 50f);
		}

		blurEffectCamera.enabled = false;
		foreach (var player in players) {
			player.canMove = true;
		} 
		isItUsedNow = false;

		Restarter.SaveAll();
	}

	void OnTriggerEnter2D(Collider2D player)
	{
		if (player.gameObject.tag == "Player")
		{
			isPlayer = true;
		}
	}

	void OnTriggerExit2D(Collider2D player)
	{
		if (player.gameObject.tag == "Player")
		{
			isPlayer = false;
		}
	}

	void OnDestroy()
	{
		if (mirrorEffectCoroutine != null)
			StopCoroutine(mirrorEffectCoroutine);
		if (blurEffectCamera != null)
			blurEffectCamera.enabled = false;
	}

	void IRestartable.Restart()
	{
		if (mirrorEffectCoroutine != null)
			StopCoroutine(mirrorEffectCoroutine);
		isItUsedNow = false;
		blurEffectCamera.enabled = false;
	}

    void IRestartable.Save()
    {
    }
}
