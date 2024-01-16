using DCLExport;
using UnityEngine;

[ExecuteInEditMode]
public class Force_GLTF : MonoBehaviour
{
    private DclSceneMeta sceneMeta;
    // Start is called before the first frame update
    private void OnEnable()
    {
        sceneMeta = FindFirstObjectByType<DclSceneMeta>();
        this.GetComponent<DclObject>().dclNodeType = EDclNodeType.gltf_forced;
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
            gameObject.GetComponent<MeshFilter>().sharedMesh = new Mesh();
            gameObject.GetComponent<MeshFilter>().sharedMesh.name = "Forced_Mesh";
        }
    }
}
