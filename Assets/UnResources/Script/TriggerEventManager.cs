using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TriggerEventManager : MonoSingleton<TriggerEventManager> {

	// 시간 데이터 클래스.
	public class TriggerTimer
	{
		public float startTime = 0f;
		public float time = 0f;
		public Action complet;
		public bool IsCheckTimer(float currentTime)
		{
			float delay = currentTime - startTime;
			if (time <= delay) {
				return true;
			}
			return false;
		}
	}
	public Dictionary<string, TriggerTimer> m_dicTimers = new Dictionary<string, TriggerTimer> ();
	private int m_nKeyIndex = 0;
	private List<string> m_listDeleteKey = new List<string> ();

	public string CreateKey()
	{
		m_nKeyIndex++;
		return m_nKeyIndex.ToString();
	}

	public void StartTimer(string key, float time, Action complet)
	{
		if (m_dicTimers.ContainsKey (key)) {
			m_dicTimers [key].startTime = Time.time;
			m_dicTimers [key].time = time;
			m_dicTimers [key].complet = complet;
		} 
		else {
			TriggerTimer timer = new TriggerTimer ();
			timer.startTime = Time.time;
			timer.time = time;
			timer.complet = complet;
			m_dicTimers.Add (key, timer);
		}
	}

	// Frame 시간 영향 받지 않는 Update.
	void FixedUpdate()
	{
		if (m_dicTimers.Count > 0) {
			float currentTime = Time.time;
			m_listDeleteKey.Clear ();
			for (Dictionary<string, TriggerTimer>.Enumerator it = m_dicTimers.GetEnumerator (); it.MoveNext ();) {
				if (it.Current.Value.IsCheckTimer (currentTime)) {
					// 시간다됨.
					it.Current.Value.complet();
					m_listDeleteKey.Add (it.Current.Key);
				}
			}
			for (int i = 0; i < m_listDeleteKey.Count; i++) {
				m_dicTimers.Remove (m_listDeleteKey[i]);
			}
		}
	}
}
