using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	public List<Vector2> nodes = new List<Vector2>();
	public float speed = 1f, jumpStrength = 1000f;
    private bool movingLeft = true, isGrounded = false;

    // Use this for initialization
    void Start () {
        if (movingLeft) {
            InvokeRepeating("JumpLeft", 5f, 5f);
        }
        else {
            InvokeRepeating("JumpRight", 5f, 5f);
        }
    }
    
	private void FixedUpdate() {
        //See if player is within 10f units
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 10f, 9);
		foreach (Collider2D collision in collisions) {
			if (collision.gameObject.CompareTag("Player")) {
                //Difference between this AI and the Player
                Vector2 temp = collision.gameObject.transform.position - transform.position;
                //Move AI in right direction
                if (temp.x < 0 && IsInvoking("JumpRight")) {
                    CancelInvoke("JumpRight");
                    InvokeRepeating("JumpLeft", 1f, 5f);
                }
                if (temp.x > 0 && IsInvoking("JumpLeft")) {
                    CancelInvoke("JumpLeft");
                    InvokeRepeating("JumpRight", 1f, 5f);
                }
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Ground")) {
			isGrounded = true;
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
			GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(direction.x * jumpStrength, direction.y * speed * jumpStrength));
		}
	}
}