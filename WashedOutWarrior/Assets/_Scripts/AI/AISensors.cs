using System.Collections.Generic;
using UnityEngine;

public class AISensors : Listener {
	public List<Vector2> nodes = new List<Vector2>();
	public HealthBar healthBar;
	public Vector2 playerRelPos = Vector2.zero, playerRelPosRaw = Vector2.zero;
	public float viewRange = 50f;

	private void Awake() {
		healthBar = gameObject.GetComponentInChildren<HealthBar>();
		if (healthBar != null) {
			healthBar.deathListeners.Add(this);
		}
	}

	// Use this for initialization
	void Start () {
		InvokeRepeating("FindPlayer", .25f, .25f);
	}

    private void FindPlayer() {
        //See if player is within viewRange units
        Collider2D[] collisions = new Collider2D[1];
		if (Physics2D.OverlapCircleNonAlloc(transform.position, viewRange, collisions, 9) >= 1) {
			//print("Player in Range");
			playerRelPosRaw = collisions[0].gameObject.transform.position - transform.position;
			playerRelPos = playerRelPosRaw;
			if (Mathf.Abs(playerRelPos.y) < 10f) {
				playerRelPos.y = 0;
			}
			else if (playerRelPos.y >= 10f) {
				playerRelPos.y = 1;
			}
			else {
				playerRelPos = Vector2.zero;
			}
			playerRelPos.Normalize();
		}
    }

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Player") && healthBar != null) {
			healthBar.OnDamage();
		}
		else if (collision.collider.CompareTag("BulletRicochet") && healthBar != null) {
			healthBar.OnDamage();
			Destroy(collision.collider.gameObject);
		}
	}
	private void OnCollisionExit2D(Collision2D collision) {

	}

	private void OnTriggerEnter2D(Collider2D collision) {
		// Checking for weapon tag is important, I don't want enemies to be hit when weapon is not swinging
		if (healthBar != null && collision.CompareTag("Weapon")){
			if (!gameObject.name.Contains("Pizza")){
				healthBar.OnDamage();
			}
			else if (collision.gameObject.name == "Knife"){
				healthBar.OnDamage(4);
			}
		}
	}

	override public void OnHear(GameObject g) {
		gameObject.SetActive(false);
	}
}