using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
    public GameObject target;
	public Vector3 offset;

	void Update () {
        this.transform.position = target.transform.position + offset;

		if (Input.GetAxis("Vertical") < 0.0F) {
			transform.GetChild(0).localPosition = Vector3.zero;
		}
		else transform.GetChild(0).localPosition = new Vector2(0, 10);
	}
}
