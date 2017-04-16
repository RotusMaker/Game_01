using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GameRound))]
public class GameRoundEditor : Editor {

	GameRound myTarget;

	void OnEnable()
	{
		myTarget = target as GameRound;
	}
	public override void OnInspectorGUI()
	{
		myTarget.m_Root = (GameObject)EditorGUILayout.ObjectField("Camera Object", myTarget.m_Root, typeof(GameObject), true);
		EditorGUILayout.BeginVertical ("box");
		GUILayout.Label (myTarget.m_updateDate);
		EditorGUILayout.EndVertical ();
		if (GUILayout.Button ("Update")) {
			if (Linking ()) {
				myTarget.m_updateDate = System.DateTime.Now.ToString ();
			}
			else {
				myTarget.m_updateDate = "Link Error";
			}
		}
	}

	public bool Linking()
	{
		if (myTarget.m_listTriggerObj != null) {
			myTarget.m_listTriggerObj.Clear ();
		}
		FindClassChild (myTarget.m_Root.transform);
		return true;
	}

	public void FindClassChild(Transform root)
	{
		for (int i = 0; i < root.childCount; i++) {
			Transform child = root.GetChild (i);
			if (child.GetComponent<TriggerRoot> ()) {
				Debug.Log (string.Format("Add Object: {0}",child.name));
				myTarget.m_listTriggerObj.Add (child.GetComponent<TriggerRoot>());
			}
			if (child.childCount > 0) {
				FindClassChild (child);
			}
		}
	}
}
