using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour {

	private string getToBoss = "fat";
	private string destroyBoss = "smash";
	//private string[] cheatCodes = {getToBoss, destroyBoss};
	private int index = 0;
	private string cheatCode = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
		{
			if((index < getToBoss.Length && Input.GetKeyDown(getToBoss[index].ToString())) || (index < destroyBoss.Length && Input.GetKeyDown(destroyBoss[index].ToString())))
			{
				index++;
				cheatCode += Input.inputString;
			}
			else
			{
				index = 0;
				cheatCode = "";
			}

			if(cheatCode == getToBoss)
			{
				TransportToBoss();
			}
			else if (cheatCode == destroyBoss)
			{
				DestroyBoss();
			}
		}
	}

	void TransportToBoss()
	{
		Transform gordo = GameObject.Find("Gordo").transform;
		Transform boss = GameObject.FindWithTag("Boss").transform;

		gordo.position = new Vector2(boss.position.x - 50, 0);
	}

	void DestroyBoss()
	{
		int maxHealth = GameObject.FindWithTag("Boss").GetComponentInChildren<HealthBar>().maxHealth;
		GameObject.FindWithTag("Boss").GetComponentInChildren<HealthBar>().OnDamage(maxHealth);
	}
}
