using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GordoMovement : MonoBehaviour {

	public float gordoSpeed;
	public float gordoJumpPower;
	private float moveX, moveY;
	private bool facingLeft = true;
	Animator anim;
	private bool isWalking = false;
	//bool isGrounded;
	bool isJumping = false;

	void Start() {
		anim = GetComponent<Animator>();
        GetComponent<Rigidbody2D>().freezeRotation = true;
	}

	// Update is called once per frame
	void Update () {
		MoveGordo();
        GordoRaycast();
	}

    void MoveGordo() {
		moveX = Input.GetAxis("Horizontal");
		if (moveX != 0.0f && isWalking == false) {
			anim.ResetTrigger("Idle_01");
			anim.SetTrigger("Walk_01");
			isWalking = true;
		}
		else if (moveX == 0.0f && isWalking == true) {
			anim.ResetTrigger("Walk_01");
			anim.SetTrigger("Idle_01");
			isWalking = false;
		}

        moveY = Input.GetAxis("Vertical");
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
        if (!isJumping) {
			isJumping = true;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
		    anim.SetTrigger("Jump_01");
		    Invoke("GordoJumpForce", 0.25f);
            //isGrounded = false;
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
        print(collision.collider.tag);
    }

	void GordoRaycast()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
		//print(hit.collider.tag);
		if (hit != null && hit.collider != null)
		{
			if (hit.distance < 0.1f && hit.collider.tag == "Enemy")
			{
				//GetComponent<Rigidbody2D>().AddForce(Vector2.up*1000);
				//hit.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.right*200);
				//hit.collider.gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
				//hit.collider.gameObject.GetComponent<Rigidbody2D>().gravityScale = 20;
				//hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
				//hit.collider.gameObject.GetComponent<EnemyMove>().enabled = false;
				//Destroy(hit.collider.gameObject);
			}
            /*
			if (hit.distance < 0.1f && hit.collider.tag == "Ground" && !isJumping)
			{
				print("Hitting the ground");
				isGrounded = true;
				gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
			}
            */
		}
	}
}
