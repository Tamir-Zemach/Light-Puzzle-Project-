using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class MeshCombinerEditor : MonoBehaviour
{
    [MenuItem("Tools/Combine Meshes and Save Asset")]
    private static void CombineAndSave()
    {
        if (Selection.activeTransform == null)
        {
            Debug.LogWarning("No GameObject selected. Please select a parent object.");
            return;
        }

        Transform parent = Selection.activeTransform;
        MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>();

        List<CombineInstance> combine = new List<CombineInstance>();

        foreach (var mf in meshFilters)
        {
            if (mf.sharedMesh == null) continue;

            CombineInstance ci = new CombineInstance
            {
                mesh = mf.sharedMesh,
                transform = mf.transform.localToWorldMatrix
            };
            combine.Add(ci);
        }

        if (combine.Count == 0)
        {
            Debug.LogWarning("No meshes found to combine.");
            return;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        combinedMesh.CombineMeshes(combine.ToArray(), true, true);

        // Save the mesh
        string path = EditorUtility.SaveFilePanelInProject("Save Combined Mesh", "CombinedMesh", "asset", "Choose where to save the mesh asset.");
        if (!string.IsNullOrEmpty(path))
        {
            AssetDatabase.CreateAsset(combinedMesh, path);
            AssetDatabase.SaveAssets();
            Debug.Log($"Combined mesh saved at: {path}");
        }
    }
}
