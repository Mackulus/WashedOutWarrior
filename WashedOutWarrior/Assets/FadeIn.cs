using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

	public bool fadeIn = true;
	public float minimum = 0.0f;
	public float maximum = 1f;
	public float duration = 5.0f;
	private float startTime;
	public SpriteRenderer sprite;
	void Start() {
		startTime = Time.time;
	}
	void Update() {
		float t = (Time.time - startTime) / duration;
		if (fadeIn)
		{
		sprite.color = new Color(1f,1f,1f,Mathf.SmoothStep(minimum, maximum, t));    
		}
		else
		{
			sprite.color = new Color(1f,1f,1f,Mathf.SmoothStep(maximum, minimum, t));
		}
	}

	public void ResetStart()
	{
		startTime = Time.time;
	}
}
