using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDuplicator : MonoBehaviour {
	public AISensors sensor;
	public GameObject toDuplicate;
	private bool duplicate = true;

	void Duplicate() {
		for (int i = 0; i < 2; i++) {
			GameObject temp = Instantiate(toDuplicate);
			if (transform != null) {
				temp.transform.position = transform.position;
			}
			if (sensor != null) {
				temp.transform.position += (Vector3)(sensor.playerRelPos * -2);
			}
		}
	}

	//To be used later to make a boss where some duplications are false and others are true;
	void ChangeDuplicate(bool newDuplicate) {
		duplicate = newDuplicate;
	}	
	
	void OnDisable() {
		if (toDuplicate != null) {
			Invoke("Duplicate", 0.01f);
		}
	}

	private void OnDestroy() {
		CancelInvoke();
	}
}
