using UnityEngine;
using System.Collections;

public class rain1 : MonoBehaviour {

	public float b = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0,-b,0) ;

	}
}
