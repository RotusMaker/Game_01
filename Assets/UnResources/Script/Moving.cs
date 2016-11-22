﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent(typeof (CapsuleCollider))]
public class Moving : MonoBehaviour 
{
	public enum eDirection
	{
		None = 0,
		Right= 1,
		Left = 2
	}
	private eDirection m_eDirection = eDirection.None;

	public enum ePlayerState
	{
		Run = 0,
		Jump,
		TwoJump,
		Dash,
		Dead,
	}
	private ePlayerState m_ePlayerState = ePlayerState.Run;

	private Rigidbody m_rigidbody;
	private CapsuleCollider m_Capsule;
	public float m_fForwardVelocity = 5f;	// 속도.
	public float m_fTouchInputGap = 5f;	// 입력 시 점프,좌우 입력 감도.
	public float m_fJumpPower = 1000f;	// 점프 force.
	public float m_fSideVariant = 5f;	// 좌우 이동 값.
	public float groundCheckDistance = 1f;	// 땅에 서 있는지 체크.
	public float m_fDashTime = 0.1f;		// dash 지속 시간.
	public int m_fDashMultiVelocity = 30;	// dash할때 속도 배율.(올릴 수록 dash 속도와 거리 증가)

	private bool m_IsJumped = false;
	private float m_fDashTimer = 0f;

	void Start ()
	{
		InputListener.GetInstance.begin0 += onTouch;
		InputListener.GetInstance.end0 += onTouch;
		InputListener.GetInstance.move0 += onTouch;

		m_rigidbody = this.GetComponent<Rigidbody> ();
		//m_rigidbody.useGravity = false;
		m_Capsule = this.GetComponent<CapsuleCollider>();
	}

	void Update()
	{
		if (m_ePlayerState == ePlayerState.Dash) 
		{
			m_fDashTimer += Time.deltaTime;
			if (m_fDashTimer >= m_fDashTime)
			{
				m_fDashTimer = 0f;
				SetState (ePlayerState.Run);
			}
			return;
		}
		
		switch (m_eDirection)
		{
		case eDirection.Right:
			transform.Translate (m_fSideVariant * Time.deltaTime, 0f, 0f);
			break;
		case eDirection.Left:
			transform.Translate (-m_fSideVariant * Time.deltaTime, 0f, 0f);
			break;
		default:
			break;
		}

		if (Input.GetKeyDown (KeyCode.Space))
		{
			SetState(ePlayerState.Dash);
		}
	}

	void FixedUpdate()
	{
		if (m_rigidbody != null)
		{
			if (m_ePlayerState == ePlayerState.Dash) 
			{
				m_rigidbody.velocity = new Vector3(0f, m_rigidbody.velocity.y, m_fForwardVelocity * m_fDashMultiVelocity);
				return;
			} 
			else 
			{
				m_rigidbody.velocity = new Vector3(0f, m_rigidbody.velocity.y, m_fForwardVelocity);
			}
		}

		GroundCheck();
	}

	void onTouch( string type, int id, float x, float y, float dx, float dy)
	{
		switch( type )
		{
		case"begin": 
			//Debug.Log( "down:" + x + "," + y );
			break;
		case"end":
			//Debug.Log ("end:" + x + "," + y + ", d:" + dx + "," + dy);
			if (Mathf.Abs (dx) <= m_fTouchInputGap) 
			{
				// jump
				if (m_ePlayerState == ePlayerState.Run) {
					SetState (ePlayerState.Jump);
					OnJump ();
				} 
				else if (m_ePlayerState == ePlayerState.Jump) {
					SetState (ePlayerState.TwoJump);
					OnJump ();
				}
				m_eDirection = eDirection.None;
			}
			else 
			{
				if (dx < 0) 
				{
					// left
					m_eDirection = eDirection.Left;
				} 
				else 
				{
					// right
					m_eDirection = eDirection.Right;
				}
			}
			break;
		case"move": 
			//Debug.Log( "move:" + x + "," + y +", d:" + dx +","+dy ); 
			break;
		}
	}

	void SetState(ePlayerState state)
	{
		if (m_ePlayerState != state) 
		{
			m_ePlayerState = state;
			Debug.Log ("### State: " + m_ePlayerState.ToString ());
		}
	}

	void OnJump()
	{
		if (m_rigidbody != null) 
		{
			m_IsJumped = true;
			m_rigidbody.velocity = new Vector3 (0f, 0f, m_rigidbody.velocity.z);
			m_rigidbody.AddForce (Vector3.up * m_fJumpPower);
		}
	}

	private void GroundCheck()
	{
		if (m_Capsule == null) 
		{
			Debug.LogError ("Not Found Capsule");
			m_Capsule = this.GetComponent<CapsuleCollider> ();
			return;
		}

		Ray ray = new Ray (transform.position, Vector3.down);
		RaycastHit hitInfo;
		if (Physics.Raycast (ray, out hitInfo, 1f)) 
		{
			if (hitInfo.distance <= groundCheckDistance) 
			{
				if (m_IsJumped) 
				{
					if (hitInfo.distance >= groundCheckDistance) 
					{
						m_IsJumped = false;
					}
					Debug.Log ("jump distance: " + hitInfo.distance.ToString ());
				}
				else
				{
					Debug.Log ("ground distance: " + hitInfo.distance.ToString ());
					SetState (ePlayerState.Run);
				}
			} 
			else 
			{
				//Debug.Log ("no check distance: " + hitInfo.distance.ToString ());
			}
		}
	}
}
