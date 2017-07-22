using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadPrefabManager : MonoSingleton<LoadPrefabManager>
{	
	// 현재 한번에 한개씩 로드해야함. 추후 컨테이너로 변경하자.
	[HideInInspector] public float m_fCurrentProgress = 0f;
	[HideInInspector] public bool m_bLoaded = false;
	private Dictionary<string, GameObject> m_dicBackground = new Dictionary<string, GameObject> ();
	private Dictionary<string, GameObject> m_dicStage = new Dictionary<string, GameObject> ();

	public GameObject GetStage(eStageType stageType, int stageID)
	{
		string stageName = string.Format ("Round_{0}_{1:00}", stageType.ToString (), stageID); 
		if (stageType == eStageType.Pattern) {
			stageName = string.Format ("Pattern - e{0}", stageID); 
		}
        else if (stageType == eStageType.Test)
        {
            stageName = string.Format("Round_T_{0:00}", stageID);
        }

		if (m_dicStage.ContainsKey (stageName)) {
			return m_dicStage [stageName];
		}
		return null;
	}

    // 만들어진건 강제로 추가
    public void SetStage(eStageType stageType, int stageID, GameObject stage)
    {
        string stageName = string.Format("Round_{0}_{1:00}", stageType.ToString(), stageID);
        if (m_dicStage.ContainsKey(stageName) == false)
        {
            m_dicStage.Add(stageName, stage);
        }
    }

    public void SetMap(int mapID, GameObject map)
    {
        string mapName = string.Format("Map_{0:00}", mapID);

        if (m_dicBackground.ContainsKey(mapName) == false)
        {
            m_dicBackground.Add(mapName, map);
        }
    }

	public void LoadStage(eStageType stageType, int stageID, Transform parent)
	{
		m_bLoaded = false;
		string stageName = string.Format ("Round_{0}_{1:00}",stageType.ToString(),stageID);
		string prefabName = string.Format ("Prefab/Stage/{0}",stageName);
		if (stageType == eStageType.Pattern) {
			stageName = string.Format ("Pattern - e{0}",stageID);
			prefabName = string.Format ("Pattern/{0}",stageName);
		}
        else if (stageType == eStageType.Test)
        {
            stageName = string.Format("Round_T_{0:00}", stageID);
            prefabName = string.Format("TestStage/{0}", stageName);
        }

		if (m_dicStage.ContainsKey (stageName)) 
		{
			m_dicStage [stageName].SetActive (true);
			m_bLoaded = true;
		} 
		else 
		{
			StartCoroutine (LoadingStage (prefabName, parent));
		}
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

	public void LoadMap(int mapID, Transform parent)
	{
		m_bLoaded = false;
		string mapName = string.Format ("Map_{0:00}",mapID);
		string prefabName = string.Format ("Prefab/Background/{0}",mapName);

		if (m_dicBackground.ContainsKey (mapName)) 
		{
			m_dicBackground [mapName].SetActive (true);
			m_bLoaded = true;
		} 
		else 
		{
			StartCoroutine (LoadingMap (prefabName, parent));
		}
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

			string[] splitKey = prefabName.Split ('/');
			m_dicBackground.Add (splitKey[splitKey.Length-1], copyPrefab);
			Debug.Log ("Add Background: " + splitKey[splitKey.Length-1]);
		}

		m_bLoaded = true;
	}

    public void ClearData()
    {
        for (Dictionary<string, GameObject>.Enumerator it = m_dicBackground.GetEnumerator(); it.MoveNext();)
        {
            DestroyImmediate(it.Current.Value);
        }
        for (Dictionary<string, GameObject>.Enumerator it = m_dicStage.GetEnumerator(); it.MoveNext();)
        {
            DestroyImmediate(it.Current.Value);
        }
        m_dicBackground.Clear();
        m_dicStage.Clear();
    }

	public void ResetBackground()
	{
		for (Dictionary<string, GameObject>.Enumerator it = m_dicBackground.GetEnumerator (); it.MoveNext ();) 
		{
			it.Current.Value.SetActive (false);
		}
	}

	public void ResetStage()
	{
		for (Dictionary<string, GameObject>.Enumerator it = m_dicStage.GetEnumerator (); it.MoveNext ();) 
		{
			it.Current.Value.SetActive (false);
		}
	}
}
