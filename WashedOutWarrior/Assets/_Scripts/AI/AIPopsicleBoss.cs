using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPopsicleBoss : Listener {

	public GameObject projectile;
	public HealthBar healthBar;
	public float bulletImpulse;
	private int bulletOrigins;
	private System.Random rand;
	private Transform parent;
	private int originPoint;

	// Use this for initialization
	void Start () {
		if (healthBar != null) {
			healthBar.deathListeners.Add(this);
		}
		bulletOrigins = GameObject.Find("BulletSpawners").transform.childCount;
		print("Num origins " + bulletOrigins);
		rand = new System.Random();
		InvokeRepeating("ChooseWhere", 0.2f, 0.2f);
		InvokeRepeating("FireAll", 10f, 5f);
	}

	void ChooseWhere()
	{
		Fire(rand.Next(1, bulletOrigins));
	}

	void FireAll()
	{
		for (int i = 0; i < bulletOrigins; i++)
		{
			Fire(i+1);
		}
	}

	void Fire(int originPoint)
	{
		originPoint = rand.Next(1, bulletOrigins);
		parent = transform.Find("BulletSpawners").transform.Find("Spot"+originPoint.ToString());
		GameObject bullet = Instantiate(projectile, parent);
		bullet.transform.localPosition = new Vector3(0,0,0);
		bullet.transform.localScale = new Vector3(5,5,5);
		bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * -bulletImpulse, ForceMode2D.Impulse);
	}

	override public void OnHear(GameObject g) {
		Destroy(this.gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Player")) {
			healthBar.OnDamage();
		}
		else if (collision.collider.CompareTag("BulletRicochet")) {
			print("Uh oh");
			healthBar.OnDamage();
			Destroy(collision.collider.gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Weapon")) {
			healthBar.OnDamage();
		}
	}
}
