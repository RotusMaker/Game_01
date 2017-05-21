using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스위치를 건들면 움직이는 오브젝트들
public class SW_Move : TriggerRoot {

	//public GameObject target = null;	// 타겟이 없으면 자기자신.
	public float delay = 0f;
	public float time = 0f;
	public Vector3 toPosition = Vector3.zero;
	public iTween.EaseType easyType = iTween.EaseType.linear;

	public void OnSwitchEnter()
	{
        iTween.MoveTo (this.gameObject, iTween.Hash ("position", toPosition, "islocal", true, "time", time, "delay", delay, "eatytype", iTween.EaseType.linear));
	}

	public void OnSwitchExit()
	{
	}
}
