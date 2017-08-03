using UnityEngine;
using System.Collections;
using Enums;

public class ActiveTempJail : MonoBehaviour
{

	public enum ActiveTrigger
	{
		None,
		ShoutZone,
		Collider
	}

	public ActiveTrigger activeTrigger;
	public GameObject tempJail;
	public GameObject triggerShoutZone;
	public Collider2D triggerCollider;
	public GameObject nextShoutZone;
	public IsDark disappearIn;

	public GameObject triggerMirror;

	// Update is called once per frame
	void Update()
	{
		if ((nextShoutZone != null) && (tempJail.activeInHierarchy == false)
			&& (disappearIn != Global.ingame.isDark))
		{
			if ((activeTrigger == ActiveTrigger.ShoutZone)
				&& (triggerShoutZone == null) && !StoryTeller.ShouldStop())
			{
				tempJail.SetActive(true);
			}
			else if ((activeTrigger == ActiveTrigger.Collider)
				&& (triggerCollider == null))
			{
				tempJail.SetActive(true);
			}
		}

		if ((tempJail.activeInHierarchy == true) && (disappearIn == Global.ingame.isDark))
		{
			tempJail.SetActive(false);
			// Destroy(triggerMirror.GetComponent<SwitchDarkLight>(), 3f);
		}
	}

	void OnTriggerEnter2D()
	{
		if (activeTrigger == ActiveTrigger.Collider)
			Destroy(triggerCollider);
	}
}
