using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public float position_X = 1;
	public float position_Y = 1;
	public float position_Z = 1;
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(0, speed * Time.deltaTime, 0);
		//transform.localPosition(0 ,0 ,0);
		transform.localPosition = new Vector3(position_X, position_Y, position_Z);
	
	}
	public void AdjustPosition_X(float newPosition_X) {
		position_X = newPosition_X;
	}
	public void AdjustPosition_Y(float newPosition_Y) {
		position_Y = newPosition_Y;
	}
	public void AdjustPosition_Z(float newPosition_Z) {
		position_Z = newPosition_Z;
	}

}
