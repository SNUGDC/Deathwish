using UnityEngine;
using System.Collections;
using Enums;

public class SpriteAlphaSwitch : MonoBehaviour
{
	public float lightAlpha = 1.0f;
	public float darkAlpha = 1.0f;
	private SpriteRenderer spriteRenderer;

	void Start ()
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	private IsDark isDarkDebug;
	void Update ()
	{
		IsDark isDarkNow = Global.ingame.GetIsDarkInPosition(gameObject);
		this.isDarkDebug = isDarkNow;
		if(isDarkNow == IsDark.Light)
		{
			var color = spriteRenderer.color;
			spriteRenderer.color = new Color(color.r, color.g, color.b, lightAlpha);
		}
		else if(isDarkNow == IsDark.Dark)
		{
			var color = spriteRenderer.color;
			spriteRenderer.color = new Color(color.r, color.g, color.b, darkAlpha);
		}
	}
}
