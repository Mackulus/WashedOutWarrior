using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatroller:Listener {

	public int enemySpeed;
	public int xMoveDirection;
	public HealthBar healthBar;
	public GameObject projectile;
	public float bulletImpulse = 20.0f;
	public bool fired = false;

	void Start () {
		if (healthBar != null) {
			healthBar.deathListeners.Add(this);
		}
	}

	// Update is called once per frame
	void Update () {
		if (xMoveDirection != 0)
		{
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
						FireOne();
						fired = true;
						Invoke("ResetFired", 1f);
					}
				}
			}
			else
			{
				gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0)*enemySpeed;
			}
		}
		else
		{
			if (!fired)
			{
				FireBoth();
				fired = true;
				Invoke("ResetFired", 1f);
			}
		}
	}

	void FlipEnemy()
	{
		print("Direction : " + xMoveDirection);
		if (xMoveDirection > 0)
		{
			print("flipped");
			xMoveDirection = -1;
		}
		else
		{
			xMoveDirection = 1;
		}
	}

	void FireOne() {
		Transform parent = (xMoveDirection > 0) ? transform.Find("BulletSpawnRight") : transform.Find("BulletSpawnLeft");
		Fire(parent);
	}

	void FireBoth()
	{
		Fire(transform.Find("BulletSpawnRight"));
		Fire(transform.Find("BulletSpawnLeft"));
	}

	void Fire(Transform parent)
	{
		GameObject bullet = Instantiate(projectile, parent);
		bullet.transform.localPosition = new Vector3(parent.transform.localPosition.x, parent.transform.localPosition.y, parent.transform.localPosition.z);
		if (parent.name == "BulletSpawnRight") {
			print("In here");
			Vector2 localScale = bullet.transform.localScale;
			localScale.y *= -1;
			bullet.transform.localScale = localScale;
			bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletImpulse, ForceMode2D.Impulse);
		}
		else {
			bullet.GetComponent<Rigidbody2D>().AddForce(transform.right * -bulletImpulse, ForceMode2D.Impulse);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (healthBar != null && collision.gameObject.CompareTag("Weapon")) {
			healthBar.OnDamage(2);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Bullet"))
		{
			healthBar.OnDamage();
			Destroy(collision.collider.gameObject);
		}
	}

	void ResetFired()
	{
		fired = false;
	}

	override public void OnHear(GameObject g) {
		gameObject.SetActive(false);
	}
}