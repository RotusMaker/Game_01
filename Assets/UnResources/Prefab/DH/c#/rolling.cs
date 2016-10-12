using UnityEngine;
using System.Collections;

public class rolling : MonoBehaviour {

	public float d = 0.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0, 0, d);
	
	}
}
