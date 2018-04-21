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
			RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0), 23f);
			if (hit.collider != null) {
				if(hit.collider.tag != "Player") {
					if(hit.collider.tag != "Bullet") {
						gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0)*enemySpeed;
						if (hit.distance < 5f && hit.collider.name != this.name) {
							FlipEnemy();
						}
					}
				}
				else {
					if (!fired) {
						Fire(xMoveDirection);
						fired = true;
						Invoke("ResetFired", 1f);
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
		bullet.transform.localPosition = new Vector3(parent.transform.localPosition.x, parent.transform.localPosition.y, parent.transform.localPosition.z);
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