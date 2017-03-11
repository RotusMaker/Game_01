using UnityEngine;
using System.Collections;

public class DeadCheck : MonoBehaviour {

	public bool isTrigging = false;
	public int collDirection = -1;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("TriggerBox")) {
			return;
		}
		isTrigging = true;

		if (other.transform.position.x > this.transform.position.x){
			collDirection = 1;
			
		}
		if (other.transform.position.x < this.transform.position.x){
			collDirection = 0;
		}

		if (collDirection != 0 && collDirection != 1) {
			collDirection = -1;
		}
	}

	void OnTriggerExit(Collider other)
	{
		isTrigging = false;
		collDirection = -1;
	}
}
