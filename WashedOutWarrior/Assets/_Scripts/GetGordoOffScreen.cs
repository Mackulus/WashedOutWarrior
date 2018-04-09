using UnityEngine;

public class GetGordoOffScreen : MonoBehaviour {
	private Animator anim;
	//private bool play = false;
	SceneFader fadeScr;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		anim.SetTrigger("Run_01");
		fadeScr = GameObject.FindObjectOfType<SceneFader>();
		fadeScr.EndScene("Level1");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2(transform.position.x + 0.05f, transform.position.y);
	}
}