﻿using UnityEngine;
using System.Collections;

public class ChracterTriggerEvent : MonoBehaviour 
{
	private Moving m_movePlayer;
	private SwampData m_swampData = new SwampData();

	private class SwampData
	{
		public bool isEnter = false;
		public float forwardVelocity;
		public float jumpPower;
	}

	void Start()
	{
		m_movePlayer = this.GetComponent<Moving> ();
	}

	void OnCollisionEnter(Collision collision)
	{
		Debug.Log(collision.gameObject.name);
		// death zone 감지.
		if (collision.gameObject.CompareTag ("DeathZone")) {
			m_movePlayer.SetState (Moving.ePlayerState.Dead);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// trigger 체크.
		Debug.Log(other.gameObject.name);
		if (other.gameObject.CompareTag ("GoalZone")) {
			m_movePlayer.SetState (Moving.ePlayerState.Goal);
		}
	}

	void EnterSwamp()
	{
		if (m_swampData.isEnter) {
			return;
		}
		m_swampData.isEnter = true;
		m_swampData.forwardVelocity = m_movePlayer.m_fForwardVelocity;
		m_swampData.jumpPower = m_movePlayer.m_fJumpPower;

		m_movePlayer.m_fForwardVelocity = m_movePlayer.m_fForwardVelocity * 0.3f;
		m_movePlayer.m_fJumpPower = m_movePlayer.m_fJumpPower * 0.5f;
	}

	void ExitSwamp()
	{
		m_swampData.isEnter = false;
		m_movePlayer.m_fForwardVelocity = m_swampData.forwardVelocity;
		m_movePlayer.m_fJumpPower = m_swampData.jumpPower;
	}
}
