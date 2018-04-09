using UnityEngine;

//code from https://unity3d.com/learn/tutorials/topics/2d-game-creation/2d-scrolling-backgrounds
public class BGScroller : MonoBehaviour {
	public float scrollSpeed = 0f;
	public float tileSizeZ = 0f;

	private Vector3 startPosition;

	void Start () {
		startPosition = transform.position;
	}

	void Update () {
		float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
		transform.position = startPosition + Vector3.down * newPosition;
	}
}