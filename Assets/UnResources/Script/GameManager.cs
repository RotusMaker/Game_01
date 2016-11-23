using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>
{
	private GameObject m_player;
	
	public enum eGameState
	{
		None,
		Loading,
		Ready,
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
	}
	
	void Update () 
	{
		DoState (GetGameState ());
	}

	private void EnterState(eGameState state)
	{
	}

	private void DoState(eGameState state)
	{
	}

	private void EndState(eGameState state)
	{
	}

	private void ResetGame()
	{
	}
	private void LoadGame(string sectionName, int stageNumber)
	{
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
}
