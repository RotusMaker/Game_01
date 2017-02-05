using UnityEngine;
using System.Collections;

public class DeadCheck : MonoBehaviour {

	public bool isTrigging = false;

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("TriggerBox")) {
			return;
		}
		isTrigging = true;
	}

	void OnTriggerExit(Collider other)
	{
		isTrigging = false;
	}
}
