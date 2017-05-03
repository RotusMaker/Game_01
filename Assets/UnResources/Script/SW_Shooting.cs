﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SW_Shooting : SW_Root
{
    public GameObject m_Root;
    public GameObject m_Missile;
    public List<GameObject> m_listMissile;
    public int m_nMissileCnt = 0;    // 한번에 나오는 미사일 총 개수(재활용 가능)
    public float m_fMissileSpeed = 0f;  // 미사일 속도
    public float m_fMissileGenTime = 0f;    // 미사일 생성 간격
    public BoxCollider m_boxCollider;  // 미사일 나오는 영역 = Switch 영역
    public string m_updateDate;

    private Vector3 orgPosition = Vector3.zero;
    private Vector3 m_vecMinArea = Vector3.zero;   // 미사일 영역 수치화
    private Vector3 m_vecMaxArea = Vector3.zero;
    private bool m_bPlayAction = false;
    private float m_fGapTime = 0f;
    private float m_fSpeedTime = 0f;

    private void Start()
    {
        //m_boxCollider = this.transform.GetComponent<BoxCollider>();
        m_Root.transform.position = m_boxCollider.transform.position;
        m_vecMaxArea = m_boxCollider.bounds.max - m_boxCollider.bounds.max/2f;
        m_vecMinArea = m_boxCollider.bounds.min - m_boxCollider.bounds.max/2f;
        //Debug.LogError(string.Format("### max: {0} min: {1}", m_vecMaxArea, m_vecMinArea));
    }

    public override void Reset()
    {
        base.Reset();
        orgPosition = this.transform.localPosition;
        m_bPlayAction = false;
        if (m_listMissile != null)
        {
            for (int i = 0; i < m_listMissile.Count; i++)
            {
                m_listMissile[i].SetActive(false);
            }
        }
        this.gameObject.SetActive(true);
    }

    public void OnSwitchEnter()
    {
        if (m_listMissile == null)
        {
            Debug.LogError("not found missile list.");
            return;
        }

        m_bPlayAction = true;
        orgPosition = this.transform.localPosition;
    }

    public void OnSwitchExit()
    {
        m_bPlayAction = false;
        for (int i = 0; i < m_listMissile.Count; i++)
        {
            m_listMissile[i].SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (m_bPlayAction)
        {
            // 생성
            m_fGapTime += Time.deltaTime;
            if (m_fGapTime >= m_fMissileGenTime)
            {
                m_fGapTime = 0f;
                for (int i = 0; i < m_listMissile.Count; i++)
                {
                    if (m_listMissile[i].activeSelf == false)
                    {
                        int RandomX = Random.Range((int)m_vecMinArea.x, (int)m_vecMaxArea.x);
                        int RandomY = Random.Range((int)m_vecMinArea.y, (int)m_vecMaxArea.y);
                        float length = (m_vecMaxArea.z - m_vecMinArea.z)/2f;
                        m_listMissile[i].transform.localPosition = new Vector3((float)RandomX, (float)RandomY, m_vecMaxArea.z + length);
                        m_listMissile[i].SetActive(true);
                        break;
                    }
                }
            }

            // 이동
            //m_fSpeedTime += Time.deltaTime;
            //if (m_fSpeedTime >= m_fMissileSpeed)
            {
                //m_fSpeedTime = 0f;
                for (int i = 0; i < m_listMissile.Count; i++)
                {
                    if (m_listMissile[i].activeSelf == true)
                    {
                        m_listMissile[i].transform.localPosition = new Vector3(m_listMissile[i].transform.localPosition.x, m_listMissile[i].transform.localPosition.y, m_listMissile[i].transform.localPosition.z - (Time.deltaTime * m_fMissileSpeed));
                        if (m_listMissile[i].transform.localPosition.z <= m_vecMinArea.z)
                        {
                            m_listMissile[i].SetActive(false);
                        }
                    }
                }
            }
        }
    }
}