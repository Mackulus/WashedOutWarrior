using UnityEngine;

public class FollowCam : MonoBehaviour {
    public GameObject target;
	public Vector3 offset;

	private void FixedUpdate() {
		this.transform.position = target.transform.position + offset;
	}

	void Update () {
        if (Input.GetAxis("Vertical") < 0.0F) {
			transform.GetChild(0).localPosition = Vector3.zero;
		}
		else transform.GetChild(0).localPosition = new Vector2(0, 10);
	}
}
