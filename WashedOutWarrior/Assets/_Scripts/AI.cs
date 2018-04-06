using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	public bool facingLeft = true;
	public int speed = 0, jumpStrength = 0;
	public List<Vector2> nodes = new List<Vector2>();

	// Use this for initialization
	void Start () {
		
	}

	private void FixedUpdate() {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	void Flip() {
		facingLeft = !facingLeft;
		Vector2 localScale = gameObject.transform.localScale;
		localScale.x *= -1;
		transform.localScale = localScale;
	}
}
