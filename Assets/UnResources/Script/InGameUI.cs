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
			m_dicInputField.Add ("bg", m_objRoot.transform.FindChild ("Popup_option/InputField_bg").GetComponent<InputField>());
			m_dicInputField.Add ("stage", m_objRoot.transform.FindChild ("Popup_option/InputField_stage").GetComponent<InputField>());
			m_dicText.Add("Popup_Loading/Text",m_objRoot.transform.FindChild ("Popup_Loading/Text").GetComponent<Text>());
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
