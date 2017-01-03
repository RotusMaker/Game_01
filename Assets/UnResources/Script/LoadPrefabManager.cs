using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadPrefabManager : MonoSingleton<LoadPrefabManager>
{	
	// 현재 한번에 한개씩 로드해야함. 추후 컨테이너로 변경하자.
	[HideInInspector] public float m_fCurrentProgress = 0f;
	[HideInInspector] public bool m_bLoaded = false;
	private Dictionary<string, GameObject> m_dicStage = new Dictionary<string, GameObject> ();

	public GameObject GetStage(string stageName)
	{
		if (m_dicStage.ContainsKey (stageName)) {
			return m_dicStage [stageName];
		}
		return null;
	}

	public void LoadStage(string stageName, Transform parent)
	{
		m_bLoaded = false;
		string prefabName = string.Format ("Prefab/Stage/{0}",stageName);
		StartCoroutine (LoadingStage (prefabName, parent));
	}

	IEnumerator LoadingStage(string prefabName, Transform parent)
	{
		if (m_dicStage.ContainsKey (prefabName)) {
			yield break;
		}

		m_bLoaded = false;

		ResourceRequest req = Resources.LoadAsync<GameObject> (prefabName);
		while (!req.isDone) 
		{
			m_fCurrentProgress = req.progress * 100f;
			yield return null;
		}

		GameObject prefabObj = req.asset as GameObject;
		if (prefabObj != null)
		{
			GameObject copyPrefab = GameObject.Instantiate (prefabObj);
			copyPrefab.transform.parent = parent;
			copyPrefab.transform.localScale = Vector3.one;
			copyPrefab.transform.localPosition = Vector3.zero;

			string[] splitKey = prefabName.Split ('/');
			m_dicStage.Add (splitKey[splitKey.Length-1], copyPrefab);
			Debug.Log ("Add Stage: " + splitKey[splitKey.Length-1]);
		}

		m_bLoaded = true;
	}

	public void LoadMap(string mapName, Transform parent)
	{
		m_bLoaded = false;
		string prefabName = string.Format ("Prefab/Background/{0}",mapName);
		StartCoroutine (LoadingMap (prefabName, parent));
	}

	IEnumerator LoadingMap(string prefabName, Transform parent)
	{
		m_bLoaded = false;

		ResourceRequest req = Resources.LoadAsync<GameObject> (prefabName);
		while (!req.isDone) 
		{
			m_fCurrentProgress = req.progress * 100f;
			yield return null;
		}

		GameObject prefabObj = req.asset as GameObject;
		if (prefabObj != null)
		{
			GameObject copyPrefab = GameObject.Instantiate (prefabObj);
			copyPrefab.transform.parent = parent;
			copyPrefab.transform.localScale = Vector3.one;
			copyPrefab.transform.localPosition = Vector3.zero;
		}

		m_bLoaded = true;
	}
}
