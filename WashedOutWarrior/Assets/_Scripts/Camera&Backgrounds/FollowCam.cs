using UnityEngine;

public class FollowCam : MonoBehaviour {
    public GameObject target;
	public Vector3 offset;
	public int xMin, xMax, yMin, yMax;

	private void LateUpdate() {
		float x = Mathf.Clamp(target.transform.position.x, xMin, xMax);
		float y = Mathf.Clamp(target.transform.position.y, yMin, yMax);
		this.transform.position = new Vector3(x, y, target.transform.position.z + offset.z);
	}

	//void Update () {
    //    if (Input.GetAxis("Vertical") < 0.0F) {
	//		transform.GetChild(0).localPosition = Vector3.zero;
	//	}
	//	else transform.GetChild(0).localPosition = new Vector2(0, 10);
	//}
}
