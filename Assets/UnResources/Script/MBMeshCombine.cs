using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class MBMeshCombine : MonoBehaviour {
	public GameObject parent = null;

	public void CombineMeshs() {
#if UNITY_EDITOR
		Material material = null;
		MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>();
		MeshRenderer[] meshRenderers = parent.GetComponentsInChildren<MeshRenderer>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		int i = 0;
		while (i < meshFilters.Length) {
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			//combine[i].transform = meshFilters[i].transform.worldToLocalMatrix;
			//meshFilters[i].gameObject.active = false;

			if (material == null){
				material = meshRenderers[i].sharedMaterial;
			}

			i++;
		}
		Mesh combineMesh = new Mesh();
		combineMesh.CombineMeshes(combine);
		combineMesh.name = parent.name;

		Material combineMat = new Material (material);

		string filePath = EditorUtility.SaveFilePanelInProject("Save Procedural Mesh", "Procedural Mesh", "asset", "");
		AssetDatabase.CreateAsset(combineMat, filePath+".mat");
		AssetDatabase.CreateAsset(combineMesh, filePath);
		AssetDatabase.Refresh ();

		GameObject prefab = new GameObject ("Prefab");
		MeshFilter prefabMeshFilter = prefab.AddComponent<MeshFilter> ();
		MeshRenderer prefabMeshRenderer = prefab.AddComponent<MeshRenderer> ();
		prefabMeshFilter.mesh = combineMesh;
		prefabMeshRenderer.castShadows = false;
		prefabMeshRenderer.receiveShadows = false;
		prefabMeshRenderer.material = combineMat;

		PrefabUtility.CreatePrefab (filePath+".prefab", prefab);
		//AssetDatabase.SaveAssets();

		//DestroyImmediate (prefab);
#endif
	}
}