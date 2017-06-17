using UnityEngine;
using System.Collections;

public class ChracterTriggerEvent : MonoBehaviour 
{
	private CharacterBody m_movePlayer;
	private SwampData m_swampData = new SwampData();

	private class SwampData
	{
		public bool isEnter = false;
		public float forwardVelocity;
		public float jumpPower;
	}

	void Start()
	{
		m_movePlayer = this.GetComponent<CharacterBody> ();
	}

	void OnCollisionEnter(Collision collision)
	{
		// 실제 충돌 부분(패턴에 TouchAble 완료되면 풀기)
		/*
		if (collision.gameObject.CompareTag ("TouchAble") == false) {
			Debug.Log("# Collision Enter Death. " + collision.gameObject.name);
			m_movePlayer.SetState (CharacterBody.ePlayerState.Dead, collision);
		}
		*/
	}

	void OnTriggerEnter(Collider other)
	{
		// trigger 체크.
		Debug.Log(other.gameObject.name);
		if (other.gameObject.CompareTag ("GoalZone")) {
			m_movePlayer.SetState (CharacterBody.ePlayerState.Goal);
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
