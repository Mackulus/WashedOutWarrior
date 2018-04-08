using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Listener {
	public HealthBar healthBar;
	public float maxWeaponAngle = 180f;
	private float curWeaponAngle = 0f;
	private bool isSwinging = false;

	public GordoMovement gordo;
	public CopyTransform weapon;

	// Use this for initialization
	void Start () {
		if (healthBar != null) {
			healthBar.deathListeners.Add(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Z) && !isSwinging) {
			StartCoroutine(SwingWeapon());
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Enemy")) {
			healthBar.OnDamage();
		}
		else if (collision.collider.CompareTag("HealthPickup")) {
			collision.collider.gameObject.SetActive(false);
			healthBar.OnDamage(-10);
		}
		/*
		else if (collision.collider.CompareTag("Boss")) {
			healthBar.OnDamage(2);
		}*/
	}

	public IEnumerator SwingWeapon() {
		isSwinging = true;
		weapon.transform.GetChild(0).tag = "Weapon";
		print("SwingWeapon");
		int n = 0;
		float swingIncrement = 7.5f;
		while (curWeaponAngle < maxWeaponAngle) {
			yield return new WaitForFixedUpdate();
			Vector3 tempVec = weapon.rotOffset;
			tempVec += new Vector3(0,0, swingIncrement);
			curWeaponAngle += swingIncrement;
			n++;
			weapon.rotOffset = tempVec;
		}
		weapon.rotOffset -= new Vector3(0, 0, n * swingIncrement);
		curWeaponAngle = 0;
		weapon.transform.GetChild(0).tag = "Untagged";
		isSwinging = false;
	}

	override public void OnHear() {

	}
}
