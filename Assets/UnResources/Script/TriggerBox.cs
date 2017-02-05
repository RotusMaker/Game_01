using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eTriggerType
{
	Disable = 0,
	Disappeare,		// 오래밟으면 사라지는 발판.
	Swamp,
	Cam_Rotation,
};

//[RequireComponent (typeof (BoxCollider))]
public class TriggerBox : MonoBehaviour 
{
	[System.Serializable]
	public struct TriggingData
	{
		public GameObject target;
		public eTriggerType method;
	}
	public TriggingData[] m_listTriggingData;
	private BoxCollider m_boxCollider;

	void Start()
	{
		m_boxCollider = this.GetComponent<BoxCollider>();
		if (m_boxCollider == null) {	//자기자신에게 없으면 첫번째 자식 서치.
			if (this.transform.childCount > 0) {
				m_boxCollider = this.transform.GetChild(0).GetComponent<BoxCollider>();
			} 
		}

		if (m_boxCollider == null) {
			Debug.LogError ("[Error] 1001");
		} 
		else {
			m_boxCollider.isTrigger = true;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (m_listTriggingData != null && m_listTriggingData.Length > 0)
		{
			for(int i=0; i<m_listTriggingData.Length; i++)
			{
				string method = GetEnterMethodName(m_listTriggingData[i].method);
				if (string.IsNullOrEmpty(method))
				{
					continue;
				}
				m_listTriggingData[i].target.SendMessage(method);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (m_listTriggingData != null && m_listTriggingData.Length > 0)
		{
			for(int i=0; i<m_listTriggingData.Length; i++)
			{
				string method = GetExitMethodName(m_listTriggingData[i].method);
				if (string.IsNullOrEmpty(method))
				{
					continue;
				}
				m_listTriggingData[i].target.SendMessage(method);
			}
		}
	}

	string GetEnterMethodName(eTriggerType type)
	{
		switch(type)
		{
		case eTriggerType.Disappeare:
			return "EnterDisappeare";
		case eTriggerType.Swamp:
			return "EnterSwamp";
		default: 
			return string.Empty;
		}
	}

	string GetExitMethodName(eTriggerType type)
	{
		switch(type)
		{
		case eTriggerType.Disappeare:
			return "ExitDisappeare";
		case eTriggerType.Swamp:
			return "ExitSwamp";
		default: 
			return string.Empty;
		}
	}
}
