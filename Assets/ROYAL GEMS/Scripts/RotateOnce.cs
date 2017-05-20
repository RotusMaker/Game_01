using UnityEngine;
using System.Collections;

public class RotateOnce : MonoBehaviour {

	public float rotationleft=360;
	public float rotationspeed=500;
	public float finished = 1;
		
	void Update()
	{

		float rotation=rotationspeed*Time.deltaTime;

		if (rotationleft > rotation && finished == 0)
		{
			rotationleft-=rotation;

		}
		else
		{
			rotation=rotationleft;
			rotationleft=0;

			finished = 1;
			rotationleft=360;
			//transform.Rotate(0,0,0);
		}
		transform.Rotate(0,rotation,0);
	}
	public void Adjustfinished(float newfinished) {
		finished = newfinished;
	}
}
