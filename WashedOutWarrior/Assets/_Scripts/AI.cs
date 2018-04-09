using System.Collections.Generic;
using UnityEngine;

public class AI : Listener {
	public List<Vector2> nodes = new List<Vector2>();
	public float speed = 1f, jumpStrength = 500f;
    private bool movingLeft = true, isGrounded = false;
	public HealthBar healthBar;

    // Use this for initialization
    void Start () {
		if (healthBar != null) {
			healthBar.deathListeners.Add(this);
		}

		//Don't ask why this is notted
		if (!FindPlayer()) {
			if (movingLeft) {
				InvokeRepeating("JumpLeft", 1f + Random.Range(0f,1f), 5f);
			}
			else {
				InvokeRepeating("JumpRight", 1f + Random.Range(0f, 1f), 5f);
			}
		}
		else {
			//print("Player not found");
		}
    }

    private bool FindPlayer() {
        float viewRange = 50f;
        //See if player is within viewRange units
        Collider2D[] collisions = new Collider2D[1];
		if (Physics2D.OverlapCircleNonAlloc(transform.position, viewRange, collisions, 9) >= 1) {
			//print("Player in Range");
			//Difference in position between this AI and the Player
			Vector2 temp = collisions[0].gameObject.transform.position - transform.position;
			//Move AI in right direction
			if (temp.x < 0) {
				print("Player to Left");
				if (!movingLeft) {
					CancelInvoke("JumpRight");
					InvokeRepeating("JumpLeft", 1f, 5f);
				}
				movingLeft = true;
			}
			else if (temp.x > 0) {
				print("Player to Right");
				if (movingLeft) {
					CancelInvoke("JumpLeft");
					InvokeRepeating("JumpRight", 1f, 5f);
				}
				movingLeft = false;
			}
			return true;
		}
		else {
			Invoke("FindPlayer", .25f);
			return false;
		}
		
    }

	private void OnTriggerEnter2D(Collider2D collision) {
		if (healthBar != null && collision.gameObject.CompareTag("Weapon")) {
			healthBar.OnDamage(2);
			FindPlayer();
			if (movingLeft) {
				CancelInvoke("JumpLeft");
				GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(2 * speed * jumpStrength, 0));
				InvokeRepeating("JumpLeft", 2.5f, 5f);
			}
			else {
				CancelInvoke("JumpRight");
				GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-2 * speed * jumpStrength, 0));
				InvokeRepeating("JumpRight", 2.5f, 5f);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Ground")) {
			isGrounded = true;
		}
		else if (collision.collider.CompareTag("Player")) {
			healthBar.OnDamage();
			if (movingLeft) {
				CancelInvoke("JumpLeft");
				GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(2 * speed * jumpStrength, 0));
				InvokeRepeating("JumpLeft", 2.5f, 5f);
			}
			else {
				CancelInvoke("JumpRight");
				GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(-2 * speed * jumpStrength, 0));
				InvokeRepeating("JumpRight", 2.5f, 5f);
			}
		}
	}
	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.collider.CompareTag("Ground")) {
			isGrounded = false;
		}
	}

	private void JumpLeft() { Jump(new Vector2(-1,1)); }
    private void JumpRight() { Jump(Vector2.one); }
    private void Jump(Vector2 direction) {
		if (isGrounded) {
			GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(direction.x * speed * jumpStrength, direction.y * jumpStrength));
		}
        FindPlayer();
    }


	override public void OnHear(GameObject g) {
		gameObject.SetActive(false);
	}
}