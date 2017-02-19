using UnityEngine;
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
		None,
		Run,
		Jump,
		TwoJump,
		Dash,
		Dead,	// y <= -10f, forward ray cast distance <= 1f, death zone
		Goal,
	}
	private ePlayerState m_ePlayerState = ePlayerState.None;

	private Rigidbody m_rigidbody;
	private CapsuleCollider m_Capsule;
	public float m_fForwardVelocity = 5f;	// 속도.
	public float m_fTouchInputGap = 5f;	// 입력 시 점프,좌우 입력 감도.
	public float m_fJumpPower = 1000f;	// 점프 force.
	public float m_fSideVariant = 5f;	// 좌우 이동 값.
	public float m_fDashTime = 0.1f;		// dash 지속 시간.
	public int m_fDashMultiVelocity = 30;	// dash할때 속도 배율.(올릴 수록 dash 속도와 거리 증가)
	public float m_fSideMoveGap = 5f;
	public float m_fFowardRayCheck = 2f;	// 정면 추돌 거리.
	public GroundCheck m_groundCheck;
	public DeadCheck m_deadCheck;

#if UNITY_EDITOR
	// Debug UI.
	public UnityEngine.UI.Text m_txtVelocity;
#endif

	private float m_fMaxRightPos = 0f;
	private float m_fMaxLeftPos = 0f;
	private float m_fDashTimer = 0f;

	private int m_nCurrentPos = 0;

	private Vector3 m_vecOriginPos;

	void Start ()
	{
		InputListener.GetInstance.begin0 += onTouch;
		InputListener.GetInstance.end0 += onTouch;
		InputListener.GetInstance.move0 += onTouch;

		SetOrigPos (transform.localPosition);
		SetState(ePlayerState.None);
	}

	void OnEnable()
	{
		if (m_rigidbody == null)
		{
			m_rigidbody = this.GetComponent<Rigidbody> ();
		}
		if (m_Capsule == null)
		{
			m_Capsule = this.GetComponent<CapsuleCollider>();
		}
	}

	void Update()
	{
		if (IsStopState()) 
		{
			return;
		}
		
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

		float purpose = 0f;
		switch (m_eDirection)
		{
		case eDirection.Right:
			// 오른쪽으로 이동.
			transform.Translate (m_fSideVariant * Time.deltaTime, 0f, 0f);

			purpose = m_vecOriginPos.x + ((m_nCurrentPos + 1) * m_fSideMoveGap);

			if (purpose <= transform.localPosition.x) {
				transform.localPosition = new Vector3 (purpose, transform.localPosition.y, transform.localPosition.z);
				m_eDirection = eDirection.None;
				m_nCurrentPos += 1;
			}
			break;
		case eDirection.Left:
			transform.Translate (-m_fSideVariant * Time.deltaTime, 0f, 0f);

			purpose = m_vecOriginPos.x + ((m_nCurrentPos - 1) * m_fSideMoveGap);

			if (purpose >= transform.localPosition.x) {
				transform.localPosition = new Vector3 (purpose, transform.localPosition.y, transform.localPosition.z);
				m_eDirection = eDirection.None;
				m_nCurrentPos -= 1;
			}
			break;
		default:
			break;
		}
	}

	void FixedUpdate()
	{
		if (IsStopState()) 
		{
			return;
		}
		
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

		// Ground Check.
		if (m_ePlayerState == ePlayerState.Jump || m_ePlayerState == ePlayerState.TwoJump) {
			if (m_groundCheck.isTrigging) {
				SetState (ePlayerState.Run);
			}
		}

		// Dead Check.
		if (m_deadCheck.isTrigging || transform.localPosition.y <= -100f) 
		{
			//Debug.LogError("Dead Check: "+m_deadCheck.isTrigging.ToString()+" "+transform.localPosition.y.ToString());
			SetState (ePlayerState.Dead);
		}

		// 충돌 체크.
		//ForwardCheck();

#if UNITY_EDITOR
		// Debug Msg.
		if (m_txtVelocity != null) {
			m_txtVelocity.text = string.Format ("Velocity:{0}",m_rigidbody.velocity);
		}
#endif
	}

	/*
	// 충돌 체크.
	void OnCollisionEnter(Collision collision)
	{
		// death zone 감지.
		Debug.Log(collision.gameObject.name);
	}

	// 트리거 감지.
	void OnTriggerEnter(Collider other)
	{
		// trigger 체크.
		Debug.Log(other.gameObject.name);
	}
	*/

	void onTouch( string type, int id, float x, float y, float dx, float dy)
	{
		if (IsStopState()) 
		{
			Debug.Log (m_ePlayerState.ToString());
			return;
		}

		switch( type )
		{
		case"begin": 
			//Debug.Log( "down:" + x + "," + y );
			break;
		case"end":
			//Debug.Log ("end:" + x + "," + y + ", d:" + dx + "," + dy);
			if (Mathf.Abs (dx) <= m_fTouchInputGap) 
			{
				OnJump ();
				// 점프할 때 좌우이동 멈춤.
				//m_eDirection = eDirection.None;
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
		case"dash":
			SetState(ePlayerState.Dash);
			break;
		}
	}

	/* public function */
	// 캐릭터 최초 시작 지점(리스폰 지역) 설정.
	public void SetOrigPos(Vector3 position)
	{
		m_vecOriginPos = position;

		m_fMaxRightPos = m_vecOriginPos.x + m_fSideMoveGap;
		m_fMaxLeftPos = m_vecOriginPos.x - m_fSideMoveGap;

		m_nCurrentPos = 0;
	}

	public void ResetGame()
	{
		SetState(ePlayerState.None);
		transform.localPosition = m_vecOriginPos;
		m_eDirection = eDirection.None;
		m_nCurrentPos = 0;
		m_deadCheck.isTrigging = false;
		m_rigidbody.velocity = Vector3.zero;
	}

	public bool IsGoal()
	{
		return m_ePlayerState == ePlayerState.Goal;
	}

	public bool IsDead()
	{
		return m_ePlayerState == ePlayerState.Dead;
	}

	public bool IsStopState()
	{
		return (m_ePlayerState == ePlayerState.Dead || m_ePlayerState == ePlayerState.None || m_ePlayerState == ePlayerState.Goal);
	}

	public void SetState(ePlayerState state)
	{
		if (state == ePlayerState.None) {
			m_ePlayerState = ePlayerState.None;
			m_rigidbody.useGravity = false;
			m_rigidbody.isKinematic = true;
		} 
		else 
		{
			if (m_ePlayerState != state) 
			{
				m_ePlayerState = state;
				Debug.Log ("### State: " + m_ePlayerState.ToString ());
				m_rigidbody.useGravity = true;
				m_rigidbody.isKinematic = false;
			}
		}
	}

	void OnJump()
	{
		if (m_rigidbody != null) 
		{
			if (m_ePlayerState == ePlayerState.Run) 
			{
				SetState (ePlayerState.Jump);
				// ground check를 바로 다시하면 상태가 잘못 정해짐.
				m_groundCheck.isTrigging = false;

				m_rigidbody.velocity = new Vector3 (0f, 0f, m_rigidbody.velocity.z);
				m_rigidbody.AddForce (Vector3.up * m_fJumpPower);
			} 
			else if (m_ePlayerState == ePlayerState.Jump) 
			{
				SetState (ePlayerState.TwoJump);

				m_rigidbody.velocity = new Vector3 (0f, 0f, m_rigidbody.velocity.z);
				m_rigidbody.AddForce (Vector3.up * m_fJumpPower);
			}
		}
	}

	private void ForwardCheck()
	{
		if (m_Capsule == null)
		{
			Debug.LogError ("Not Found Capsule");
			m_Capsule = this.GetComponent<CapsuleCollider> ();
			return;
		}

		Vector3 rayPos = new Vector3(transform.position.x, transform.position.y-0.3f, transform.position.z);

		Ray ray = new Ray (rayPos, Vector3.forward);
		RaycastHit hitInfo;
		if (Physics.Raycast (ray, out hitInfo, m_fFowardRayCheck))		// layermask 정하기.
		{
			if (hitInfo.collider.CompareTag ("TriggerBox")) {
				return;
			}
			// 한 프레임 당 이동거리 = 초기 위치 + velocity * deltaTime 보다 작으면
			//Debug.Log(string.Format("forward object:{0} distance:{1}",hitInfo.collider.name,hitInfo.distance));
			SetState(ePlayerState.Dead);
		}
		//Debug.DrawRay (rayPos, Vector3.forward, Color.red, 5f);
	}
}
