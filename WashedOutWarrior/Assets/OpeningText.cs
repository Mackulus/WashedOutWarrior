﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//code for fading here https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
public class OpeningText : MonoBehaviour {

	public GameObject chatbubble;
	private bool transitionFinished = false;
	private string[] strings = {
		"Doc says I'm sick...",
		"Says I can't eat bad food anymore...",
		"I'm going to have to fight my cravings...",
		"And become..."
	};
	private int currentTextSlot = 0;
	private float startTime;
	private bool fadeIn = true;
	private bool messageShowing = false;
	private bool transitioning = false;
	private bool transitionsFinished = false;

	void Start()
	{
		startTime = Time.time;
	}


	void Update()
	{
		if (!transitioning && !transitionsFinished)
		{
			transitioning = true;
			if (fadeIn)
			{
				StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
			}
			else
			{
				StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
			}
		}
	}
		
	public IEnumerator FadeTextToFullAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
		StartCoroutine(WaitAndShowText());
	}

	public IEnumerator WaitAndShowText()
	{
		yield return new WaitForSeconds(2);
		fadeIn = false;
		transitioning = false;
	}

	public IEnumerator FadeTextToZeroAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
		if (currentTextSlot < strings.Length - 1)
		{
			currentTextSlot++;
			i.text = strings[currentTextSlot];
		}
		else
		{
			transitionsFinished = true;
			chatbubble.GetComponent<FadeIn>().ResetStart();
			chatbubble.GetComponent<FadeIn>().fadeIn = false;
			Text[] texts = GameObject.Find("Canvas").GetComponentsInChildren<Text>();
			texts[1].GetComponent<TextFadeIn>().enabled = true;
			texts[2].GetComponent<TextFadeIn>().enabled = true;
		}

		if (currentTextSlot == 1)
		{
			GameObject.Find("MainMenuBackground").GetComponent<FadeIn>().enabled = true;
		}

		fadeIn = true;
		transitioning = false;
	}
}
