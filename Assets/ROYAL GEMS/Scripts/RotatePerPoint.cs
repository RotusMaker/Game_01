using UnityEngine;
using System.Collections;

public class RotatePerPoint : MonoBehaviour {

	public float point_x = 0f;
	public float point_y = 0f;
	public float point_z = 0f;
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(point_x, point_y, point_z);
		//transform.Rotate(0, point_y, 0);
	
	}
	public void AdjustPoint_x(float newPoint_x) {
		point_x = newPoint_x;
	}
	public void AdjustPoint_y(float newPoint_y) {
		point_y = newPoint_y;
	}
	public void AdjustPoint_z(float newPoint_z) {
		point_z = newPoint_z;
	}

}
