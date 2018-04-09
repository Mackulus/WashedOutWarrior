using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : Listener {
	public HealthBar healthBarPlayer = null;
	public List<HealthBar> healthBarBoss = null;
	public bool defeatAllBosses = false;
	private List<bool> defeatedBosses = null;

	private void Awake() {
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Boss")) {
			healthBarBoss.Add(g.GetComponent<AI>().healthBar);
		}

		healthBarBoss.Capacity = healthBarBoss.Count;
		defeatedBosses = new List<bool>(healthBarBoss.Count);

		if (healthBarPlayer == null && GameObject.FindGameObjectWithTag("Player") != null) {
			healthBarPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().healthBar;
		}
	}

	// Use this for initialization
	void Start () {
		foreach (HealthBar hB in healthBarBoss) {
			hB.deathListeners.Add(this);
		}
		healthBarPlayer.deathListeners.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void OnHear(GameObject g) {
		print("Message Received");
		if (g.CompareTag("Player")) {
			//GameOver
			SceneManager.LoadScene("MainMenu");
		}
		else {
			if (defeatAllBosses) {
				//TODO: Implement detecting which boss was killed

			}
			else {
				SceneManager.LoadScene("MainMenu");
			}
		}
	}
}