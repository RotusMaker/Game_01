using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class Moving : MonoBehaviour {
	// Use this for initialization
	void Start () {
		InputListener.GetInstance.begin0 += onTouch;
		InputListener.GetInstance.end0 += onTouch;
		InputListener.GetInstance.move0 += onTouch;
	}

	void onTouch( string type, int id, float x, float y, float dx, float dy)
	{
		switch( type ){
		case"begin": Debug.Log( "down:" + x + "," + y ); break;
		case"end": Debug.Log( "end:" + x + "," + y +", d:" + dx +","+dy ); break;
		case"move": Debug.Log( "move:" + x + "," + y +", d:" + dx +","+dy ); break;
		}
	}
}
