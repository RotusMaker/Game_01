using UnityEngine;
using System.Collections;

public static class FTGlobal {
	//[System Info]
	public static string GAME_NAME = "";
	public static string GAME_VERSION = "";

	//[Paths]
	public static string PATH_PATTERN_PREFAB = "";
	public static string PATH_EASY_PATTERN = PATH_PATTERN_PREFAB + "";
	public static string PATH_NORMAL_PATTERN = PATH_PATTERN_PREFAB + "";
	public static string PATH_HARD_PATTERN = PATH_PATTERN_PREFAB + "";

	public static string getPatternPrefabPath(int gameLevel, int boxId)
	{
		return "";
	}

	//[Game Info]
	public static int nGameScore = 0;


	//[Utils]
	public static void SaveGameData()
	{
		//PlayerPrefs.SetString ();
	}
}
