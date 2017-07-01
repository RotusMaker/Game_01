using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬 오브젝트
public class TriggerSkillItem : C8.Collectible
{
	public eSkillState skill = eSkillState.None;

	public override void OnTriggerEnter(Collider other)
	{
        base.OnTriggerEnter(other);

		if (other.CompareTag ("Player")) {
			gameObject.SetActive (false);
            SoundManager.GetInstance.PlaySound("item_get", eSoundPlayType.OneShot);
			ItemManager.GetInstance.SetSlot (skill);
		}
	}
}
