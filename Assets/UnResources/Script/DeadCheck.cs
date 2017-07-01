using UnityEngine;
using System.Collections;

public class DeadCheck : MonoBehaviour {

	public bool isTrigging = false;
    public Vector3 collDirection = Vector3.one;
    public Vector3 otherPosition = Vector3.one;

	// 예전 방식
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("TriggerBox")) {
			return;
		}

		isTrigging = true;

        collDirection = other.transform.position - this.transform.position;
        collDirection.Normalize();

        otherPosition = other.transform.position;

    }

	void OnTriggerExit(Collider other)
	{
		isTrigging = false;
        collDirection = Vector3.one;
        otherPosition = Vector3.one;
    }
}
