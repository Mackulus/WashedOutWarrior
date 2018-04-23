using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicSingleton : MonoBehaviour {

	//Because we didn't originally think of Singleton for this
	//Code found here https://answers.unity.com/questions/11314/audio-or-music-to-continue-playing-between-scene-c.html

	private static BackgroundMusicSingleton instance = null;

	public static BackgroundMusicSingleton Instance {
		get { return instance; }
	}

	void Awake() {
		if (instance != null && instance != this) 
		{
			Destroy(this.gameObject);
			return;
		} 
		else 
		{
			instance = this;
		}

		DontDestroyOnLoad(this.gameObject);
	}
}
