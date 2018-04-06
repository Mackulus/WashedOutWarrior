using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	public List<Vector2> nodes = new List<Vector2>();
	public float speed = 1f, jumpStrength = 500f;
    private bool movingLeft = true, isGrounded = false;

    // Use this for initialization
    void Start () {
        FindPlayer();
        if (movingLeft) {
            InvokeRepeating("JumpLeft", 1f, 5f);
        }
        else {
            InvokeRepeating("JumpRight", 1f, 5f);
        }
    }

    private void FindPlayer() {
        float viewRange = 50f;
        //See if player is within viewRange units
        Collider2D[] collisions = new Collider2D[1];
        if (Physics2D.OverlapCircleNonAlloc(transform.position, viewRange, collisions, 9) >= 1) {
            //print("Player in Range");
            //Difference between this AI and the Player
            Vector2 temp = collisions[0].gameObject.transform.position - transform.position;
            //Move AI in right direction
            if (temp.x < 0 && !movingLeft) {
                CancelInvoke("JumpRight");
                InvokeRepeating("JumpLeft", 1f, 5f);
                movingLeft = true;
            }
            else if (temp.x > 0 && movingLeft) {
                CancelInvoke("JumpLeft");
                InvokeRepeating("JumpRight", 1f, 5f);
                movingLeft = false;
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
        FindPlayer();
    }
}