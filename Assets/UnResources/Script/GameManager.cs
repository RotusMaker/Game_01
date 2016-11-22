using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>
{
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
		LoadPrefabManager.instance.LoadPrefab ("r", 0);
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
}
