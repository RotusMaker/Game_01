using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSkillState
{
	None,
	TwoJump,
	Dash,
}

// 게임내 아이템 슬롯과 재화등 관리.
public class ItemManager : MonoSingleton<ItemManager> {

	private eSkillState m_eSkillSlot = eSkillState.None;	// 현재 장착된 스킬.

	public void Reset()
	{
		m_eSkillSlot = eSkillState.None;
	}

	public void SetSlot(eSkillState skill)
	{
		m_eSkillSlot = skill;
	}
	public eSkillState GetSlotSkill()
	{
		return m_eSkillSlot;
	}
	public bool UseSkill(eSkillState useSkill)
	{
		if (m_eSkillSlot == useSkill) {
			m_eSkillSlot = eSkillState.None;
			return true;
		} 
		else {
			return false;
		}
	}
}
