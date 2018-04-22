using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeIn : MonoBehaviour {

	public bool fadeIn = true;
	public float minimum = 0.0f;
	public float maximum = 1f;
	public float duration = 5.0f;
	private float startTime;
	public Text text;

	void Start() {
		startTime = Time.time;
	}

	void Update() {
		float t = (Time.time - startTime) / duration;
		if (fadeIn) {
			text.color = new Color(text.color.r,text.color.g,text.color.b,Mathf.SmoothStep(minimum, maximum, t));    
		}
		else {
			text.color = new Color(text.color.r,text.color.g,text.color.b,Mathf.SmoothStep(maximum, minimum, t));
		}
	}

	public void ResetStart() {
		startTime = Time.time;
	}
}
