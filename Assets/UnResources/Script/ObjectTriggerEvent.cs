using UnityEngine;
using System.Collections;

public class ObjectTriggerEvent : MonoBehaviour {

	private string m_sKey = string.Empty;
	private eTriggerType type = eTriggerType.Disable;

	private Vector3 origPos;
	private GameObject model;

	void Awake()
	{
		if (this.transform.childCount > 0) {
			model = this.transform.GetChild (0).gameObject;
		}
	}

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
			if (model != null) {
				iTween.Stop (model);
			}
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
		if (model != null) {
			iTween.MoveTo (model, iTween.Hash ("y", 1f, "time", 1f, "delay", 0.1f, "islocal", true, "looptype", iTween.LoopType.pingPong));
		}
		iTween.MoveTo(this.gameObject, iTween.Hash("position", purposePos, "time", 2f, "islocal", true));
	}
}
