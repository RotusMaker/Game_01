using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(SW_Shooting))]
public class SW_ShootingEditor : Editor
{
    SW_Shooting myTarget;

    void OnEnable()
    {
        myTarget = target as SW_Shooting;
    }
    public override void OnInspectorGUI()
    {
        //myTarget.m_Root = (GameObject)EditorGUILayout.ObjectField("Root Object", myTarget.m_Root, typeof(GameObject), true);
        myTarget.m_boxCollider = (BoxCollider)EditorGUILayout.ObjectField("Switch Object", myTarget.m_boxCollider, typeof(BoxCollider), true);
        myTarget.m_Missile = (GameObject)EditorGUILayout.ObjectField("Missile Object", myTarget.m_Missile, typeof(GameObject), true);
        myTarget.m_nMissileCnt = EditorGUILayout.IntField("Missile Count", myTarget.m_nMissileCnt);
        myTarget.m_fMissileSpeed = EditorGUILayout.FloatField("Missile Speed", myTarget.m_fMissileSpeed);
        myTarget.m_fMissileGenTime = EditorGUILayout.FloatField("Missile Gen Time", myTarget.m_fMissileGenTime);

        EditorGUILayout.BeginVertical("box");
        GUILayout.Label(myTarget.m_updateDate);
        EditorGUILayout.EndVertical();
        if (GUILayout.Button("Update"))
        {
            if (Linking())
            {
                myTarget.m_updateDate = System.DateTime.Now.ToString();
            }
            else
            {
                myTarget.m_updateDate = "Link Error";
            }
        }
    }

    public bool Linking()
    {
        if (myTarget.m_listMissile != null)
        {
            for(int i=0; i< myTarget.m_listMissile.Count; i++)
            {
                GameObject.DestroyImmediate(myTarget.m_listMissile[i]);
            }
            myTarget.m_listMissile.Clear();
        }
        MakeMissileObject();
        return true;
    }

    public void MakeMissileObject()
    {
        if (myTarget.m_nMissileCnt > 0)
        {
            for(int i=0; i<myTarget.m_nMissileCnt; i++)
            {
                GameObject missile = GameObject.Instantiate(myTarget.m_Missile);
                missile.transform.localScale = Vector3.one;
                missile.transform.localPosition = Vector3.zero;
                missile.transform.parent = myTarget.transform;
                missile.layer = LayerMask.NameToLayer("Round");
                myTarget.m_listMissile.Add(missile);
            }
        }
    }
}
