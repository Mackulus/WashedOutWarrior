using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour {
	public static int weaponInHand = 1;
	public static bool PickedUpSpoon = false;
	public static bool PickedUpKnife = false;
	public bool pickedUpSpoon = PickedUpSpoon;
	public bool pickedUpKnife = PickedUpKnife;

	private List<Weapon> weapons = new List<Weapon>();

	private void Awake() {
		Weapon[] w = gameObject.GetComponentsInChildren<Weapon>();
		weapons.AddRange(w);
	}

	// Use this for initialization
	void Start () {
		foreach (Weapon w in weapons){
			w.gameObject.SetActive(false);
		}
		weapons[weaponInHand - 1].gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3)) {
			ChangeWeapon(Int32.Parse(Input.inputString));
		}
	}

	public Weapon CurrentWeapon() {
		return weapons[weaponInHand - 1];
	}

	public void AddWeapon(Weapon w) {
		weapons.Add(w);
	}

	public void ChangeWeapon(int weaponSelected) {
		print("Selected " + weaponSelected);
		print("In hand " + weaponInHand);
		//print("Spoon? " + pickedUpSpoon);
		if (weaponSelected != weaponInHand && (pickedUpSpoon || weaponSelected != 2) && (pickedUpKnife || weaponSelected != 3)) {
			weapons[weaponInHand - 1].gameObject.SetActive(false);
			weapons[weaponSelected - 1].gameObject.SetActive(true);

			weaponInHand = weaponSelected;
		}
		else {
			print("Stopped it");
		}
	}
}
