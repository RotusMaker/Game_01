﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum eStageType
{
	R = 0,
	O,
	Pattern,
    Test,
};

public class GameManager : MonoSingleton<GameManager>
{
	public class GameLoadInfo
	{
		public int mapID;
		public int stageID;
		public eStageType stageType = eStageType.R;
	}

    [HideInInspector] public GameObject m_player;
    [HideInInspector] public GameObject m_root;
    [HideInInspector] public InGameUI m_ui;
	[HideInInspector] public GameLoadInfo m_gameLoadInfo = null;
	private CharacterBody m_movePlayer;
	[HideInInspector] public int m_nGameScore = 0;
	private float m_fScoreTick = 0f;
	[HideInInspector] public float m_fDistance = 0f;

	public enum eGameState
	{
		None,
		Loading_Background,	// 배경을 로드 한다.
		Loading_Stage,		// 스테이지를 로드 한다.
		Ready,				// 게임 시작 준비를 한다.
		Start,
		Play,
		Result
	}
	private eGameState m_gameState = eGameState.None;
	public eGameState GetGameState()
	{
		return m_gameState;
	}

	public void SetState(eGameState state)
	{
		if (m_gameState != state) 
		{
			EndState (m_gameState);
			m_gameState = state;
			EnterState (m_gameState);
		}
	}
    
	void Start ()
	{
        /*
		m_movePlayer = m_player.GetComponent<CharacterBody> ();
		m_movePlayer.ResetGame (m_fDistance);
		SetState (eGameState.None);
        */
	}

    public void InitManager(CharacterBody player, GameObject root)
    {
        m_root = root;
        //m_movePlayer = m_player.GetComponent<CharacterBody>();
        m_movePlayer = player;
        m_movePlayer.ResetGame(m_fDistance);
        SetState(eGameState.None);
    }
	
	void Update () 
	{
		DoState (GetGameState ());
	}

	private void EnterState(eGameState state)
	{
		switch(m_gameState)
		{
		case eGameState.Loading_Background:
			{
				//Debug.Log ("Background Load.");
                if (m_ui != null)
                {
                    m_ui.OnLoading(true, "Background Loading...");
                }
				if (m_gameLoadInfo != null) {
					LoadPrefabManager.GetInstance.LoadMap (m_gameLoadInfo.mapID, m_root.transform);
				}
			}
			break;
		case eGameState.Loading_Stage:
			{
                if (m_ui != null)
                {
                    m_ui.OnLoading(true, "Stage Loading...");
                }
				if (m_gameLoadInfo != null) {
					LoadPrefabManager.GetInstance.LoadStage (m_gameLoadInfo.stageType, m_gameLoadInfo.stageID, m_root.transform);
				}
			}
			break;
		case eGameState.Ready:
			{
                if (m_ui != null)
                {
                    m_ui.OnLoading(true, "Character Loading...");
                }
				GameObject stageObj = LoadPrefabManager.GetInstance.GetStage (m_gameLoadInfo.stageType, m_gameLoadInfo.stageID);
				Transform startPos = stageObj.transform.FindChild ("StartPosition");
				if (startPos != null) {
					m_movePlayer.SetOrigPos (startPos.localPosition);
				}
				m_movePlayer.ResetGame (m_fDistance);
			}
			break;
		case eGameState.Play:
			{
                if (m_ui != null)
                {
                    m_ui.OnLoading(false, "Go");
                }
				m_movePlayer.SetState (CharacterBody.ePlayerState.Run);
			}
			break;
		case eGameState.Result:
			{
				//m_ui.OnLoading(false, "Game Reset Loading...");
				StartCoroutine (Result (m_movePlayer.IsGoal()));
			}
			break;
		}
	}

