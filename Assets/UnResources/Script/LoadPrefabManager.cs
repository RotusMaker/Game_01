using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadPrefabManager : MonoSingleton<LoadPrefabManager>
{
	public float m_fCurrentProgress = 0f;
	private Dictionary<string,GameObject> m_dicPrefabs = new Dictionary<string, GameObject> ();

	void Start()
	{
		m_dicPrefabs.Clear();
	}

	public void LoadPrefab(string sectionName, int stageNumber)
	{
		string prefabName = string.Format ("{0}_{1}",sectionName,stageNumber);
		StartCoroutine (LoadingPrefab (prefabName));
	}

	IEnumerator LoadingPrefab(string prefabName)
	{
		string prefabPath = string.Format ("Prefab/Pattern/{0}",prefabName);

		ResourceRequest req = Resources.LoadAsync<GameObject> (prefabPath);
		while (!req.isDone) 
		{
			m_fCurrentProgress = req.progress * 100f;
			yield return null;
		}

		GameObject prefabObj = req.asset as GameObject;
		m_dicPrefabs.Add (prefabName, prefabObj);
	}
}
