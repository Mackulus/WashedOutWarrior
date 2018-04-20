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
		InvokeRepeating("Fire", 0.5f, 0.5f);
	}

	void Fire()
	{
		originPoint = rand.Next(1, bulletOrigins);
		parent = transform.Find("BulletSpawners").transform.Find("Spot"+originPoint.ToString());
		GameObject bullet = Instantiate(projectile, parent);
		bullet.transform.localPosition = new Vector3(0,0,0);
		bullet.transform.localScale = new Vector3(5,5,5);
		bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * -bulletImpulse, ForceMode2D.Impulse);
	}

	override public void OnHear(GameObject g) {
		gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
