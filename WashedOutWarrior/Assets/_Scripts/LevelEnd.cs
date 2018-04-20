using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : Listener {
	public HealthBar healthBarPlayer = null;
	public List<HealthBar> healthBarBoss = null;
	public bool defeatAllBosses = false;
	private List<bool> defeatedBosses = null;
	public Text winText, loseText;

	private void Awake() {
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Boss")) {
			if (g.name == "IceCreamBoss")
			{
				healthBarBoss.Add(g.GetComponent<AIPopsicleBoss>().healthBar);
			}
			else
			{
				healthBarBoss.Add(g.GetComponent<AISensors>().healthBar);
			}
		}

		healthBarBoss.Capacity = healthBarBoss.Count;
		defeatedBosses = new List<bool>(healthBarBoss.Count);
		
		//healthBarPlayer = Camera.main.transform.GetChild(1).GetComponent<PlayerController>().healthBar;
		//GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().healthBar;
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
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene("MainMenu");
		}
	}

	public override void OnHear(GameObject g) {
		//print("Message Received");
		if (g.GetComponent<HealthBar>() == healthBarPlayer) {
			//GameOver
			loseText.gameObject.SetActive(true);
			SceneManager.LoadScene("MainMenu");
		}
		else {
			if (defeatAllBosses) {
				//TODO: Implement detecting which boss was killed
				winText.gameObject.SetActive(true);
				SceneManager.LoadScene("MainMenu");
			}
			else {
				winText.gameObject.SetActive(true);
				SceneManager.LoadScene("MainMenu");
			}
		}
	}
}