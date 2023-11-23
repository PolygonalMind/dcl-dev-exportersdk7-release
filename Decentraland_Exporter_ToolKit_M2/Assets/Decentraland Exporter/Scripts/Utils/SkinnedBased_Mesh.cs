using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DCLExport;

[ExecuteInEditMode]
public class SkinnedBased_Mesh : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (this.GetComponent<DclObject>())
        {
            this.GetComponent<DclObject>().dclNodeType = EDclNodeType.gltf;
            BuildMesh();
        }
    }
    private void OnDestroy()
    {
#if UNITY_EDITOR
        if (this.GetComponent<MeshRenderer>())
        {
            Component.DestroyImmediate(this.gameObject.GetComponent<MeshRenderer>());
        }
        if (this.GetComponent<MeshFilter>())
        {
            Component.DestroyImmediate(this.gameObject.GetComponent<MeshFilter>());
        }
        if (this.GetComponent<DclObject>())
        {
            this.GetComponent<DclObject>().dclNodeType = EDclNodeType.entity;
        }
#endif
    }

    public void BuildMesh()
    {
        if (!this.GetComponent<MeshRenderer>())
        {
            this.gameObject.AddComponent<MeshRenderer>();
            this.gameObject.GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard")); //sharedMaterial.shader.name.Contains("Universal Render Pipeline/PolyShader")
            this.gameObject.GetComponent<MeshRenderer>().sharedMaterial.shader = Shader.Find("Universal Render Pipeline/PolyShader");

        }
        if (!this.GetComponent<MeshFilter>())
        {
            this.gameObject.AddComponent<MeshFilter>();
            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[4]
            {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 0)
            };
            mesh.vertices = vertices;

            int[] tris = new int[6]
            {
            // lower left triangle
            0, 2, 1,
            // upper right triangle
            2, 3, 1
            };
            mesh.triangles = tris;

            Vector3[] normals = new Vector3[4]
            {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
            };
            mesh.normals = normals;

            Vector2[] uv = new Vector2[4]
            {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
            };
            mesh.uv = uv;

            this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}