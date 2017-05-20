using UnityEngine;
using System.Collections;

public class PingPonf_Rotate : MonoBehaviour {

	public float Rotate_X = 1f;
	public float Speed_X = 0f;
	public float Rotate_Y = 1f;
	public float Speed_Y = 0f;
	public float Rotate_Z = 1f;
	public float Speed_Z = 0f;


		void Update() {
		transform.localEulerAngles = new Vector3(Mathf.PingPong( Speed_X * Time.time, Rotate_X), Mathf.PingPong( Speed_Y * Time.time, Rotate_Y), Mathf.PingPong( Speed_Z * Time.time, Rotate_Z));
		//transform.eulerAngles = new Vector3(transform.position.x, Mathf.PingPong( Speed_Y * Time.time, Rotate_Y), transform.position.z);
		}
	}