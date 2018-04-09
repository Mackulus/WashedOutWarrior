using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	public int maxHealth = 20;
	public int damage = 0;
	public List<Listener> deathListeners = new List<Listener>();

	public Sprite[] spriteIcons = null;

	private GameObject[] healthBar = null;

	// Use this for initialization
	void Start () {
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

	private void Update() {
		if (damage >= maxHealth) {
			foreach(Listener l in deathListeners) {
				l.OnHear(gameObject);
			}
		}
	}

	public void OnDamage(int d = 1) {
		if (d > 0 && damage < maxHealth) {
			for (int i = 0; i < d && damage/2 < healthBar.Length; i++) {
				damage++;
				if (damage % 2 == 1) {
					healthBar[damage / 2].GetComponent<SpriteRenderer>().sprite = spriteIcons[1];
				}
				else if (damage / 2 > 0) {
					healthBar[(damage / 2) - 1].GetComponent<SpriteRenderer>().sprite = spriteIcons[2];
				}
			}
		}
		else if (d < 0 && damage > 0) {
			d = Mathf.Abs(d);
			
			for (int i = d; i >= 0 && damage/2 >= 0; i--) {
				damage--;
				//Triggers an out of index error on rare occasion, unsure why
				healthBar[damage / 2].GetComponent<SpriteRenderer>().sprite = damage % 2 == 1 ? spriteIcons[1] : spriteIcons[0];
			}
			if (damage < 0) damage = 0;
		}
	}
}