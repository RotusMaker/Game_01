using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
