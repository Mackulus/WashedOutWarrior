using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GordoMovement : MonoBehaviour {
	public float gordoSpeed;
	public float gordoJumpPower;
	private float moveX, moveY;
	public bool facingLeft = true;
	Animator anim;
	private bool isWalking = false;
	bool isGrounded = true;
	bool isJumping = false;

	void Start() {
		anim = GetComponent<Animator>();
        GetComponent<Rigidbody2D>().freezeRotation = true;
	}

	// Update is called once per frame
	void Update () {
		MoveGordo();
        //GordoRaycast();
	}

    void MoveGordo() {
		moveX = Input.GetAxis("Horizontal");
		moveY = Input.GetAxis("Vertical");
		if (moveX != 0.0f && isWalking == false && moveY == 0.0f) {
			anim.ResetTrigger("Idle_01");
			anim.SetTrigger("Walk_01");
			isWalking = true;
		}
		else if (moveX == 0.0f && isWalking == true && moveY == 0.0f) {
			anim.ResetTrigger("Walk_01");
			anim.SetTrigger("Idle_01");
			isWalking = false;
		}
			
        //if (Input.GetButtonDown("Jump"))
        if (moveY > 0.0F && !isJumping) {
			GordoJump();
		}

		if (moveX > 0.0f && facingLeft == true || moveX < 0.0f && facingLeft == false) {
			FlipGordo();
		}

		gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX*gordoSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
	}

	void FlipGordo() {
		facingLeft = !facingLeft;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}

	void GordoJump() {
        if (isGrounded && !isJumping) {
            isJumping = true;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		    anim.SetTrigger("Jump_01");
		    Invoke("GordoJumpForce", 0.25f);
		    Invoke("TurnJumpOff", 1.22f);
        }
	}

	void GordoJumpForce() {
		gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up*gordoJumpPower);
	}

	void TurnJumpOff() {
		isJumping = false;
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        //print(collision.collider.tag);
		if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("LollipopWall")) {
			//print("Entered");
            isGrounded = true;
        }
    }
	private void OnCollisionStay2D(Collision2D collision) {
		if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("LollipopWall")) {
			isGrounded = true;
		}
	}
	private void OnCollisionExit2D(Collision2D collision) {
		if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("LollipopWall")) {
			//print("Exited");
			isGrounded = false;
		}
    }
}