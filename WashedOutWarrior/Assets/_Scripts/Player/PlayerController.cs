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
	public WeaponSelect weapons;

	private int calorieBurner;

	private void Awake() {
		if (healthBar != null) {
			healthBar.deathListeners.Add(this);
		}
	}

	void Start()
	{
		calorieBurner = PlayerPrefs.GetInt("CalorieBurner");
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.activeSelf && Input.GetKeyDown(KeyCode.Z) && !isSwinging) {
			StartCoroutine(SwingWeapon());
		}
	}

	public bool ReturnSwinging() {
		return isSwinging;
	}

	private void FixedUpdate() {
		//Checking for null weapon so that the main screen can use the script
		if (gameObject.activeSelf && !isSwinging && weapon != null) {
			if (IsFacingLeft()) {
				weapon.posOffset = Vector3.back;
				weapon.rotOffset = Vector3.zero;
			}
			else {
				weapon.posOffset = new Vector3(1, -0.25f, -1);
				weapon.rotOffset = new Vector3(0, 0, -125);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Enemy")) {
			healthBar.OnDamage();
			if (calorieBurner == 1){
				healthBar.OnDamage();
			}
		}
		else if (collision.collider.CompareTag("HealthPickup")) {
			collision.collider.gameObject.SetActive(false);
			if (calorieBurner == 1){
				healthBar.OnDamage(-5);
			}
			else{
				healthBar.OnDamage(-10);
			}
		}
		else if (collision.collider.CompareTag("Boss")) {
			healthBar.OnDamage(2);
			if (calorieBurner == 1){
				healthBar.OnDamage(2);
			}
		}
		else if (collision.collider.CompareTag("Bullet")) {
			if(collision.gameObject.GetComponent<Bullet>().HasHitPlayer() == false)
			{
				//print("Hit");
				collision.gameObject.GetComponent<Bullet>().HitPlayer();
				Destroy(collision.collider.gameObject);
				healthBar.OnDamage();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("NewWeapon")) {
			if (collision.gameObject.name == "Table Spoon") {
				Destroy(collision.gameObject);
				weapons.pickedUpSpoon = true;
				weapons.ChangeWeapon(2);
			}
			if (collision.gameObject.name == "Knife") {
				Destroy(collision.gameObject);
				weapons.pickedUpKnife = true;
				weapons.ChangeWeapon(3);
			}
		}
	}

	public IEnumerator SwingWeapon() {
		isSwinging = true;
		weapons.CurrentWeapon().tag = "Weapon";
		//print("SwingWeapon");

		int nTimes = 10;
		if (weapons.CurrentWeapon().name == "Knife"){
			nTimes = 20;
		}
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

		weapons.CurrentWeapon().tag = "Untagged";
		isSwinging = false;
	}

	public bool IsFacingLeft() {
		return gordo.transform.localScale.x > 0;
	}

	public void CalorieBurnerMode(int onOrOff){
		//1 == on 0 == off
		PlayerPrefs.SetInt("CalorieBurner", onOrOff);
		print("Calorie burner? " + PlayerPrefs.GetInt("CalorieBurner"));
	}

	override public void OnHear(GameObject g) {
		//TODO: add Death screen
	}
}
