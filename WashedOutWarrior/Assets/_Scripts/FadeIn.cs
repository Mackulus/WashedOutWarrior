using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

	public bool fadeIn = true;
	public float minimum = 0.0f;
	public float maximum = 1f;
	public float duration = 5.0f;
	private float startTime;
	public SpriteRenderer sprite;
	public Image image;
	void Start() {
		startTime = Time.time;
	}
	void Update() {
		float t = (Time.time - startTime) / duration;
		if (fadeIn)
		{
			if (image == null)
			{
				sprite.color = new Color(1f,1f,1f,Mathf.SmoothStep(minimum, maximum, t)); 
			}
			else
			{
				image.color = new Color(1f,1f,1f,Mathf.SmoothStep(minimum, maximum, t));	
			}
		}
		else
		{
			if (image == null)
			{
				sprite.color = new Color(1f,1f,1f,Mathf.SmoothStep(maximum, minimum, t));
			}
			else
			{
				image.color = new Color(1f,1f,1f,Mathf.SmoothStep(maximum, minimum, t));
			}
		}
	}

	public void ResetStart()
	{
		startTime = Time.time;
	}
}
