using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(TriggerBox))]
public class TriggerBoxEditor : Editor {

	TriggerBox myTarget;
	private ReorderableList list;

	private void OnEnable()
	{
		myTarget = (TriggerBox)target;
		list = new ReorderableList(serializedObject, serializedObject.FindProperty("m_listTriggingData"), true, true, true, true);

		list.elementHeight = EditorGUIUtility.singleLineHeight * 2f;

		list.drawHeaderCallback = (Rect rect) => {
			EditorGUI.LabelField(rect, "Bound");
		};

		list.drawElementCallback =  
			(Rect rect, int index, bool isActive, bool isFocused) => {
			var element = list.serializedProperty.GetArrayElementAtIndex(index);
			rect.y += 2;
			EditorGUI.PropertyField(
				new Rect(rect.x, rect.y, rect.width/2f, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("target"), GUIContent.none);
			EditorGUI.PropertyField(
				new Rect(rect.x + rect.width/2f, rect.y, rect.width/2f, EditorGUIUtility.singleLineHeight),
				element.FindPropertyRelative("method"), GUIContent.none);
		};
	}

	public override void OnInspectorGUI() 
	{
		EditorGUILayout.BeginVertical ("box");
		serializedObject.Update();
		list.DoLayoutList();
		serializedObject.ApplyModifiedProperties();
		EditorGUILayout.EndVertical ();
	}
}
