using DCLExport;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Dcl Exporter ToolKit/Force GLTF")]
[ExecuteInEditMode]
public class Force_GLTF : MonoBehaviour
{
    private DclSceneMeta sceneMeta;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (!sceneMeta)
        {
            CheckAndGetDclSceneMetaObject();
        }
        this.GetComponent<DclObject>().dclNodeType = EDclNodeType.gltf_forced;
        BuildForced_GLTF();
        sceneMeta.RefreshStatistics();
        sceneMeta.getParcelSetVolumes();
    }

    public void BuildForced_GLTF()
    {
        if (!GetComponent<MeshRenderer>())
        {
            gameObject.AddComponent<MeshRenderer>();
            gameObject.GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));

        }
        if (!GetComponent<MeshFilter>())
        {
            gameObject.AddComponent<MeshFilter>();
            gameObject.GetComponent<MeshFilter>().sharedMesh = DclPrimitiveMeshBuilder.BuildCube(1);
            gameObject.GetComponent<MeshFilter>().sharedMesh.name = "Forced_Cube";
        }
    }

    public void DisableForcedGLTF(bool destroyMesh)
    {
#if UNITY_EDITOR
        if (GetComponent<MeshRenderer>() && destroyMesh)
        {
            DestroyImmediate(GetComponent<MeshRenderer>());
        }
        if (GetComponent<MeshFilter>() && destroyMesh)
        {
            DestroyImmediate(GetComponent<MeshFilter>());
        }
        DestroyImmediate(this);
        
        //Refresh
        sceneMeta.RefreshStatistics();
        sceneMeta.getParcelSetVolumes();
#endif
    }

    [CustomEditor(typeof(Force_GLTF))]
    public class CustomForceGltfEditor : Editor
    {
        Force_GLTF t;
        void OnEnable()
        {
            t = (Force_GLTF)target;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Destroy Component"))
            {
                t.DisableForcedGLTF(false);
            }
            if (GUILayout.Button("Destroy Mesh & Component"))
            {
                t.DisableForcedGLTF(true);
            }
        }
    }
    private void CheckAndGetDclSceneMetaObject()
    {
        var rootGameObjects = new List<GameObject>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            rootGameObjects.AddRange(SceneManager.GetSceneAt(i).GetRootGameObjects());
        }

        foreach (var go in rootGameObjects)
        {
            if (go.name == ".dclManager")
            {
                sceneMeta = go.GetComponent<DclSceneMeta>();
                if (!sceneMeta)
                {
                    sceneMeta = go.AddComponent<DclSceneMeta>();
                    GameObject spawnPoint = new GameObject("spawnPoint0DCL");
                    spawnPoint.transform.position = new Vector3(0, 1, 0);
                    spawnPoint.transform.localScale = Vector3.zero;
                    spawnPoint.transform.parent = sceneMeta.transform;
                    GameObject cam = new GameObject("Camera");
                    cam.transform.parent = sceneMeta.transform;
                    cam.AddComponent<Camera>();
                    sceneMeta.spawnPoints.Add(spawnPoint.transform);
                    EditorUtility.SetDirty(sceneMeta);
                    EditorSceneManager.MarkSceneDirty(go.scene);
                }
                return;
            }
        }

        //Did not find .dclManager, Create one.
        var o = new GameObject(".dclManager");
        sceneMeta = o.AddComponent<DclSceneMeta>();
        GameObject spwPoint = new GameObject("spawnPoint0DCL");
        spwPoint.transform.position = new Vector3(0, 1, 0);
        spwPoint.transform.localScale = Vector3.zero;
        spwPoint.transform.parent = sceneMeta.transform;
        GameObject camera = new GameObject("Camera");
        camera.transform.parent = sceneMeta.transform;
        camera.AddComponent<Camera>();
        sceneMeta.spawnPoints.Add(spwPoint.transform);
        EditorUtility.SetDirty(sceneMeta);
        EditorSceneManager.MarkSceneDirty(o.scene);
    }
}
