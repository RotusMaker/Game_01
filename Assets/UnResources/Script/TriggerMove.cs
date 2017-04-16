using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMove : TriggerRoot {

	//public GameObject target = null;	// 타겟이 없으면 자기자신.
	public float delay = 0f;
	public float time = 0f;
	public Vector3 toPosition = Vector3.zero;
	public iTween.EaseType easyType = iTween.EaseType.linear;

	private Vector3 orgPosition = Vector3.zero;

	public void OnSwitchEnter()
	{
		orgPosition = this.transform.localPosition;
		iTween.MoveTo (this.gameObject, iTween.Hash ("position", toPosition, "islocal", true, "time", time, "delay", delay, "eatytype", iTween.EaseType.linear));
	}

	public void OnSwitchExit()
	{
	}
}
