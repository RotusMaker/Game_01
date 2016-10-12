using UnityEngine;
using System.Collections;

public class earthdown : MonoBehaviour {

	public float a = 0.05f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0,-a,0) ;

	}
}
