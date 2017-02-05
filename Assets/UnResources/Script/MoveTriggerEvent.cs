using UnityEngine;
using System.Collections;

public class MoveTriggerEvent : ObjectTriggerEvent 
{
	public enum eTriggerEventMove
	{
		Up,
		Down,
		Right,
		Left,
	}
	public eTriggerEventMove m_eEventMove = eTriggerEventMove.Up;
	public float m_fValue = 5f;
	public float m_fSpeed = 10f;
	public float m_fDelay = 0.5f;
	/*
	public override void EnterGo()
	{
		//base.EnterGo ();

		switch (m_eEventMove) {
		case eTriggerEventMove.Up:
		case eTriggerEventMove.Down:
		case eTriggerEventMove.Right:
		case eTriggerEventMove.Left:
			StartCoroutine (Move ());
			break;
		}
	}

	public override void ExitGo()
	{
		//base.ExitGo ();
	}
	*/
	IEnumerator Move()
	{
		yield return new WaitForSeconds (m_fDelay);

		float moveValue = 0f;
		float timeMoveValue = 0f;
		while (moveValue < m_fValue) {
			timeMoveValue = m_fSpeed * Time.deltaTime;
			moveValue += timeMoveValue;

			switch (m_eEventMove) {
			case eTriggerEventMove.Up:
				transform.Translate (0f, timeMoveValue, 0f);
				break;
			case eTriggerEventMove.Down:
				transform.Translate (0f, -timeMoveValue, 0f);
				break;
			case eTriggerEventMove.Right:
				transform.Translate (timeMoveValue, 0f, 0f);
				break;
			case eTriggerEventMove.Left:
				transform.Translate (-timeMoveValue, 0f, 0f);
				break;
			}
			yield return null;
		}
	}
}
