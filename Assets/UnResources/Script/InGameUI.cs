using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour 
{
	private GameObject m_objRoot;
	private Dictionary<string, GameObject> m_dicPopup = new Dictionary<string, GameObject> ();
	private Dictionary<string, InputField> m_dicInputField = new Dictionary<string, InputField>();

	void Start()
	{
		m_dicPopup.Clear ();
		m_dicInputField.Clear ();

		m_objRoot = this.gameObject;

		if (m_objRoot == null) {
			Debug.LogError ("Not Found UIRoot.");
		}
		else {
			m_dicPopup.Add("Popup_option", m_objRoot.transform.FindChild ("Popup_option").gameObject);

			m_dicInputField.Add ("bg", m_objRoot.transform.FindChild ("Popup_option/InputField_bg").GetComponent<InputField>());
			m_dicInputField.Add ("stage", m_objRoot.transform.FindChild ("Popup_option/InputField_stage").GetComponent<InputField>());
		}
	}

	public void OnClickedOption()
	{
		if (m_dicPopup.ContainsKey ("Popup_option")) {
			m_dicPopup ["Popup_option"].SetActive (!m_dicPopup ["Popup_option"].activeSelf);
		}
	}

	public void OnClickedStart()
	{
		if (m_dicInputField.ContainsKey ("bg") && m_dicInputField.ContainsKey ("stage")) 
		{
			LoadPrefabManager.GetInstance.ResetBackground ();
			LoadPrefabManager.GetInstance.ResetStage ();

			string bgName = m_dicInputField ["bg"].text;
			string stageName = m_dicInputField ["stage"].text;
			GameManager.GetInstance.m_gameLoadInfo = new GameManager.GameLoadInfo ();
			GameManager.GetInstance.m_gameLoadInfo.mapName = bgName;
			GameManager.GetInstance.m_gameLoadInfo.stageName = stageName;
			GameManager.GetInstance.SetState (GameManager.eGameState.Loading_Background);
		}
	}
}
