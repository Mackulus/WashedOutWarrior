using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	public List<Vector2> nodes = new List<Vector2>();
	public float speed = 1f, jumpStrength = 1000f, cooldown = 1f;

	private bool isGrounded = false;
	private float curCooldown = 0;

	// Use this for initialization
	void Start () {
		if (speed > 0) {
			speed *= -1;
			Flip();
		}

		InvokeRepeating("Jump", 1f, 2f);
	}

	private void FixedUpdate() {
		Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 10f, 9);
		foreach (Collider2D collision in collisions) {
			if (collision.gameObject.CompareTag("Player")) {
				Vector2 temp = collision.gameObject.transform.position - transform.position;
				if ((temp.x < 0 && speed > 0) || (temp.x > 0 && speed < 0)) {
					Flip();
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (curCooldown > 0) curCooldown -= Time.deltaTime;
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Ground")) {
			isGrounded = true;
		}
	}
	private void OnCollisionStay2D(Collision2D collision) {
		if (collision.collider.CompareTag("Player")) {
			if (curCooldown <= 0) {
				Jump(new Vector2(-2,0));
				curCooldown += cooldown;
			}
		}
	}
	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.collider.CompareTag("Ground")) {
			isGrounded = false;
		}
	}

	private void Jump() { Jump(Vector2.zero); }
	private void Jump(Vector2 offset) {
		if (isGrounded) {
			GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(jumpStrength, speed * jumpStrength) - offset);
		}
	}

	void Flip() {
		speed *= -1;
		/*
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
		*/
	}
}