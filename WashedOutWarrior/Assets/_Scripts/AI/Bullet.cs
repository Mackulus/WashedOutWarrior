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

	private void OnCollisionEnter2D(Collision2D collision) {
		print(collision.collider.tag);
		if (collision.gameObject.CompareTag("Bullet"))
		{
			Destroy(this.gameObject);
		}
		if (collision.gameObject.CompareTag("LollipopWall"))
		{
			print("Trying to ignore");
			Physics2D.IgnoreLayerCollision(11, 10, true);
		}
	}

}
