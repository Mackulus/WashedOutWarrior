using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDuplicator : MonoBehaviour {
	public AISensors sensor;
	public GameObject toDuplicate;
	private bool duplicate = true;
	private bool isQuitting = false;

	void OnApplicationQuit()
	{
		isQuitting = true;
	}

	void OnDisable() {
		if (duplicate && !isQuitting && toDuplicate != null) {
			for (int i = 0; i < 2; i++) {
				GameObject temp = Instantiate(toDuplicate);
				temp.GetComponent<AIDuplicator>().enabled = false;
				temp.transform.position = (Vector2)transform.position + (sensor.playerRelPos * -2);

				/*
				Vector2 parentPosition = transform.localPosition;
				GameObject clone = Instantiate(toDuplicate.gameObject);
				
				clone.transform.localPosition = new Vector2 (parentPosition.x, parentPosition.y + 5 + i);
				clone.transform.localScale = new Vector3(transform.localScale.x / 1.5f , transform.localScale.y / 1.5f, transform.localScale.z / 1.5f);
				//Currently is specific to pizza because there is no good way to loop through components and check if they're disabled

				clone.GetComponent<BoxCollider2D>().enabled = true;
				clone.GetComponent<AISensors>().enabled = true;
				clone.GetComponent<AIMovement>().enabled = true;
				clone.GetComponent<AIDuplicator>().ChangeDuplicate(false);
				clone.GetComponentInChildren<HealthBar>().enabled = true;
				clone.GetComponentInChildren<HealthBar>().maxHealth = toDuplicate.healthBar.maxHealth/2;
				clone.GetComponentInChildren<HealthBar>().damage = 0;
				clone.SetActive(true);
				*/
			}
		}
		print("Pizza was destroyed");
	}

	//To be used later to make a boss where some duplications are false and others are true;
	void ChangeDuplicate(bool newDuplicate) {
		duplicate = newDuplicate;
	}
}
