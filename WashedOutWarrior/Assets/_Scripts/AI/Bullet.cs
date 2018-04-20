using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private bool hasBeenHit = false;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision(11, 10, true);
		Physics2D.IgnoreLayerCollision(13, 10, true);
		Physics2D.IgnoreLayerCollision(11, 11, true);
		Physics2D.IgnoreLayerCollision(12, 11, true);
		StartCoroutine(BulletDestroy());
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.name.Contains("Table Spoon") && !hasBeenHit) {
			StopAllCoroutines();
			hasBeenHit = true;
			this.GetComponent<Rigidbody2D>().velocity *= -2;
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
