using UnityEngine;
using System.Collections;

public class earthdown : MonoBehaviour {

	public float a = 0.05f;
	private bool isDown = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isDown) {
			transform.Translate (0, -a, 0);
		}
	}

	public void OnDownBox()
	{
		Debug.Log ("### OnDownBox: "+this.name);

		isDown = true;
	}
}
