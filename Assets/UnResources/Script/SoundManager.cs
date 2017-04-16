using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSoundPlayType
{
	OneShot,
	Loop,
}

public class SoundManager : MonoSingleton<SoundManager> {

	public GameObject m_Root;
	public string m_updateDate;
	public List<AudioSource> m_listAudio;
	private Dictionary<string, AudioSource> m_dicAudio;

	void Awake()
	{
		if (m_dicAudio == null) {
			m_dicAudio = new Dictionary<string, AudioSource> ();
		}
		for (int i = 0; i < m_listAudio.Count; i++) {
			m_dicAudio.Add (m_listAudio [i].name, m_listAudio [i]);
		}
	}

	public void PlaySound(string soundName, eSoundPlayType type)
	{
		AudioSource audio = null;

		if (m_dicAudio != null && m_dicAudio.ContainsKey (soundName)) {
			audio = m_dicAudio [soundName];
		}
		if (audio != null) {
			switch (type) {
			case eSoundPlayType.OneShot:
				if (!audio.isPlaying) {
					audio.Play ();
				}
				break;
			case eSoundPlayType.Loop:
				audio.loop = true;
				audio.Play ();
				break;
			}
		}
	}

	public void StopSound(string soundName)
	{
		AudioSource audio = null;
		if (m_dicAudio.ContainsKey (soundName)) {
			audio = m_dicAudio [soundName];
		}
		if (audio != null) {
			audio.Stop ();
		}
	}

	public void AllStop()
	{
		for (Dictionary<string, AudioSource>.Enumerator i = m_dicAudio.GetEnumerator(); i.MoveNext();) {
			i.Current.Value.Stop ();
		}
	}
}
