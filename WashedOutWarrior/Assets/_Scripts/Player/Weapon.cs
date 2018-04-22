using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public PlayerController player;

	public float knockbackForce = 200f;
	public int speed = 10;
	public int damage = 1;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (gameObject.CompareTag("Weapon") && (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))) {
			if (player.IsFacingLeft()) {
				collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-knockbackForce, 1));
			}
			else {
				collision.gameObject.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(knockbackForce, 1));
			}
		}
	}
}
