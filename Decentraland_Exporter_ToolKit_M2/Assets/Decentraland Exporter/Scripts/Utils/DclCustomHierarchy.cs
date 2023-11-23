using System;
using UnityEngine;
using System.Collections.Generic;
using DCLExport;
using UnityEditor;
using Object = UnityEngine.Object;

[InitializeOnLoad]
public class DclCustomHierarchy
{
    static List<int> markedObjects;

    static DclCustomHierarchy()
    {
        // Init
        EditorApplication.update += UpdateCB;
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
    }

    static void UpdateCB()
    {
        // Check here
        GameObject[] go = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];

        markedObjects = new List<int>();
        foreach (GameObject g in go)
        {
            markedObjects.Add(g.GetInstanceID());
        }

    }

    static void HierarchyItemCB(int instanceID, Rect selectionRect)
    {
        // place the icoon to the right of the list:
        Rect r = new Rect(selectionRect);
        r.x = r.xMin - 30;
        r.width = 24;

        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        EDclNodeType nodeType = EDclNodeType._none;
        Texture2D tex = null;
        if (go)
        {
            var dclObject = go.GetComponent<DclObject>();
            if (dclObject)
            {
                nodeType = dclObject.dclNodeType;
                switch (dclObject.dclNodeType)
                {
                    case EDclNodeType.entity:
                        tex = DclEditorSkin.Entity;
                        break;
                    case EDclNodeType.ignore:
                        tex = DclEditorSkin.Ignore;
                        break;
                    case EDclNodeType.nft:
                        tex = DclEditorSkin.Nft;
                        break;
                    case EDclNodeType.box:
                        tex = DclEditorSkin.Cube;
                        break;
                    case EDclNodeType.sphere:
                        tex = DclEditorSkin.Sphere;
                        break;
                    case EDclNodeType.plane:
                        tex = DclEditorSkin.Quad;
                        break;
                    case EDclNodeType.cylinder:
                        tex = DclEditorSkin.Cylinder;
                        break;
                    case EDclNodeType.cone:
                        tex = DclEditorSkin.Cone;
                        break;
                    case EDclNodeType.circle:
                        tex = DclEditorSkin.Sphere;
                        break;
                    case EDclNodeType.gltf:
                        tex = DclEditorSkin.Mesh;
                        break;
                    case EDclNodeType.gltf_forced:
                        tex = DclEditorSkin.Mesh;
                        break;
                    case EDclNodeType.gltf_break:
                        tex = DclEditorSkin.Mesh;
                        break;
                    case EDclNodeType.ChildOfGLTF:
                        tex = DclEditorSkin.FollowUp;
                        break;
                }
            }
        }

        if (tex)
        {
            GUI.Label(r, new GUIContent(tex, GetTooltipForNodeTypeIcon(nodeType)));
        }
    }

    static bool IsChildOfGLTF(Transform t)
    {
        var parent = t.parent;
        if (!parent) return false;
        var dclObject = t.GetComponent<DclObject>();
        if (dclObject)
        {
            if (dclObject.dclNodeType == EDclNodeType.gltf || dclObject.dclNodeType == EDclNodeType.gltf_forced) return true;
            return false;
        }
        else
        {
            return IsChildOfGLTF(parent);
        }
    }

    static string GetTooltipForNodeTypeIcon(EDclNodeType nodeType)
    {
        switch (nodeType)
        {
            case EDclNodeType.entity:
                return "Empty Entity";
            case EDclNodeType.box:
                return "BoxShape";
            case EDclNodeType.sphere:
                return "SphereShape";
            case EDclNodeType.plane:
                return "PlaneShape";
            case EDclNodeType.cylinder:
                return "CylinderShape";
            case EDclNodeType.cone:
                return "ConeShape";
            case EDclNodeType.circle:
                return "CircleShape";
            case EDclNodeType.gltf:
                return "GLTFShape";
            case EDclNodeType.gltf_forced:
                return "GLTFShape";
            case EDclNodeType.gltf_break:
                return "GLTFShape";
            case EDclNodeType.ChildOfGLTF:
                return "will be contained in its parent's gltf file";
            case EDclNodeType.nft:
                return "Empty Entity";
            default:
                return null;
        }
    }
}
