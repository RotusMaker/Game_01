using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor {

	SoundManager myTarget;

	void OnEnable()
	{
		myTarget = target as SoundManager;
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
		if (myTarget.m_dicAudio == null) {
			myTarget.m_dicAudio = new System.Collections.Generic.Dictionary<string, AudioSource>();
		}
		FindClassChild (myTarget.m_Root.transform);
		return true;
	}

	public void FindClassChild(Transform root)
	{
		for (int i = 0; i < root.childCount; i++) {
			Transform child = root.GetChild (i);
			if (child.GetComponent<AudioSource>()) {
				Debug.Log (string.Format("Add Audio: {0}",child.name));
				child.GetComponent<AudioSource>().playOnAwake = false;
				myTarget.m_dicAudio.Add (child.name, child.GetComponent<AudioSource>());
			}
		}
	}
}
