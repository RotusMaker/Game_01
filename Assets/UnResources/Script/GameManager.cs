﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>
{
	public class GameLoadInfo
	{
		public int mapID;
		public int stageID;
	}
	
	public GameObject m_player;
	public GameObject m_root;
	public InGameUI m_ui;
	[HideInInspector] public GameLoadInfo m_gameLoadInfo = null;
	private Moving m_movePlayer;

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
		m_movePlayer = m_player.GetComponent<Moving> ();
		m_movePlayer.ResetGame ();

		SetState (eGameState.None);
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
				m_ui.OnLoading(true, "Background Loading...");
				if (m_gameLoadInfo != null) {
					LoadPrefabManager.GetInstance.LoadMap (m_gameLoadInfo.mapID, m_root.transform);
				}
			}
			break;
		case eGameState.Loading_Stage:
			{
				m_ui.OnLoading(true, "Stage Loading...");
				if (m_gameLoadInfo != null) {
					LoadPrefabManager.GetInstance.LoadStage (m_gameLoadInfo.stageID, m_root.transform);
				}
			}
			break;
		case eGameState.Ready:
			{
				m_ui.OnLoading(true, "Character Loading...");
				GameObject stageObj = LoadPrefabManager.GetInstance.GetStage (m_gameLoadInfo.stageID);
				Transform startPos = stageObj.transform.FindChild ("StartPosition");
				if (startPos != null) {
					m_movePlayer.SetOrigPos (startPos.localPosition);
				}
				m_movePlayer.ResetGame ();
			}
			break;
		case eGameState.Play:
			{
				m_ui.OnLoading(false, "Go");
				m_movePlayer.SetState (Moving.ePlayerState.Run);
			}
			break;
		case eGameState.Result:
			{
				m_ui.OnLoading(true, "Game Reset Loading...");
				StartCoroutine (Result ());
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
				if (m_movePlayer.IsDead ())
				{
					SetState (eGameState.Result);
				}
			}
			break;
		case eGameState.Loading_Background:
			{
				//Debug.Log (string.Format("{0}% Loading...",LoadPrefabManager.GetInstance.m_fCurrentProgress));
				m_ui.OnLoading(true, string.Format("Background Loading...{0}%",LoadPrefabManager.GetInstance.m_fCurrentProgress));
				if (LoadPrefabManager.GetInstance.m_bLoaded == true) 
				{
					SetState (eGameState.Loading_Stage);
				}
			}
			break;
		case eGameState.Loading_Stage:
			{
				//Debug.Log (string.Format("{0}% Loading...",LoadPrefabManager.GetInstance.m_fCurrentProgress));
				m_ui.OnLoading(true, string.Format("Stage Loading...{0}%",LoadPrefabManager.GetInstance.m_fCurrentProgress));
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
		m_movePlayer.ResetGame();
		yield return null;
		GameObject stageObj = LoadPrefabManager.GetInstance.GetStage (m_gameLoadInfo.stageID);
		yield return StartCoroutine(SearchTrigger (stageObj));
		SetState (eGameState.Play);
	}

	private void LoadGame(string sectionName, int stageNumber)
	{
	}

	// 연산이 좀 많음.
	IEnumerator SearchTrigger(GameObject root)
	{
		for (int i = 0; i < root.transform.childCount; i++) {
			Transform child = root.transform.GetChild (i);
			ObjectTriggerEvent trigger = child.GetComponent<ObjectTriggerEvent>();
			if (trigger != null) {
				trigger.Reset ();
			}
			if (child.childCount > 0) {
				SearchTrigger (child.gameObject);
			}
			yield return null;
		}
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

	IEnumerator Result()
	{
		Debug.Log ("Result 연출.");
		yield return new WaitForSeconds(1.5f);
		Debug.Log ("Result 연출 끝");
		StartCoroutine(ResetGame ());
	}
}
