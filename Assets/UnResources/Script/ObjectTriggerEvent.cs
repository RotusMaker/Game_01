using UnityEngine;
using System.Collections;

public class ObjectTriggerEvent : MonoBehaviour {

	private string m_sKey = string.Empty;

	// 게임 리셋될 때 타야함.
	public void Reset()
	{
		this.gameObject.SetActive (true);
	}

	// 스테이지 5,6 떨어지는 발판.
	public void EnterDisappeare()
	{
		if (string.IsNullOrEmpty (m_sKey)) {
			m_sKey = TriggerEventManager.GetInstance.CreateKey ();
		}
		TriggerEventManager.GetInstance.StartTimer (m_sKey, 0.5f, () => {
			// 연출 추가 하면 좋음.
			this.gameObject.SetActive(false);
		});
	}
	public void ExitDisappeare()
	{
	}
}
