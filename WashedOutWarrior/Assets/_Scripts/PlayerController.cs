using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public HealthBar healthBar;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Enemy")) {
			healthBar.OnDamage();
		}
		/*
		if (collision.collider.CompareTag("Boss")) {
			healthBar.OnDamage(2);
		}*/
	}
}
