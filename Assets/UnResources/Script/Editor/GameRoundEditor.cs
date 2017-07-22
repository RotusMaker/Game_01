using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(GameRound))]
public class GameRoundEditor : Editor {

	GameRound myTarget;
    Dictionary<string, int> dicMaterialNames = new Dictionary<string, int>();

	void OnEnable()
	{
		myTarget = target as GameRound;
	}
	public override void OnInspectorGUI()
	{
		myTarget.m_Root = (GameObject)EditorGUILayout.ObjectField("Root Object", myTarget.m_Root, typeof(GameObject), true);
		EditorGUILayout.BeginVertical ("box");
		GUILayout.Label (myTarget.m_updateDate);
		EditorGUILayout.EndVertical ();
		if (GUILayout.Button ("Update")) {
			if (Linking ())
            {
                string matInfo = "[Materials]\n";
                for(Dictionary<string,int>.Enumerator it = dicMaterialNames.GetEnumerator(); it.MoveNext();)
                {
                    matInfo += string.Format("{0}:{1}", it.Current.Key,it.Current.Value);
                    matInfo += "\n";
                }
				myTarget.m_updateDate = string.Format("{0}\n[UpdateTime]\n{1}",matInfo,System.DateTime.Now.ToString ());
			}
			else
            {
				myTarget.m_updateDate = "Link Error";
			}
            Debug.Log(myTarget.m_updateDate);
		}
	}

	public bool Linking()
	{
		if (myTarget.m_listTriggerObj != null) {
			myTarget.m_listTriggerObj.Clear ();
		}
        dicMaterialNames.Clear();
        FindClassChild (myTarget.m_Root.transform);
		return true;
	}

	public void FindClassChild(Transform root)
	{
		for (int i = 0; i < root.childCount; i++) {
			Transform child = root.GetChild (i);
			if (child.GetComponent<TriggerRoot> () != null) {
				Debug.Log (string.Format ("Add Object: {0}", child.name));
				myTarget.m_listTriggerObj.Add (child.GetComponent<TriggerRoot> ());
			} 
			else {
				Debug.Log (string.Format("Not Add Object: {0}",child.name));
			}

            // 사용된 매터리얼 종류와 개수 정보
            if (child.GetComponent<MeshRenderer>() != null)
            {
                if (child.GetComponent<MeshRenderer>().materials != null)
                {
                    for(int j=0; j<child.GetComponent<MeshRenderer>().materials.Length; j++)
                    {
                        Material mat = child.GetComponent<MeshRenderer>().materials[j];
                        if (dicMaterialNames.ContainsKey(mat.name))
                        {
                            dicMaterialNames[mat.name] += 1;
                        }
                        else
                        {
                            dicMaterialNames.Add(mat.name, 1);
                        }
                    }
                }
            }

			if (child.childCount > 0) {
				FindClassChild (child);
			}
		}
	}
}
