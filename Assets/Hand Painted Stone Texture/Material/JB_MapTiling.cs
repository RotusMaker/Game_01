using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JB_MapTiling : MonoBehaviour {

    public Material mat;

	void Start ()
    {
        if (mat != null)
        {
            Vector3 vScale = this.transform.localScale;
            mat.SetVector("_Tiling", new Vector4(vScale.x, vScale.z, 0f, 0f));
        }
        else
        {
            Debug.LogError("Not Found Material!");
        }
	}
}
