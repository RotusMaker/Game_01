using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[RequireComponent (typeof (BoxCollider))]
public class Switch : MonoBehaviour 
{
	[System.Serializable]
	public struct TriggingData
	{
		public GameObject target;
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
				m_listTriggingData[i].target.SendMessage("OnSwitchEnter", other);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (m_listTriggingData != null && m_listTriggingData.Length > 0)
		{
			for(int i=0; i<m_listTriggingData.Length; i++)
			{
				m_listTriggingData[i].target.SendMessage("OnSwitchExit", other);
			}
		}
	}
}
