using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	public int maxHealth;
	public float Damage = 0f;

	public Sprite[] spriteIcons;
	private GameObject[] healthBar;

	// Use this for initialization
	void Start () {
		healthBar = new GameObject[maxHealth];
		for(int ix = 0; ix < healthBar.Length; ix++) {
			float scaleUpFactor = 10f;
			GameObject tempGO = new GameObject("HealthIcon" + ix);
			tempGO.transform.parent = gameObject.transform;
			tempGO.transform.localPosition = new Vector2(ix, 0);
			tempGO.AddComponent<SpriteRenderer>();
			tempGO.GetComponent<SpriteRenderer>().sprite = spriteIcons[0];
			Vector2 tempScale = new Vector2(scaleUpFactor, scaleUpFactor);
			tempGO.transform.localScale = tempScale;
		}
	}
}
