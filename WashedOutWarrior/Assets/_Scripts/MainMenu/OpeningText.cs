using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//code for fading here https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
public class OpeningText : MonoBehaviour {
	public GameObject chatbubble;
	//private bool transitionFinished = false;
	private string[] strings = {
		"Doc says I'm sick...",
		"Says I can't eat bad food anymore...",
		"I'm going to have to fight my cravings...",
		"And become..."
	};
	private int currentTextSlot = 0;
	//private float startTime;
	private bool fadeIn = true;
	//private bool messageShowing = false;
	private bool transitioning = false;
	private bool transitionsFinished = false;
	private Button[] buttons;
	private Text[] texts;
	private Text chatText;

	void Start() {
		//startTime = Time.time;
		texts = GameObject.Find("Canvas").GetComponentsInChildren<Text>();
		buttons = GameObject.Find("Canvas").GetComponentsInChildren<Button>();
		for (int i = 0; i < buttons.Length; i++) {
			buttons[i].gameObject.SetActive(false);
		}
		chatText = chatbubble.GetComponentInChildren<Text>();
	}


	void Update() {
		print("Im running");
		if (!transitioning && !transitionsFinished && chatText != null) {
			transitioning = true;
			if (fadeIn) {
				StartCoroutine(FadeTextToFullAlpha(1f, chatText));
			}
			else {
				StartCoroutine(FadeTextToZeroAlpha(1f, chatText));
			}
		}
		else if(chatText == null && !transitionsFinished)
		{
			chatText = chatbubble.GetComponentInChildren<Text>();
		}

		if (Input.anyKeyDown && !transitionsFinished)
		{
			StopAllCoroutines();
			SkipStory();
		}
	}

	public void SkipStory()
	{
		Color fadedIn = new Color (1f, 1f, 1f, 1f);
		GameObject.Find("MainMenuBackground").GetComponent<SpriteRenderer>().color = fadedIn;
		texts[0].GetComponent<Text>().color = fadedIn;
		texts[1].GetComponent<Text>().color = fadedIn;
		ChatBubbleInactiveAndLoadButtons();
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
			texts[0].GetComponent<TextFadeIn>().enabled = true;
			texts[1].GetComponent<TextFadeIn>().enabled = true;
			StartCoroutine(BringInButtons());
		}

		if (currentTextSlot == 1) {
			GameObject.Find("MainMenuBackground").GetComponent<FadeIn>().enabled = true;
		}

		fadeIn = true;
		transitioning = false;
	}

	public IEnumerator BringInButtons() {
		yield return new WaitForSeconds(3);
		ChatBubbleInactiveAndLoadButtons();
	}

	void ChatBubbleInactiveAndLoadButtons()
	{
		chatbubble.SetActive(false);
		for (int i = 0; i < buttons.Length; i++)
		{
			if(i != 1 || (i == 1 && PlayerPrefs.HasKey("NormalModeComplete"))){
				buttons[i].gameObject.SetActive(true);
			}
		}
	}

	public void Quit(){
		//If we are running in a standalone build of the game
		#if UNITY_STANDALONE
		//Quit the application
		Application.Quit();
		#endif

		//If we are running in the editor
		#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
}