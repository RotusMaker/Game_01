using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

//Custom
using Rotus.Enum;
using UnityStandardAssets.Characters.FirstPerson;

namespace Rotus.Enum
{
	public enum ePlayerState
	{
		Idle = 0,
		Walk,
	}
}

public class FTFPSController : MonoBehaviour 
{
	private ePlayerState eState = ePlayerState.Idle;
	private RigidbodyFirstPersonController personController = null;

	void Start()
	{
		eState = ePlayerState.Walk;
	}

	void Update()
	{
		switch (eState) 
		{
		case ePlayerState.Idle:
			OnIdle ();
			break;
		case ePlayerState.Walk:
			OnWalk ();
			break;
		default:
			break;
		}
	}

	private void OnIdle()
	{
		Debug.Log ("### None State.");
	}

	private void OnWalk()
	{
		Debug.Log ("### Walk State.");
	}
}