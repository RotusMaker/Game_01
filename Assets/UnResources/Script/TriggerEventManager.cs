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

	public string CreateKey()
	{
		m_nKeyIndex++;
		return m_nKeyIndex.ToString();
	}

	public void StartTimer(string key, float time, Action complet)
	{
		TriggerTimer timer = new TriggerTimer ();
		timer.startTime = Time.time;
		timer.time = time;
		timer.complet = complet;

		if (m_dicTimers.ContainsKey (key)) {
			Debug.LogError ("[Error] 10002");
			return;
		}
		m_dicTimers.Add (key, timer);
	}

	// Frame 시간 영향 받지 않는 Update.
	void FixedUpdate()
	{
		if (m_dicTimers.Count > 0) {
			float currentTime = Time.time;
			for (Dictionary<string, TriggerTimer>.Enumerator it = m_dicTimers.GetEnumerator (); it.MoveNext ();) {
				if (it.Current.Value.IsCheckTimer (currentTime)) {
					// 시간다됨.
					it.Current.Value.complet();
					m_dicTimers.Remove (it.Current.Key);
				}
			}
		}
	}
}
