using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	public int maxHealth = 20;
	public int damage = 0;
	public List<Listener> deathListeners = new List<Listener>();

	public Sprite[] spriteIcons;

	private GameObject[] healthBar;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("CalorieBurner") == 1 && (transform.parent.CompareTag("Enemy") || transform.parent.CompareTag("Boss"))){
			maxHealth *= 2;
		}
		healthBar = new GameObject[maxHealth/2];
		for(int ix = 0; ix < healthBar.Length; ix++) {
			float scaleUpFactor = 10f;
			GameObject tempGO = new GameObject("HealthIcon" + ix);
			tempGO.transform.parent = gameObject.transform;
			tempGO.transform.localPosition = new Vector2(ix, 0);
			tempGO.AddComponent<SpriteRenderer>();
			tempGO.GetComponent<SpriteRenderer>().sprite = spriteIcons[0];
			Vector2 tempScale = new Vector2(scaleUpFactor, scaleUpFactor);
			tempGO.transform.localScale = tempScale;
			healthBar[ix] = tempGO;
		}
	}

	public void OnDamage(int d = 1) {
		//print("Took (" + d + ") damage");

		damage += d;
		if (damage < 0){
			damage = 0;
		}
		UpdateBar();

		if (damage >= maxHealth) {
			foreach (Listener l in deathListeners) {
				l.OnHear(gameObject);
			}
		}
	}

	public void UpdateBar() {
		for(int ix = 0; ix < healthBar.Length; ix++) {
			int tempDamage = damage - (2 * ix);
			if (tempDamage <= 0) {
				healthBar[ix].GetComponent<SpriteRenderer>().sprite = spriteIcons[0];
			}
			else if (tempDamage == 1) {
				healthBar[ix].GetComponent<SpriteRenderer>().sprite = spriteIcons[1];
			}
			else /* tempDamage >= 2 */
			{
				healthBar[ix].GetComponent<SpriteRenderer>().sprite = spriteIcons[2];
			}
		}
	}
}