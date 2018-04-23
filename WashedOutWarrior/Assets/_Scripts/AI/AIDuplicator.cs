using UnityEngine;

public class AIDuplicator : MonoBehaviour {
	public AISensors sensor;
	public GameObject[] toDuplicate = new GameObject[2];
	//private bool duplicate = true;

	void Duplicate() {
		for (int i = 0; i < 2; i++) {
			print("Duplicating: " + i);
			GameObject temp = Instantiate(toDuplicate[i]);
			temp.transform.position = transform.position;
			temp.transform.position -= (Vector3)(sensor.playerRelPos * (1 + i));

			if (temp.GetComponent<AIDuplicator>() != null) {
				temp.GetComponent<AIDuplicator>().enabled = false;
			}
		}
	}

	/*To be used later to make a boss where some duplications are false and others are true;
	void ChangeDuplicate(bool newDuplicate) {
		duplicate = newDuplicate;
	}*/	
	
	void OnDisable() {
		if (toDuplicate != null) {
			Invoke("Duplicate", 0.01f);
		}
	}

	private void OnDestroy() {
		CancelInvoke();
	}
}
