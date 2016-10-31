using UnityEngine;
using System.Collections;

public class MC : MonoBehaviour {
	public float z = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (0, 0, -z);
	
	}
}
