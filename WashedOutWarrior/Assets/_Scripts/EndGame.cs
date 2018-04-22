using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {

	public GameObject chatbubble;
	private GameObject gordo;
	private string[] strings = {
		"I did it! Doc says I'm good to go",
		"I'll never forget where I've come from",
		"Or the challenges I had to face",
		"Let's hope that I can stay a..."
	};
	private int currentTextSlot = 0;
	private bool fadeIn = true;
	private bool transitioning = false;
	private bool transitionsFinished = false;
	private Text[] texts;
	private SceneFader fadeScr;
	Transform[] children;
	

	void Start() {
		fadeScr = GameObject.FindObjectOfType<SceneFader>();
		gordo = GameObject.Find("OldGordo");
		children = GameObject.Find("OldEnemies").GetComponentsInChildren<Transform>();
		texts = GameObject.Find("Canvas").GetComponentsInChildren<Text>();
	}


	void Update() {
		if (!transitioning && !transitionsFinished) {
			transitioning = true;
			if (fadeIn) {
				StartCoroutine(FadeTextToFullAlpha(1f, GetComponent<Text>()));
			}
			else {
				StartCoroutine(FadeTextToZeroAlpha(1f, GetComponent<Text>()));
			}
		}

		if (Input.anyKeyDown && !transitionsFinished)
		{
			StopAllCoroutines();
			SkipStory();
		}
	}

	public void SkipStory()
	{
		chatbubble.GetComponent<FadeIn>().ResetStart();
		chatbubble.GetComponent<FadeIn>().fadeIn = false;
		chatbubble.GetComponentInChildren<Text>().text = "";
		fadeScr.EndScene("MainMenu");
	}

	public IEnumerator FadeTextToFullAlpha(float t, Text i) {
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f) {
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
		StartCoroutine(WaitAndShowText());
	}

	public IEnumerator WaitAndShowText() {
		yield return new WaitForSeconds(2);
		fadeIn = false;
		transitioning = false;
	}

	public IEnumerator FadeTextToZeroAlpha(float t, Text i) {
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f) {
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
		if (currentTextSlot < strings.Length - 1) {
			currentTextSlot++;
			i.text = strings[currentTextSlot];
		}
		else {
			transitionsFinished = true;
			chatbubble.GetComponent<FadeIn>().ResetStart();
			chatbubble.GetComponent<FadeIn>().fadeIn = false;
			foreach (Transform child in children){
				if(child.name != "OldEnemies"){
					child.gameObject.GetComponent<FadeIn>().ResetStart();
					child.gameObject.GetComponent<FadeIn>().fadeIn = false;
				}
			}
			gordo.GetComponent<FadeIn>().ResetStart();
			gordo.GetComponent<FadeIn>().fadeIn = false;
			texts[0].GetComponent<TextFadeIn>().enabled = true;
			texts[1].GetComponent<TextFadeIn>().enabled = true;
			StartCoroutine(BringInButtons());
		}

		if (currentTextSlot == 1) {
			gordo.GetComponent<FadeIn>().enabled = true;
		}
		else if (currentTextSlot == 2) {
			foreach (Transform child in children){
				if(child.name != "OldEnemies"){
					child.gameObject.GetComponent<FadeIn>().enabled = true;
				}
			}
		}

		fadeIn = true;
		transitioning = false;
	}

	public IEnumerator BringInButtons() {
		yield return new WaitForSeconds(3);
		chatbubble.SetActive(false);
		//for (int i = 0; i < buttons.Length; i++)
		//{
		//	buttons[i].gameObject.SetActive(true);
		//}
	}
}