using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>
{
	public GameObject m_player;
	private Moving m_movePlayer;
	
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
		m_movePlayer = m_player.GetComponent<Moving> ();

		SetState (eGameState.Play);
	}
	
	void Update () 
	{
		DoState (GetGameState ());
	}

	private void EnterState(eGameState state)
	{
		switch(m_gameState)
		{
		case eGameState.Result:
			StartCoroutine (Result ());
			break;
		}
	}

	private void DoState(eGameState state)
	{
		switch (m_gameState) {
		case eGameState.Play:
			if (m_movePlayer != null) {
				if (m_movePlayer.IsDead ()) {
					SetState (eGameState.Result);
				}
			}
			break;
		case eGameState.Result:
			break;
		}
	}

	private void EndState(eGameState state)
	{
	}

	private void ResetGame()
	{
		SetState (eGameState.Play);
		m_movePlayer.ResetGame();
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

	IEnumerator Result()
	{
		Debug.Log ("Result 연출.");
		yield return new WaitForSeconds(1.5f);
		Debug.Log ("Result 연출 끝");
		ResetGame ();
	}
}
