using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldStoryTeller : MonoBehaviour
{
    // public string text;
    public float coolTime;
    private IEnumerator animation;
    private float _alpha;
    private float alpha
    {
        get
        {
            return _alpha;
        }
        set
        {
            _alpha = value;
            var textColor = textComponent.color;
            textComponent.color = new Color(textColor.r, textColor.g, textColor.b, _alpha);
			var imageColor = imageComponent.color;
			imageComponent.color = new Color(imageColor.r, imageColor.g, imageColor.b, _alpha);
        }
    }
    public Text textComponent;
	public Image imageComponent;

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
        {
            return;
        }

        FadeIn();
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            return;
        }

        FadeOut();
    }

    void FadeIn()
    {
        if (animation != null)
        {
            StopCoroutine(animation);
        }

        animation = FadeInCoroutine();
        StartCoroutine(animation);
    }

    IEnumerator FadeInCoroutine()
    {
        while (alpha < 1)
        {
            alpha = Mathf.Clamp(alpha + 0.05f, 0, 1);
            yield return new WaitForSeconds(0.05f);
        }
    }

    void FadeOut()
    {
        if (animation != null)
        {
            StopCoroutine(animation);
        }

        animation = FadeOutCoroutine();
        StartCoroutine(animation);
    }

    IEnumerator FadeOutCoroutine()
    {
        while (alpha > 0)
        {
            alpha = Mathf.Clamp(alpha - 0.05f, 0, 1);
            yield return new WaitForSeconds(0.05f);
        }
    }


    // Use this for initialization
    void Start()
    {
        alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
