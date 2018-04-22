using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour {
	public static int weaponInHand = 1;
	public static bool PickedUpSpoon = false;
	public static bool PickedUpKnife = false;
	public bool pickedUpSpoon = false;
	public bool pickedUpKnife = false;

	private List<Weapon> weapons = new List<Weapon>();

	private void Awake() {
		pickedUpSpoon = PickedUpSpoon;

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
		if (weaponSelected != weaponInHand && ((weaponSelected == 2 && pickedUpSpoon) || weaponSelected != 2) && ((weaponSelected == 3 && pickedUpKnife) || weaponSelected != 3)) {
			/*
			Vector3 localPosition = new Vector3();
			Quaternion localRotation = new Quaternion();
			Vector3 localScale = new Vector3();
			foreach (Transform child in GameObject.Find("Weapon").transform)
			{
				localPosition = child.localPosition;
				localRotation = child.localRotation;
				GameObject.Destroy(child.gameObject);
			}
			GameObject weapon = Instantiate(weapons[weaponSelected - 1], GameObject.Find("Weapon").transform);
			weapon.transform.localPosition = localPosition;
			weapon.transform.localRotation = localRotation;
			*/

			weapons[weaponInHand - 1].gameObject.SetActive(false);
			weapons[weaponSelected - 1].gameObject.SetActive(true);

			weaponInHand = weaponSelected;
		}
		else
		{
			print("Stopped it");
		}
	}

	private void OnDisable() {
		PickedUpSpoon = pickedUpSpoon;
	}
}
