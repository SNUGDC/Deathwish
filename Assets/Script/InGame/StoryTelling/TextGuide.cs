using UnityEngine;
using System.Collections;

public class TextGuide : MonoBehaviour {

	public GameObject shoutZone;

	private bool isAlreadyActive = false;
	private SpriteRenderer renderer;

	// Use this for initialization
	void Start () {
		renderer = gameObject.GetComponent<SpriteRenderer>();
		renderer.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if ((isAlreadyActive == false) && (shoutZone == null) &&
			(StoryTeller.ShouldStop()))
		{
			isAlreadyActive = true;
			renderer.enabled = true;
		}

		if ((renderer.enabled = true) && StoryTeller.ShouldStop() == false)
			renderer.enabled = false;
	}
}
