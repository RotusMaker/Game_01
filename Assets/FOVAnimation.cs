using UnityEngine;
using System.Collections;

public class FOVAnimation : MonoBehaviour {

	IEnumerator Start () 
	{
		Camera cam = this.GetComponent<Camera> ();

		while (true) 
		{
			cam.fieldOfView -= Time.deltaTime * 0.2f;
			yield return null;
		}
	}
}
