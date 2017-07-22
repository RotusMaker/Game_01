using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static string stageName;

    public GameObject m_map;
    public GameObject[] m_stage;
    public CharacterBody m_character;
    public GameObject m_root;
     
	void Start ()
    {
        // 로드 매니저에 등록
        LoadPrefabManager.GetInstance.SetStage(eStageType.R, 1, m_stage[0]);
        LoadPrefabManager.GetInstance.SetStage(eStageType.R, 2, m_stage[1]);
        LoadPrefabManager.GetInstance.SetStage(eStageType.R, 3, m_stage[2]);
        LoadPrefabManager.GetInstance.SetMap(3, m_map);

        // 게임 매니저 초기화 & 실행
        GameManager.GetInstance.InitManager(m_character, m_root);
        GameManager.GetInstance.SetState(GameManager.eGameState.Loading_Background);
    }

    public void Button_Exit()
    {
        StartCoroutine(LoadLevelSelectScene());
    }

    IEnumerator LoadLevelSelectScene()
    {
        LoadPrefabManager.GetInstance.ClearData();
        GameManager.GetInstance.ChangeScene();

        AsyncOperation m_Async = null;
        m_Async = SceneManager.LoadSceneAsync("ToonyLITE Demo 03 - Landscape - Level Select");
        yield return m_Async;
    }
}
