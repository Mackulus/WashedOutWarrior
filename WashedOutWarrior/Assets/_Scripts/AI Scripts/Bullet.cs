using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, 2f);
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.name.Contains("Table Spoon")) {
			this.GetComponent<Rigidbody2D>().velocity *= -1;
			Vector2 localScale = this.transform.localScale;
			localScale.y *= -1;
			this.transform.localScale = localScale;
		}
	}

}
