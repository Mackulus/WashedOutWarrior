using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(BulletDestroy());
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.name.Contains("Table Spoon")) {
			StopAllCoroutines();
			this.GetComponent<Rigidbody2D>().velocity *= -1;
			Vector2 localScale = this.transform.localScale;
			localScale.y *= -1;
			this.transform.localScale = localScale;
			this.transform.tag = "BulletRicochet";
			this.gameObject.layer = 13;
			StartCoroutine(BulletDestroy());
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		print(collision.collider.tag);
		if ((collision.gameObject.CompareTag("Bullet") && this.transform.CompareTag("BulletRicochet")) ||
			(collision.gameObject.CompareTag("BulletRicochet") && this.transform.CompareTag("Bullet")))
		{
			Destroy(this.gameObject);
		}
		if (collision.gameObject.CompareTag("LollipopWall"))
		{
			print("Trying to ignore");
			Physics2D.IgnoreLayerCollision(11, 10, true);
			Physics2D.IgnoreLayerCollision(13, 10, true);
		}
		if (collision.gameObject.CompareTag("Enemy") && this.transform.CompareTag("Bullet"))
		{
			Physics2D.IgnoreLayerCollision(12, 11, true);
		}
		if (collision.gameObject.CompareTag("Bullet") && this.transform.CompareTag("Bullet"))
		{
			Physics2D.IgnoreLayerCollision(11, 11, true);
		}
	}

	void DestroySelf()
	{
		Destroy(this.gameObject);
	}

	IEnumerator BulletDestroy()
	{
		yield return new WaitForSeconds(2f);
		Destroy(this.gameObject);
	}

}
