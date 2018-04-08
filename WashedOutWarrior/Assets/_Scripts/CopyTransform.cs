using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyTransform : MonoBehaviour {
    public GameObject target;
	public Vector3 posOffset, rotOffset;

	void Update () {
        transform.position = target.transform.position + posOffset;
		transform.rotation = target.transform.rotation * Quaternion.Euler(rotOffset);
		transform.localScale = target.transform.localScale;
	}
}
