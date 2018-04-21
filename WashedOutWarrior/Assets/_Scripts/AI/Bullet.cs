using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private bool hasBeenHit = false;
	private bool hasHitPlayer = false;

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision(11, 10, true);
		Physics2D.IgnoreLayerCollision(13, 10, true);
		Physics2D.IgnoreLayerCollision(11, 11, true);
		Physics2D.IgnoreLayerCollision(12, 11, true);
		StartCoroutine(BulletDestroy());
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.name.Contains("Table Spoon") && !hasBeenHit && GameObject.Find("Gordo").GetComponent<PlayerController>().ReturnSwinging() == true) {
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
		if ((collision.gameObject.CompareTag("Bullet") && this.transform.CompareTag("BulletRicochet")) ||
			(collision.gameObject.CompareTag("BulletRicochet") && this.transform.CompareTag("Bullet")) ||
			collision.gameObject.CompareTag("Ground"))
		{
			Destroy(this.gameObject);
		}
	}

	public void HitPlayer()
	{
		hasHitPlayer = true;
	}

	public bool HasHitPlayer()
	{
		return hasHitPlayer;
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
