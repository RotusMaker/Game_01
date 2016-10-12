using UnityEngine;
using System.Collections;

public class rain2 : MonoBehaviour {

	public float c = 0.07f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0,-c,0) ;

	}
}
