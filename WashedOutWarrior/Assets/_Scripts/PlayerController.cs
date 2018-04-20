using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Listener {
	public HealthBar healthBar;
	public GameObject[] weapons;
	public float maxWeaponAngle = 180f;
	private float curWeaponAngle = 0f;
	private bool isSwinging = false;

	public GordoMovement gordo;
	public CopyTransform weapon;

	private void Awake() {
		if (healthBar != null) {
			healthBar.deathListeners.Add(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Z) && !isSwinging) {
			StartCoroutine(SwingWeapon());
		}
		if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))
		{
			ChangeWeapon(Int32.Parse(Input.inputString) - 1);
		}
	}

	private void ChangeWeapon(int weaponSelected)
	{
		Vector3 localPosition = new Vector3();
		Quaternion localRotation = new Quaternion();
		Vector3 localScale = new Vector3();
		foreach (Transform child in GameObject.Find("Weapon").transform){
			localPosition = child.localPosition;
			localRotation = child.localRotation;
			localScale = child.localScale;
			GameObject.Destroy(child.gameObject);
		}
		GameObject weapon = Instantiate(weapons[weaponSelected], GameObject.Find("Weapon").transform); 
		weapon.transform.localPosition = localPosition;
		weapon.transform.localRotation = localRotation;
		weapon.transform.localScale = localScale;

	}

	private void FixedUpdate() {
		if (IsFacingLeft()) {
			weapon.posOffset = Vector3.back;
			weapon.rotOffset = Vector3.zero;
		}
		else {
			weapon.posOffset = new Vector3(1, -0.25f, -1);
			weapon.rotOffset = new Vector3(0, 0, -125);
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
		else if (collision.collider.CompareTag("Boss")) {
			healthBar.OnDamage(2);
		}
		else if (collision.collider.CompareTag("Bullet")) {
			healthBar.OnDamage();
			Destroy(collision.collider.gameObject);
		}
	}

	public IEnumerator SwingWeapon() {
		isSwinging = true;
		weapon.transform.GetChild(0).tag = "Weapon";
		//print("SwingWeapon");

		int nTimes = 10;
		float startOffset, endOffset;
		if (IsFacingLeft()) {
			//Swing in a 140 degree left-facing arc
			startOffset = 0f;
			endOffset = 140f;
			for(int ix = (int)startOffset; ix <= endOffset; ix += 140 / nTimes) {
				yield return new WaitForFixedUpdate();
				weapon.rotOffset = Vector3.forward * ix;
			}
		}
		else {
			//Swing in a 140 degree right-facing arc
			startOffset = -125f;
			endOffset = -265f;
			for (int ix = (int)startOffset; ix >= endOffset; ix -= 140 / nTimes) {
				yield return new WaitForFixedUpdate();
				weapon.rotOffset = Vector3.forward * ix;
			}
		}
		
		weapon.transform.GetChild(0).tag = "Untagged";
		isSwinging = false;
	}

	public bool IsFacingLeft() {
		return gordo.transform.localScale.x > 0;
	}

	override public void OnHear(GameObject g) {
		//TODO: add Death screen
	}
}
