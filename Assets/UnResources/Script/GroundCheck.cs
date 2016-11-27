using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	public bool isTrigging = false;

	void OnTriggerEnter(Collider other)
	{
		isTrigging = true;
	}

	void OnTriggerExit(Collider other)
	{
		isTrigging = false;
	}
}
