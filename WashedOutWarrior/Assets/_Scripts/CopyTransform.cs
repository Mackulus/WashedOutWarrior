using UnityEngine;

public class CopyTransform : MonoBehaviour {
    public GameObject target;
	public Vector3 posOffset = Vector3.zero, rotOffset = Vector3.zero;

	void Update () {
        transform.position = target.transform.position + posOffset;
		transform.rotation = target.transform.rotation * Quaternion.Euler(rotOffset);
		transform.localScale = target.transform.localScale;
	}
}
