using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChapterStartTextViewer : MonoBehaviour {

	public Text chapterTitleText;
	public Text chapterPrologueText;
	public TextAsset chapterPrologueTextFile;
	public Image chapterBackground;

	// Use this for initialization
	IEnumerator Start () {
		Initialize();
		
		yield return StartCoroutine(FadeInBackground());

		// Player CANNOT move when prologue is playing.
		GameObject.FindObjectOfType<Player>().canMove = false;

		if (chapterPrologueText != null && chapterPrologueTextFile != null)
		{
			var textMesh = chapterPrologueText.GetComponent<TextMesh>();
			textMesh.text = chapterPrologueTextFile.text;
		}

		yield return StartCoroutine(ViewText(chapterTitleText));
		
		if (chapterPrologueText != null)
			yield return StartCoroutine(ViewText(chapterPrologueText));

		yield return StartCoroutine(FadeOutBackground());

		GameObject.FindObjectOfType<Player>().canMove = true;
	}

	void Initialize()
	{
		chapterBackground.color = Color.black;
		if (chapterPrologueText != null)
			chapterPrologueText.color += new Color(0, 0, 0, -1); 
		chapterTitleText.color += new Color(0, 0, 0, -1);
	}

	IEnumerator FadeInBackground()
	{
		for (int i=0; i<50; i++)
		{
			chapterBackground.color += new Color(0.02f, 0.02f, 0.02f, 0);
			yield return new WaitForSeconds(0.005f);
		}
		
		yield return new WaitForSeconds(1);
	}

	IEnumerator FadeOutBackground()
	{
		for (int i=0; i<50; i++)
		{
			chapterBackground.color -= new Color(0, 0, 0, 0.02f);
			yield return new WaitForSeconds(0.005f);
		}
	}	
	
	IEnumerator ViewText(Text text)
	{
		for (int i=0; i<50; i++)
		{
			text.color += new Color(0, 0, 0, 0.02f);
			yield return new WaitForSeconds(0.005f);
		}

		yield return new WaitForSeconds(2);

		for (int i=0; i<50; i++)
		{
			text.color -= new Color(0, 0, 0, 0.02f);
			yield return new WaitForSeconds(0.005f);
		}

		yield return new WaitForSeconds(2);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
