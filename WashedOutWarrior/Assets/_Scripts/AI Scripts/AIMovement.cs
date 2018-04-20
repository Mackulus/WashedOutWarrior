using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {
	public AISensors sensor;
	public bool isTypeJumping = false;
	public float speed = 1f, jumpStrength = 500f;
	private bool isMoving = false, isGrounded = false;

	// Use this for initialization
	private void Awake() {
		sensor = gameObject.GetComponent<AISensors>();
	}

	private void Start() {
		
	}

	// Update is called once per frame
	void Update () {
		if ((!isMoving) && sensor.playerRelPos != Vector2.zero) {
			isMoving = true;
			if (sensor.playerRelPos.y > 0 && isTypeJumping) {
				Jump(Vector2.up);
			}
			else if (sensor.playerRelPos.x < 0) {
				if (isTypeJumping) {
					Jump(new Vector2(-1, 1));
				}
				else {
					Jump(Vector2.left);
				}
			}
			else {
				if (isTypeJumping) {
					Jump(Vector2.one);
				}
				else {
					Jump(Vector2.right);
				}
			}

		}
		else if (gameObject.GetComponent<Rigidbody2D>().GetPointVelocity(transform.position).x <= .1f) {
			isMoving = false;
		}
	}

	private void Jump(Vector2 direction) {
		if (isGrounded) {
			GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(direction.x * speed * jumpStrength, direction.y * jumpStrength));
		}
	}
	
	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Ground")) {
			isGrounded = true;
		}
		else if (collision.collider.CompareTag("Player")) {
			if (sensor.playerRelPosRaw.x < 0) {
				Jump(Vector2.left);
			}
			else {
				Jump(Vector2.right);
			}
		}
		else if (collision.collider.CompareTag("Bullet")) {
			Destroy(collision.collider.gameObject);
		}
	}
	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.collider.CompareTag("Ground")) {
			isGrounded = false;
		}
	}
}
