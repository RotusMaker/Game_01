using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	public bool isTrigging = false;
	public string touchTag = string.Empty;

	void OnTriggerEnter(Collider other)
	{
		isTrigging = true;
		touchTag = other.tag;
	}

	void OnTriggerExit(Collider other)
	{
		isTrigging = false;
		touchTag = string.Empty;
	}

	public void ResetGroundCheck()
	{
		isTrigging = false;
		touchTag = string.Empty;
	}
}
