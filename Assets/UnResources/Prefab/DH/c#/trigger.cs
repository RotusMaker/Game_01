using UnityEngine;
using System.Collections;

public class trigger : MonoBehaviour 
{
	public GameObject recvObj = null;
	
	//트리거 동작 스위치.
	void OnTriggerEnter(Collider other) 
	{
		Debug.Log ("### Trigger Enter.");

		if (recvObj != null) 
		{
			recvObj.SendMessage ("OnDownBox");
		}
	}
}
