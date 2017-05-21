using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRoot : MonoBehaviour {

	private Vector3 orgPosition;

	void Awake()
	{
        //orgPosition = this.transform.localPosition;
        InitPosition();
    }

    public void InitPosition()
    {
        orgPosition = this.transform.localPosition;
    }

    /*
    public void Reset()
	{
		this.transform.localPosition = orgPosition;
		this.gameObject.SetActive (true);
	}
    */
    public virtual void Reset()
    {
        this.transform.localPosition = orgPosition;
        this.gameObject.SetActive(true);
    }
}
