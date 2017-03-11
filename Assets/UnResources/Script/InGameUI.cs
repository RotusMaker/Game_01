using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour 
{
	private GameObject m_objRoot;
	private Dictionary<string, GameObject> m_dicPopup = new Dictionary<string, GameObject> ();
	private Dictionary<string, InputField> m_dicInputField = new Dictionary<string, InputField>();
	private Dictionary<string, Text> m_dicText = new Dictionary<string, Text>();
	private Slider m_resultSlider;

	void Start()
	{
		m_dicPopup.Clear ();
		m_dicInputField.Clear ();
		m_dicText.Clear ();

		m_objRoot = this.gameObject;

		if (m_objRoot == null) {
			Debug.LogError ("Not Found UIRoot.");
		}
		else {
			m_dicPopup.Add("Popup_option", m_objRoot.transform.FindChild ("Popup_option").gameObject);
			m_dicPopup.Add ("Popup_Loading", m_objRoot.transform.FindChild ("Popup_Loading").gameObject);
			m_dicPopup.Add("Popup_Result", m_objRoot.transform.FindChild ("Popup_Result").gameObject);
			m_dicInputField.Add ("bg", m_objRoot.transform.FindChild ("Popup_option/InputField_bg").GetComponent<InputField>());
			m_dicInputField.Add ("stage", m_objRoot.transform.FindChild ("Popup_option/InputField_stage").GetComponent<InputField>());
			m_dicText.Add("Popup_Loading/Text",m_objRoot.transform.FindChild ("Popup_Loading/Text").GetComponent<Text>());
			m_dicText.Add("Popup_Result/Text",m_objRoot.transform.FindChild ("Popup_Result/Text").GetComponent<Text>());
			m_dicText.Add("Popup_Result/DistanceText",m_objRoot.transform.FindChild ("Popup_Result/DistanceText").GetComponent<Text>());
			m_dicText.Add("Infomation/Text",m_objRoot.transform.FindChild ("Infomation/Text").GetComponent<Text>());

			m_resultSlider = m_objRoot.transform.FindChild ("Popup_Result/Slider").GetComponent<Slider> ();
		}
	}

	public void SetScoreText(int score)
	{
		if (m_dicText.ContainsKey ("Infomation/Text")) {
			m_dicText ["Infomation/Text"].text = score.ToString();
		}
	}

	public void OnClickedOption()
	{
		if (m_dicPopup.ContainsKey ("Popup_option")) {
			m_dicPopup ["Popup_option"].SetActive (!m_dicPopup ["Popup_option"].activeSelf);
		}
	}

	public void OnLoading(bool isOn, string content)
	{
		if (m_dicPopup.ContainsKey ("Popup_Loading")) {
			m_dicPopup ["Popup_Loading"].SetActive (isOn);
			if (m_dicText.ContainsKey ("Popup_Loading/Text")) {
				m_dicText ["Popup_Loading/Text"].text = content;
			}
		}
	}

	// 결과창 열기.
	public void ActiveResultPopup(bool active, string content, float distance, float maxDistance)
	{
		if (m_dicPopup.ContainsKey ("Popup_Result")) {
			m_dicPopup ["Popup_Result"].SetActive (active);
			if (active == true) {
				if (m_dicText.ContainsKey ("Popup_Result/Text")) {
					m_dicText ["Popup_Result/Text"].text = content;
				}
				if (m_dicText.ContainsKey ("Popup_Result/DistanceText")) {
					m_dicText ["Popup_Result/DistanceText"].text = distance.ToString();
					m_resultSlider.maxValue = maxDistance;
					m_resultSlider.minValue = 0f;
					m_resultSlider.value = distance;
				}
			}
		}
	}

	public void OnResultSliderUpdate()
	{
		if (m_dicText.ContainsKey ("Popup_Result/DistanceText")) {
			m_dicText ["Popup_Result/DistanceText"].text = m_resultSlider.value.ToString ();
		}
	}

	public void OnClickedReStart()
	{
		ActiveResultPopup (false, string.Empty, 0f, 1f);
		float startDistance = m_resultSlider.value;
		GameManager.GetInstance.RestartGame (startDistance);
	}

	public void OnClickedNextStart()
	{
		ActiveResultPopup (false, string.Empty, 0f, 1f);

		//LoadPrefabManager.GetInstance.ResetBackground ();
		LoadPrefabManager.GetInstance.ResetStage ();

		//GameManager.GetInstance.m_gameLoadInfo = new GameManager.GameLoadInfo ();
		//GameManager.GetInstance.m_gameLoadInfo.mapID = GameManager.GetInstance.m_gameLoadInfo.mapID;
		int stageID = GameManager.GetInstance.m_gameLoadInfo.stageID + 1;
		if (stageID <= 0) {
			stageID = 1;
		}
		GameManager.GetInstance.m_gameLoadInfo.stageID = stageID;
		GameManager.GetInstance.m_fDistance = 0f;
		GameManager.GetInstance.m_nGameScore = 0;
		GameManager.GetInstance.SetState (GameManager.eGameState.Loading_Stage);
	}

	public void OnClickedPrevStart()
	{
		ActiveResultPopup (false, string.Empty, 0f, 1f);

		//LoadPrefabManager.GetInstance.ResetBackground ();
		LoadPrefabManager.GetInstance.ResetStage ();

		//GameManager.GetInstance.m_gameLoadInfo = new GameManager.GameLoadInfo ();
		//GameManager.GetInstance.m_gameLoadInfo.mapID = GameManager.GetInstance.m_gameLoadInfo.mapID;
		int stageID = GameManager.GetInstance.m_gameLoadInfo.stageID - 1;
		if (stageID <= 0) {
			stageID = 1;
		}
		GameManager.GetInstance.m_gameLoadInfo.stageID = stageID;
		GameManager.GetInstance.m_fDistance = 0f;
		GameManager.GetInstance.m_nGameScore = 0;
		GameManager.GetInstance.SetState (GameManager.eGameState.Loading_Stage);
	}

	public void OnClickedStart()
	{
		if (m_dicInputField.ContainsKey ("bg") && m_dicInputField.ContainsKey ("stage")) 
		{
			LoadPrefabManager.GetInstance.ResetBackground ();
			LoadPrefabManager.GetInstance.ResetStage ();

			string bgID = m_dicInputField ["bg"].text;
			string stageID = m_dicInputField ["stage"].text;
			GameManager.GetInstance.m_gameLoadInfo = new GameManager.GameLoadInfo ();
			GameManager.GetInstance.m_gameLoadInfo.mapID = System.Convert.ToInt32(bgID);
			GameManager.GetInstance.m_gameLoadInfo.stageID = System.Convert.ToInt32(stageID);
			GameManager.GetInstance.SetState (GameManager.eGameState.Loading_Background);
		}
	}
}
