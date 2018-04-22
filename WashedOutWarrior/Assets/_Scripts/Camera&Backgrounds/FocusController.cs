using UnityEngine;

public class FocusController : MonoBehaviour {
	public GameObject focus;

	private void Start() {
		if (focus == null) focus = GameObject.Find("Camera Mount");
	}

	void LateUpdate() {
		transform.LookAt(2 * transform.position - focus.transform.position);
	}
}