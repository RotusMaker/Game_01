using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eSoundPlayType
{
	OneShot,
	Loop,
}

public enum eSoundState
{
    Fast_Step,
    Dash,
    Dead,
    Hit_Dead,
    Jump,
}

public class SoundManager : MonoSingleton<SoundManager> {

	public GameObject m_Root;
	public string m_updateDate;
	public List<AudioSource> m_listAudio;
	private Dictionary<string, AudioSource> m_dicAudio;
    private Dictionary<string, int> m_dicSoundValue;

	void Awake()
	{
		if (m_dicAudio == null) {
			m_dicAudio = new Dictionary<string, AudioSource> ();
		}
        if (m_dicSoundValue == null)
        {
            m_dicSoundValue = new Dictionary<string, int>();
        }
		for (int i = 0; i < m_listAudio.Count; i++) {
			m_dicAudio.Add (m_listAudio [i].name, m_listAudio [i]);
		}
	}

    public bool IsPlaySound(string soundName)
    {
        if (m_dicAudio != null && m_dicAudio.ContainsKey(soundName))
        {
            return m_dicAudio[soundName].isPlaying;
        }
        return false;
    }

	public void PlaySound(string soundName, eSoundPlayType type)
	{
		AudioSource audio = null;

		if (m_dicAudio != null && m_dicAudio.ContainsKey (soundName)) {
			audio = m_dicAudio [soundName];
		}
		if (audio != null)
        {
			switch (type)
            {
			    case eSoundPlayType.OneShot:
                    if (!audio.isPlaying)
                    {
                        audio.Play ();
				    }
				    break;
			    case eSoundPlayType.Loop:
				    audio.loop = true;
				    audio.Play ();
                    break;
                default:
                    break;
			}
		}
	}

    public void PlayRandomPickSound(eSoundState type)
    {
        if (m_dicSoundValue.ContainsKey(type.ToString()) == false)
        {
            m_dicSoundValue.Add(type.ToString(), 0);
        }

        int randValue = 0;
        switch (type)
        {
            case eSoundState.Dash:
                randValue = Random.Range(0, 3);
                PlaySound(string.Format("dash_{0}", randValue), eSoundPlayType.OneShot);
                break;
            case eSoundState.Fast_Step:
                if (m_dicSoundValue[type.ToString()] == 0)
                {
                    if (IsPlaySound("fast_step_1") == false)
                    {
                        m_dicSoundValue[type.ToString()] = 1;
                        PlaySound("fast_step_0", eSoundPlayType.OneShot);
                    }
                }
                else
                {
                    if (IsPlaySound("fast_step_0") == false)
                    {
                        m_dicSoundValue[type.ToString()] = 0;
                        PlaySound("fast_step_1", eSoundPlayType.OneShot);
                    }
                }
                break;
            case eSoundState.Dead:
                randValue = Random.Range(0, 2);
                PlaySound(string.Format("die_{0}", randValue), eSoundPlayType.OneShot);
                break;
            case eSoundState.Hit_Dead:
                randValue = Random.Range(0, 3);
                PlaySound(string.Format("hit_{0}", randValue), eSoundPlayType.OneShot);
                break;
            case eSoundState.Jump:
                randValue = Random.Range(0, 4);
                PlaySound(string.Format("jump_{0}", randValue), eSoundPlayType.OneShot);
                break;
            default:
                break;
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
