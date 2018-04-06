using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour {
    public GameObject target;
	public Vector3 offset;

	void Update () {
        this.transform.position = target.transform.position + offset;
	}
}
