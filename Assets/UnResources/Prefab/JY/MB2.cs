using UnityEngine;
using System.Collections;

public class MB2 : MonoBehaviour {
	public float a = 0.7f;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () 
	{
		transform.Translate (0, 0, -a);
	
	}
}
