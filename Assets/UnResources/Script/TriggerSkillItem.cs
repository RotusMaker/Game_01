using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬 오브젝트
public class TriggerSkillItem : TriggerRoot {

	public eSkillState skill = eSkillState.None;

	public void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player")) {
			gameObject.SetActive (false);
			ItemManager.GetInstance.SetSlot (skill);
		}
	}
}
