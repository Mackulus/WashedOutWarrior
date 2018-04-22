using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour {
	private List<Transform> Spawnpoints = new List<Transform>();

	public AISensors sensor;
	public GameObject child;
	public int maxSpawns = 20;

	// Use this for initialization
	void Start () {
		for (int ix = 0; ix < transform.childCount; ix++) {
			if (transform.GetChild(ix).CompareTag("Spawnpoint")) {
				Spawnpoints.Add(transform.GetChild(ix));
			}
		}
	}

	private void Update() {
		if ((!IsInvoking("Spawn")) && sensor != null && sensor.playerRelPos != Vector2.zero) {
			InvokeRepeating("Spawn", 5f, 15f);
		}
	}

	private void Spawn() {
		if (child != null) {
			foreach (Transform t in Spawnpoints) {
				Instantiate(child);
				child.transform.position = t.position;
				maxSpawns--;
			}
		}
	}
	
	private void OnDisable() {
		CancelInvoke();
	}

	private void OnDestroy() {
		CancelInvoke();
	}
}
