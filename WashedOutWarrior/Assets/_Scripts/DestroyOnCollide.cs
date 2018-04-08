using UnityEngine;

public class DestroyOnCollide : MonoBehaviour {
	public int durability = 0;

	private void OnCollisionEnter(Collision collision) {
		if (collision.collider.CompareTag("Player")) {
			if (durability-- <= 0) {
				this.gameObject.SetActive (false);
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("Player")) {
			if (durability-- <= 0) {
				this.gameObject.SetActive(false);
			}
		}
	}
}
