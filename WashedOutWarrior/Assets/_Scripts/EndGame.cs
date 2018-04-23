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
	private bool congratulationsDisplayed = false;
	private Text[] texts;
	private SceneFader fadeScr;
	Transform[] children;
	private Text chatText;
	private AudioSource background;
	

	void Start() {
		background = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
		if (background != null && background.isPlaying)
		{
			background.Pause();
		}
		PlayerPrefs.SetInt("NormalModeComplete", 1);
		fadeScr = GameObject.FindObjectOfType<SceneFader>();
		gordo = GameObject.Find("OldGordo");
		children = GameObject.Find("OldEnemies").GetComponentsInChildren<Transform>();
		texts = GameObject.Find("Canvas").GetComponentsInChildren<Text>();
		chatText = chatbubble.GetComponentInChildren<Text>();
	}


	void Update() {
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

		if (Input.anyKeyDown){
			if (!transitionsFinished && !congratulationsDisplayed){
				StopAllCoroutines();
				EnsureAllSpritesAreOff();
				SkipStory();
			}
			else if (congratulationsDisplayed){
				GameObject.Find("Gordo").GetComponent<Animator>().SetTrigger("Hi_01");
				fadeScr.EndScene("MainMenu");
			}
		}
	}

	public void SkipStory()
	{
		Color fadedIn = new Color (1f, 1f, 1f, 1f);
		texts[0].GetComponent<Text>().color = fadedIn;
		texts[1].GetComponent<Text>().color = fadedIn;
		ChatAndCongrats();
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
		ChatAndCongrats();
	}

	public void EnsureAllSpritesAreOff(){
		gordo.SetActive(false);
		GameObject.Find("OldEnemies").SetActive(false);
	}

	public void ChatAndCongrats() {
		if(background != null)
		{
			background.Play();
		}
		chatbubble.SetActive(false);
		Text text = texts[2].GetComponent<Text>();
		if (PlayerPrefs.GetInt("CalorieBurner") == 0){
			text.text = "Calorie burner unlocked!\n Are you ready for a real challenge?";
		}
		else if (PlayerPrefs.GetInt("CalorieBurner") == 1){
			text.text = "Congratulations!\n May your spork be swift and your dietary choices always appropriate";
		}
		texts[2].color = new Color (1f, 1f, 1f, 1f);
		texts[3].color = new Color (1f, 1f, 1f, 1f);
		congratulationsDisplayed = true;
	}
}