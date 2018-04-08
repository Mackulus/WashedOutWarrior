using UnityEngine;

public class HealthBar : MonoBehaviour {
	public int maxHealth = 20;
	public int damage = 0;

	public Sprite[] spriteIcons;

	private GameObject[] healthBar;

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

		//OnDamage(10);
	}

	public void OnDamage(int d = 1) {
		if (d > 0) {
			for (int i = 0; i < healthBar.Length && i < d && damage <= maxHealth; i++) {
				damage++;
				if (damage % 2 == 1) {
					healthBar[damage / 2].GetComponent<SpriteRenderer>().sprite = spriteIcons[1];
				}
				else {
					healthBar[(damage / 2) - 1].GetComponent<SpriteRenderer>().sprite = spriteIcons[2];
				}
			}
			if (damage > maxHealth) {
				damage = maxHealth;
				//Death();
				return;
			}
		}
		else if (d < 0 && damage > 0) {
			d = Mathf.Abs(d);
			
			for (int i = d; i >= 0 && damage >= 0; i--) {
				damage--;
				healthBar[damage / 2].GetComponent<SpriteRenderer>().sprite = damage % 2 == 1 ? spriteIcons[1] : spriteIcons[0];
			}
			if (damage < 0) damage = 0;
		}
	}
}
