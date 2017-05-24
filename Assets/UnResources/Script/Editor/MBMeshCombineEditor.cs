using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MBMeshCombine))]
public class MBMeshCombineEditor : Editor {

	MBMeshCombine myTarget;

	private void OnEnable()
	{
		myTarget = (MBMeshCombine)target;
	}

	public override void OnInspectorGUI()
	{
		myTarget.parent = (GameObject)EditorGUILayout.ObjectField("Mesh Parent", myTarget.parent, typeof(GameObject));

		if (GUILayout.Button ("Combine")) {
			myTarget.CombineMeshs();
		}
	}
}
