using UnityEngine;

public class FocusController : MonoBehaviour {
	public GameObject focus;

	void LateUpdate() {
		transform.LookAt(2 * transform.position - focus.transform.position);
	}
}
