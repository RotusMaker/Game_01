using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (BoxCollider))]
public class TriggerBox : MonoBehaviour 
{
	public enum eTriggerType
	{
		Disable = 0,
		Swamp,
		Go,
		Cam_Rotation,
	}
	
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
		m_boxCollider.isTrigger = true;
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
		case eTriggerType.Go:	
			return "Go";
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
		case eTriggerType.Go:	
			return "GoExit";
		case eTriggerType.Swamp:
			return "ExitSwamp";
		default: 
			return string.Empty;
		}
	}
}
