using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    [SerializeField]
    private Text targetColorText;

	[SerializeField]
	private Text incorrectColorMessage;

	[SerializeField]
	float incorrectColorMessageDuration = 1.0f;

	private Coroutine incorrectMsgCoroutine;

	void Awake()
    {
		Debug.Assert(targetColorText != null, "TargetColorText is null, setup it");
		Debug.Assert(incorrectColorMessage != null, "IncorrectMessage is null, setup it");

		incorrectColorMessage.color = new Color(incorrectColorMessage.color.r, incorrectColorMessage.color.g, incorrectColorMessage.color.b, 0.0f);
	}
    public void SetTargetColorText(string text)
    {
		targetColorText.text = text;
	}

	public void ShowIncorrectColorMessage()
	{
		if (incorrectMsgCoroutine != null)
		{
			StopCoroutine(incorrectMsgCoroutine);
		}

		incorrectMsgCoroutine = StartCoroutine(FadeOutText(incorrectColorMessage, incorrectColorMessageDuration));
	}

	IEnumerator FadeOutText(Text text, float time)
	{
		text.color = new Color(text.color.r, text.color.g, text.color.b, 1.0f);
		while (text.color.a > 0.0f)
		{
			text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / time));
			yield return null;
		}
	}
}