	private void DoState(eGameState state)
	{
		switch (m_gameState) {
		case eGameState.Play:
			if (m_movePlayer != null)
			{
				if (m_movePlayer.IsDead () || m_movePlayer.IsGoal ()) {
					SetState (eGameState.Result);
				} 
				else {
					m_fScoreTick += Time.deltaTime;
					if (m_fScoreTick >= 0.1f) {
						m_fScoreTick = 0f;
						m_nGameScore += 1;
                        if (m_ui != null)
                        {
                            m_ui.SetScoreText(m_nGameScore);
                        }
					}
				}
			}
			break;
		case eGameState.Loading_Background:
			{
				//Debug.Log (string.Format("{0}% Loading...",LoadPrefabManager.GetInstance.m_fCurrentProgress));
                if (m_ui != null)
                {
                    m_ui.OnLoading(true, string.Format("Background Loading...{0}%", LoadPrefabManager.GetInstance.m_fCurrentProgress));
                }
				if (LoadPrefabManager.GetInstance.m_bLoaded == true) 
				{
					SetState (eGameState.Loading_Stage);
				}
			}
			break;
		case eGameState.Loading_Stage:
			{
				//Debug.Log (string.Format("{0}% Loading...",LoadPrefabManager.GetInstance.m_fCurrentProgress));
                if (m_ui != null)
                {
                    m_ui.OnLoading(true, string.Format("Stage Loading...{0}%", LoadPrefabManager.GetInstance.m_fCurrentProgress));
                }
				if (LoadPrefabManager.GetInstance.m_bLoaded == true) 
				{
					SetState (eGameState.Ready);
				}
			}
			break;
		case eGameState.Ready:
			{
				SetState (eGameState.Play);
			}
			break;
		}
	}

	private void EndState(eGameState state)
	{
		switch (state) 
		{
		case eGameState.Loading_Background:
			{
				Debug.Log ("End Background Loading.");
			}
			break;
		case eGameState.Loading_Stage:
			{
				Debug.Log ("End Stage Loading.");
			}
			break;
		}
	}

	IEnumerator ResetGame()
	{
		m_nGameScore = 0;
        if (m_ui != null)
        {
            m_ui.SetScoreText(0);
            m_ui.OnDamageInfo(0, 0f);
            m_ui.OnDamageInfo(1, 0f);
        }
		m_movePlayer.ResetGame(m_fDistance);
		//Debug.LogWarning ("Distance: " + m_fDistance.ToString());
		yield return null;
		GameObject stageObj = LoadPrefabManager.GetInstance.GetStage (m_gameLoadInfo.stageType,m_gameLoadInfo.stageID);
		yield return StartCoroutine(SearchTrigger (stageObj));
		SetState (eGameState.Play);
	}

	private void LoadGame(string sectionName, int stageNumber)
	{
	}

	// 연산이 좀 많음.
	IEnumerator SearchTrigger(GameObject root)
	{
		yield return null;
		if (root != null) {
			GameRound round = root.GetComponent<GameRound>();
			if (round != null) {
				for (int i = 0; i < round.m_listTriggerObj.Count; i++) {
					round.m_listTriggerObj[i].GetComponent<TriggerRoot>().Reset();
					yield return null;
				}
			}
		}

		/*
		for (int i = 0; i < root.transform.childCount; i++) {
			Transform child = root.transform.GetChild (i);
			ObjectTriggerEvent trigger = child.GetComponent<ObjectTriggerEvent>();
			if (trigger != null) {
				trigger.Reset ();
			}
			if (child.childCount > 0) {
				yield return StartCoroutine(SearchTrigger (child.gameObject));
			}
		}
		*/
	}

	IEnumerator Loading()
	{
		// 게임 리소스 로드 및 진행상황 UI.
		yield return null;
	}

	IEnumerator Ready()
	{
		// 게임 진행 카운트 다운 및 UI 초기화 애니메이션.
		yield return null;
	}

	IEnumerator Result(bool success)
	{
		yield return null;
		m_fDistance = m_movePlayer.transform.localPosition.z;
		string content = string.Format("{0}\n{1}Point",success?"Success!":"Failed!",m_nGameScore);
        if (m_ui != null)
        {
            m_ui.ActiveResultPopup(true, content, m_fDistance, m_fDistance * 2f);	//결과 팝업을 띄움.
        }

        yield return new WaitForSeconds(1f);

        // 레벨 씬으로 이동
        /*
        AsyncOperation m_Async = null;
        m_Async = SceneManager.LoadSceneAsync("ToonyLITE Demo 03 - Landscape - Level Select");
        yield return m_Async;
        */

        RestartGame();
    }

	public void RestartGame(float startDistance = 0f)
	{
		m_fDistance = startDistance;
		StartCoroutine(ResetGame ());
	}
}
