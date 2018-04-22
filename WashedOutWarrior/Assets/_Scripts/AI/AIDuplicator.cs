using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDuplicator : MonoBehaviour {
	public GameObject toDuplicate;
	private bool duplicate = true;
	private bool isQuitting = false;

	void OnApplicationQuit()
	{
		isQuitting = true;
	}

	void OnDestroy()
	{
		if (duplicate && !isQuitting && toDuplicate != null)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 parentPosition = this.transform.localPosition;
				GameObject clone = Instantiate(toDuplicate);
				clone.transform.localPosition = new Vector2 (parentPosition.x, parentPosition.y + 5 + i);
				//Currently is specific to pizza because there is no good way to loop through components and check if they're disabled
				clone.GetComponent<BoxCollider2D>().enabled = true;
				clone.GetComponent<AISensors>().enabled = true;
				clone.GetComponent<AIMovement>().enabled = true;
				clone.GetComponent<AIDuplicator>().ChangeDuplicate(false);
				clone.GetComponentInChildren<HealthBar>().enabled = true;
				clone.GetComponentInChildren<HealthBar>().damage = 0;
				clone.SetActive(true);
			}
		}
		print("Pizza was destroyed");
	}

	//To be used later to make a boss where some duplications are false and others are true;
	void ChangeDuplicate(bool newDuplicate) {
		duplicate = newDuplicate;
	}
}
