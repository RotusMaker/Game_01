using UnityEngine;
using System.Collections;

public class ObjectTriggerEvent : MonoBehaviour {

	private string m_sKey = string.Empty;
	private eTriggerType type = eTriggerType.Disable;

	private Vector3 origPos;

	void Start()
	{
		origPos = this.transform.localPosition;
	}

	// 게임 리셋될 때 타야함.
	public void Reset()
	{
		switch(type){
		case eTriggerType.Disappeare:
			this.gameObject.SetActive (true);
			break;
		case eTriggerType.Bird:
			iTween.Stop (this.gameObject);
			this.transform.localPosition = origPos;
			break;
		}
	}

	// 스테이지 5,6 떨어지는 발판.
	public void EnterDisappeare()
	{
		type = eTriggerType.Disappeare;

		if (string.IsNullOrEmpty (m_sKey)) {
			m_sKey = TriggerEventManager.GetInstance.CreateKey ();
		}
		TriggerEventManager.GetInstance.StartTimer (m_sKey, 0.5f, () => {
			// 연출 추가 하면 좋음.
			this.gameObject.SetActive(false);
		});
	}

	// 스테이지 8,9 다가오는 새.
	public void EnterBird()
	{
		type = eTriggerType.Bird;

		Vector3 purposePos = origPos + (Vector3.back * 200f);
		//iTween.MoveTo(this.gameObject, iTween.Hash("y", origPos.y + 12f, "time", 1f, "delay", 0.1f, "islocal", true, "looptype", iTween.LoopType.pingPong));
		iTween.MoveTo(this.gameObject, iTween.Hash("position", purposePos, "time", 10f, "delay", 0.1f, "islocal", true));
	}
}
