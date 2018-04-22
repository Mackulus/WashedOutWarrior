using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRanged:MonoBehaviour {
	public int enemySpeed;
	public int xMoveDirection;
	public GameObject projectile;
	public float bulletImpulse = 20.0f;
	public bool fired = false;
	private Transform parent;

	void Start () {
		parent = transform.Find("BulletSpawn");
	}

	// Update is called once per frame
	void Update () {
		if (xMoveDirection != 0) {
			RaycastHit2D hitRight = Physics2D.Raycast(transform.position, new Vector2(1, 0), 23f);
			RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, new Vector2(-1, 0), 23f);
			if (hitRight.collider != null || hitLeft.collider != null) {
				if (hitRight.collider != null && hitRight.collider.CompareTag("Player") && !fired){
					FireDirection(1);
				}
				else if (hitLeft.collider != null && hitLeft.collider.CompareTag("Player") && !fired){
					FireDirection(-1);
				}
				else if (!fired){
					if(hitRight.collider != null){
						CheckForFlip(hitRight, 1);
					}
					else{
						CheckForFlip(hitLeft, -1);
					}
				}
			}
			else {
				gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0)*enemySpeed;
			}
		}
		else {
			if (!fired) {
				Fire(-1);
				Fire(1);
				fired = true;
				Invoke("ResetFired", 1f);
			}
		}
	}

	void FireDirection(int direction) {
		Fire(direction);
		fired = true;
		Invoke("ResetFired", 1f);
	}

	void CheckForFlip(RaycastHit2D hit, int direction)
	{
		if(hit.collider.tag != "Bullet" && hit.collider.tag != "BulletRicochet" && hit.collider.tag != "Weapon"){
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0)*enemySpeed;
			if (hit.distance < 5f && hit.collider.name != this.name && direction == xMoveDirection){
				FlipEnemy();
			}
		}
	}

	void FlipEnemy() {
		//print("Direction : " + xMoveDirection);
		if (xMoveDirection > 0) {
			//print("flipped");
			xMoveDirection = -1;
		}
		else {
			xMoveDirection = 1;
		}
	}

	void Fire(int direction) {
		GameObject bullet = Instantiate(projectile, parent);
		bullet.transform.localPosition = new Vector2(parent.transform.localPosition.x, parent.transform.localPosition.y);
		if (direction == 1 ) {
			//print("In here");
			Vector2 localScale = bullet.transform.localScale;
			localScale.y *= -1;
			bullet.transform.localScale = localScale;
			bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletImpulse, ForceMode2D.Impulse);
		}
		else {
			bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * -bulletImpulse, ForceMode2D.Impulse);
		}
	}

	void ResetFired() {
		fired = false;
	}
}