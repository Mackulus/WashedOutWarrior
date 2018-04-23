using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locked : MonoBehaviour {
	public string playerPref = "";


	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt(playerPref) == 1) {
			this.gameObject.SetActive(true);
		}
		else {
			this.gameObject.SetActive(false);
		}
	}
}
