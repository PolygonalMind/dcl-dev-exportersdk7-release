using System;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using Object = UnityEngine.Object;
using static DCLExport.DclExporter;

namespace DCLExport
{
    public static class SceneTraverserDcl
    {
        static ResourceRecorder _resourceRecorder;
        private static DclSceneMeta _sceneMeta;
        public static List<GameObject> randomSpawnerEntities = new List<GameObject>();

        public static ResourceRecorder TraverseDclScene(StringBuilder exportStr, StringBuilder additionalStr, SceneStatistics statistics,
            SceneWarningRecorder warningRecorder, ExportFormat mFormat)
        {
            var rootGameObjects = new List<GameObject>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var roots = SceneManager.GetSceneAt(i).GetRootGameObjects();
                rootGameObjects.AddRange(roots);
            }

            _sceneMeta = Object.FindObjectOfType<DclSceneMeta>();
            _resourceRecorder = new ResourceRecorder();

            //====== Start Traversing ======

            foreach (var rootGO in rootGameObjects)
            {
                try
                {
                    RecursivelyTravTransDecentraland(rootGO.transform, exportStr, additionalStr, _resourceRecorder, 4, statistics,
                        warningRecorder, mFormat);
                }
                catch (Exception e)
                {
                    Debug.LogError("Check the gameobject: " + rootGO.name + " for errors or empty fields.\n" + e);
                    return null;
                }
            }

            if (statistics != null)
            {
                statistics.textureCount = _resourceRecorder.primitiveTexturesToExport.Count +
                                          _resourceRecorder.gltfTextures.Count;
            }

            return _resourceRecorder;
        }
        public static void RecursivelyTravTransDecentraland(Transform tra, StringBuilder exportStr, StringBuilder additionalStr,
            ResourceRecorder resourceRecorder, int indentLevel, SceneStatistics statistics,
            SceneWarningRecorder warningRecorder, ExportFormat mFormat)
        {
            if (!tra.gameObject.activeInHierarchy) return;
            if (tra.gameObject.GetComponent<DclSceneMeta>()) return; //skip .dclManager

            var dclObject = tra.GetComponent<DclObject>() ?? tra.gameObject.AddComponent<DclObject>();

            dclObject.orderComps();     //Ordena los componentes colocando DclObject el primero por debajo de transform

            dclObject.dclNodeType = EDclNodeType.entity;


            if (dclObject.ignoreObject == true) { tra.gameObject.GetComponent<DclObject>().dclNodeType = EDclNodeType.ignore; return; } //skip ignored objects

            if (dclObject.dclName != SceneTraverserUtils.BuildIdentityName(dclObject.gameObject))
            {
                dclObject.dclName = SceneTraverserUtils.BuildIdentityName(dclObject.gameObject);    //Automate name generation when refreshing and avoid any repeated name failures.
            }

            var entityName = SceneTraverserUtils.GetIdentityName(tra.gameObject);
            if (statistics != null)
            {
                statistics.entityCount += 1;
            }

            if (exportStr != null)
            {
                SceneTraverserUtils.ImportModuleECS(resourceRecorder, "engine");
                
                //Add New Entity
                exportStr.AppendFormat(AddEntity, entityName, tra.name);
            }

            if (tra.GetComponent<TriggerDcl>() || tra.GetComponent<ModifierAreaDcl>())
            {
                tra.gameObject.GetComponent<DclObject>().dclNodeType = EDclNodeType.area;
            }
            
            Break_Child break_Child = tra.gameObject.GetComponent<Break_Child>();
            if (break_Child)
            {
                if (tra.parent == null)
                {
                    Object.DestroyImmediate(tra.gameObject.GetComponent<Break_Child>());
                    tra.gameObject.GetComponent<DclObject>().dclNodeType = EDclNodeType.gltf;
                }
                else
                    tra.gameObject.GetComponent<DclObject>().dclNodeType = EDclNodeType.gltf_break;
            }
            // skinned based mesh
            if (tra.gameObject.GetComponent<SkinnedMeshRenderer>() && !tra.gameObject.GetComponent<MeshFilter>())
            {
                tra.gameObject.GetComponent<DclObject>().dclNodeType = EDclNodeType.gltf;
                tra.gameObject.AddComponent<MeshFilter>();
                tra.gameObject.GetComponent<MeshFilter>().mesh = tra.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
            }
            // force gltf
            if (tra.gameObject.GetComponent<Force_GLTF>())
            {
                tra.gameObject.GetComponent<DclObject>().dclNodeType = EDclNodeType.gltf_forced;
                tra.gameObject.GetComponent<Force_GLTF>().BuildForced_GLTF();
            }
            
            ProcessShape(tra, entityName, exportStr, resourceRecorder, statistics, mFormat);

            if (exportStr != null )
            {
                var rotation = tra.localRotation;
                //GLTF rotation transform, reverse 180° along local y-axis to match Dcl transform.
                if (dclObject.dclNodeType == EDclNodeType.gltf || dclObject.dclNodeType == EDclNodeType.gltf_forced || dclObject.dclNodeType == EDclNodeType.gltf_break)
                {
                    rotation = Quaternion.AngleAxis(180, tra.up) * rotation;
                }

                SceneTraverserUtils.ImportModuleECS(resourceRecorder, "Transform");
                
                //Set Transform
                if (tra.parent)
                {
                    exportStr.AppendFormat(SetTransformParent, entityName, SceneTraverserUtils.Vector3ToJSONString(tra.localPosition), SceneTraverserUtils.QuaternionToJSONString(rotation), SceneTraverserUtils.Vector3ToJSONString(tra.localScale), SceneTraverserUtils.GetIdentityName(tra.parent.gameObject));
                }
                else
                {
                    exportStr.AppendFormat(SetTransform, entityName, SceneTraverserUtils.Vector3ToJSONString(tra.localPosition), SceneTraverserUtils.QuaternionToJSONString(rotation), SceneTraverserUtils.Vector3ToJSONString(tra.localScale));
                }
            }
            
            if (dclObject.dclNodeType != EDclNodeType.gltf && dclObject.dclNodeType != EDclNodeType.gltf_forced && dclObject.dclNodeType != EDclNodeType.gltf_break)
            {

                ProcessText(tra, entityName, exportStr, statistics, resourceRecorder);

                var streamVideo = tra.GetComponent<StreamingVideoDcl>();
                var streamImage = tra.GetComponent<StreamImageDcl>();
                if (streamVideo)
                {
                    ProcessVideo(streamVideo, entityName, exportStr, resourceRecorder);
                }
                else if (streamImage)
                {
                    ProcessImage(streamImage, entityName, exportStr, resourceRecorder);
                }
                else
                {
                    ProcessMaterial(tra, false, entityName, exportStr, resourceRecorder, statistics);
                }

                if (tra.GetComponent<MeshRenderer>() || tra.GetComponent<SkinnedMeshRenderer>())
                {
                    if (!resourceRecorder.primitivesToExport.Contains(tra.gameObject))
                    {
                        resourceRecorder.primitivesToExport.Add(tra.gameObject);
                    }
                    
                    var meshFilter = tra.GetComponent<MeshFilter>();
                    Renderer meshRenderer = tra.GetComponent<MeshRenderer>();
                    SkinnedMeshRenderer smr = null;
                    Mesh mesh = null;

                    if (!meshRenderer)
                    {
                        smr = tra.GetComponent<SkinnedMeshRenderer>();
                        if (smr)
                        {
                            mesh = smr.sharedMesh;
                        }
                    }
                    else if (meshFilter)
                    {
                        mesh = meshFilter.sharedMesh;
                    }

                    //Statistics
                    if (statistics != null)
                    {
                        if (mesh)
                        {
                            statistics.triangleCount += mesh.triangles.LongLength / 3;
                            statistics.bodyCount += 1;
                        }

                        var curHeight = meshRenderer ? meshRenderer.bounds.max.y : smr.bounds.max.y;
                        if (curHeight > statistics.maxHeight) statistics.maxHeight = curHeight;
                    }

                    //Warnings OutOfLand
                    if (_sceneMeta && warningRecorder != null)
                    {
                        var isOutOfLand = false;
                        var startParcel = SceneUtil.GetParcelCoordinates(meshRenderer ? meshRenderer.bounds.min : smr.bounds.min);
                        var endParcel = SceneUtil.GetParcelCoordinates(meshRenderer ? meshRenderer.bounds.max : smr.bounds.max);
                        for (int x = startParcel.x; x <= endParcel.x; x++)
                        {
                            for (int y = startParcel.y; y <= endParcel.y; y++)
                            {
                                if (!_sceneMeta.parcels.Exists(parcel => parcel == new ParcelCoordinates(x, y)))
                                {
                                    warningRecorder.OutOfLandWarnings.Add(
                                        new SceneWarningRecorder.OutOfLand(meshRenderer ? meshRenderer : smr));
                                    isOutOfLand = true;
                                    break;
                                }
                            }

                            if (isOutOfLand) break;
                        }
                        var n = _sceneMeta.parcels.Count;

                        var curHeight = meshRenderer ? meshRenderer.bounds.max.y : smr.bounds.max.y;
                        if (curHeight > LimitationConfigs.GetMaxHeight(n))
                        {
                            warningRecorder.OutOfHeightLandWarnings.Add(new SceneWarningRecorder.OutOfLand(meshRenderer ? meshRenderer : smr));
                        }

                        //Dont warning if text shader is not supported because we dont export a material or 3d model for text, just create Dcl Test Shapes
                        if (tra.GetComponent<TextMesh>() || tra.GetComponent<TextMeshPro>()) return;

                        try
                        {
                            var mat = meshRenderer ? meshRenderer : smr;
                            if (!mat.sharedMaterial.shader.name.Contains("Universal Render Pipeline/Lit"))
                            {
                                warningRecorder.UnsupportedShaderWarnings.Add(new SceneWarningRecorder.UnsupportedShader(mat.sharedMaterial));
                            }
                            if (tra.gameObject.name.Contains(":"))
                            {
                                warningRecorder.InvalidNamingWarnings.Add(new SceneWarningRecorder.InvalidNaming(tra.gameObject));
                            }
                        }
                        catch (NullReferenceException ex)
                        {
                            Debug.LogError("Material is not set in gameObject named: " + tra.gameObject.name + ex);
                        }
                    }
                }
                else if ((tra.GetComponent<ModifierAreaDcl>() || tra.GetComponent<TriggerDcl>()) && _sceneMeta)
                {
                    var isOutOfLand = false;
                    var startParcel = SceneUtil.GetParcelCoordinates(new Vector3(tra.position.x - tra.lossyScale.x / 2, tra.position.y - tra.lossyScale.y / 2, tra.position.z - tra.lossyScale.z / 2));
                    var endParcel = SceneUtil.GetParcelCoordinates(new Vector3(tra.position.x + tra.lossyScale.x / 2, tra.position.y + tra.lossyScale.y / 2, tra.position.z + tra.lossyScale.z / 2));
                    for (int x = startParcel.x; x <= endParcel.x; x++)
                    {
                        for (int y = startParcel.y; y <= endParcel.y; y++)
                        {
                            if (!_sceneMeta.parcels.Exists(parcel => parcel == new ParcelCoordinates(x, y)))
                            {
                                warningRecorder.AreaOutOfLandWarnings.Add(
                                    new SceneWarningRecorder.OutOfLandArea(tra.gameObject));
                                isOutOfLand = true;
                                break;
                            }
                        }
                        if (isOutOfLand)
                            break;
                    }
                    var curHeight = tra.position.y + tra.lossyScale.y / 2;
                    if (curHeight > LimitationConfigs.GetMaxHeight(_sceneMeta.parcels.Count))
                    {
                        warningRecorder.AreaOutOfHeightLandWarnings.Add(new SceneWarningRecorder.OutOfLandArea(tra.gameObject));
                    }
                }

                foreach (Transform child in tra)
                {
                    try
                    {
                        RecursivelyTravTransDecentraland(child, exportStr, additionalStr, resourceRecorder, indentLevel + 1, statistics,
                            warningRecorder, mFormat);
                        
                    } catch(Exception e)
                    {
                        Debug.LogError("Check the gameobject: " + child.name + " for errors.\n" + e);
                    }
                }
            }
            else
            {
                if (statistics != null) statistics.gltfMaterials.Clear();
                statistics.gltfCount += 1;
                try
                {
                    RecursivelyTraverseIntoDclGLTF(tra, 0, statistics, warningRecorder, exportStr, additionalStr, resourceRecorder, mFormat);
                }
                catch (Exception e)
                {
                    Debug.LogError("Check the gameobject: " + tra.name + " for errors.\n" + e);
                    return;
                }
                if (statistics != null)
                {
                    statistics.materialCount += statistics.gltfMaterials.Count;
                }
            }

            if (exportStr == null) return; //exportStr is null because the traverse is a refresh, not a export

            //Export Animations
            ProcessAnimation(tra, entityName, exportStr, resourceRecorder);
            
            //Export Audio
            ProcessAudio(tra, entityName, exportStr, resourceRecorder);

            //Export Triggers
            ProcessTrigger(tra, entityName, exportStr, resourceRecorder);
            
            //Export Areas
            ProcessAreas(tra, entityName, exportStr, resourceRecorder);

            //Export Input Event
            ProcessEvent(tra, entityName, exportStr, resourceRecorder);
        }
        public static ResourceRecorder TraverseDclSceneDependences()
        {
            var rootGameObjects = new List<GameObject>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var roots = SceneManager.GetSceneAt(i).GetRootGameObjects();
                rootGameObjects.AddRange(roots);
            }

            _sceneMeta = Object.FindObjectOfType<DclSceneMeta>();
            _resourceRecorder = new ResourceRecorder();

            //====== Start Traversing ======

            foreach (var rootGO in rootGameObjects)
            {
                RecursivelyTravTransDecentralandDependences(rootGO.transform, _resourceRecorder);
            }

            return _resourceRecorder;
        }
        public static void RecursivelyTravTransDecentralandDependences(Transform tra, ResourceRecorder resourceRecorder)
        {
            if (!tra.gameObject.activeInHierarchy) return;
            if (tra.gameObject.GetComponent<DclSceneMeta>()) return; //skip .dclManager

            foreach (Transform child in tra)
            {
                RecursivelyTravTransDecentralandDependences(child, resourceRecorder);
            }
        }
        public static void RecursivelyTraverseIntoDclGLTF(Transform tra, int layerUnderGLTFRoot,
            SceneStatistics statistics, SceneWarningRecorder warningRecorder, StringBuilder exportStr, StringBuilder additionalStr,
            ResourceRecorder resourceRecorder, ExportFormat mFormat)
        {
            if (!tra.gameObject.activeInHierarchy) return;
            
            if (tra.gameObject.GetComponent<DclSceneMeta>()) return; //skip .dclManager
            
            var dclObject = tra.GetComponent<DclObject>() ?? tra.gameObject.AddComponent<DclObject>();
            
            if (dclObject.ignoreObject) { dclObject.dclNodeType = EDclNodeType.ignore; return; } //skip ignore
            
            if (layerUnderGLTFRoot > 0 && dclObject.dclNodeType != EDclNodeType.gltf_break) dclObject.dclNodeType = EDclNodeType.ChildOfGLTF;
            
            if(dclObject.dclNodeType == EDclNodeType.gltf_break && !dclObject.gameObject.GetComponent<Break_Child>()) dclObject.dclNodeType = EDclNodeType.ChildOfGLTF;
            
            if (tra.GetComponent<MeshRenderer>() || tra.GetComponent<SkinnedMeshRenderer>())
            {
                var meshFilter = tra.GetComponent<MeshFilter>();
                Renderer meshRenderer = tra.GetComponent<MeshRenderer>();
                SkinnedMeshRenderer smr = tra.GetComponent<SkinnedMeshRenderer>();
                Mesh mesh = null;
                
                if (meshFilter)
                {
                    mesh = meshFilter.sharedMesh;
                }
                else if (!meshRenderer && smr)
                {
                    mesh = smr.sharedMesh;
                }

                //Statistics
                if (statistics != null)
                {
                    if (mesh)
                    {
                        statistics.triangleCount += mesh.triangles.LongLength / 3;
                        statistics.bodyCount += 1;
                    }
                    
                    var curHeight = meshRenderer ? meshRenderer.bounds.max.y : smr.bounds.max.y;
                    if (curHeight > statistics.maxHeight) statistics.maxHeight = curHeight;
                }

                //Warning OutOfLand
                if (_sceneMeta && warningRecorder != null)
                {
                    var isOutOfLand = false;
                    var startParcel = SceneUtil.GetParcelCoordinates(meshRenderer ? meshRenderer.bounds.min : smr.bounds.min);
                    var endParcel = SceneUtil.GetParcelCoordinates(meshRenderer ? meshRenderer.bounds.max : smr.bounds.max);
                    for (int x = startParcel.x; x <= endParcel.x; x++)
                    {
                        for (int y = startParcel.y; y <= endParcel.y; y++)
                        {
                            if (!_sceneMeta.parcels.Exists(parcel => parcel == new ParcelCoordinates(x, y)))
                            {
                                //Debug.Log(startParcel + " " + endParcel);
                                warningRecorder.OutOfLandWarnings.Add(
                                    new SceneWarningRecorder.OutOfLand(meshRenderer ? meshRenderer : smr));
                                isOutOfLand = true;
                                break;
                            }
                        }
                        if (isOutOfLand) break;
                    }
                        
                    var n = _sceneMeta.parcels.Count;
                    var curHeight = meshRenderer ? meshRenderer.bounds.max.y : smr.bounds.max.y;
                    if (curHeight > LimitationConfigs.GetMaxHeight(n))
                    {
                        warningRecorder.OutOfHeightLandWarnings.Add(new SceneWarningRecorder.OutOfLand(meshRenderer ? meshRenderer : smr));
                    }
                    try
                    {
                        var mat = meshRenderer ? meshRenderer : smr;
                        if (!mat.sharedMaterial.shader.name.Contains("Universal Render Pipeline/Lit"))
                        {
                            warningRecorder.UnsupportedShaderWarnings.Add(new SceneWarningRecorder.UnsupportedShader(mat.sharedMaterial));
                        }
                    }
                    catch (NullReferenceException ex)
                    {
                        Debug.LogError("Material is not set in gameObject named: " + tra.gameObject.name + ex);
                    }
                }
            }

            ProcessMaterial(tra, true, null, null, resourceRecorder, statistics);

            foreach (Transform child in tra)
            {
                if (!child.gameObject.GetComponent<Break_Child>())
                {
                    try
                    {
                        RecursivelyTraverseIntoDclGLTF(child, layerUnderGLTFRoot + 1, statistics, warningRecorder, exportStr, additionalStr, resourceRecorder, mFormat);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Check the gameobject: " + child.name + " for errors.\n" + e);
                        return;
                    }
                }
                else
                {
                    try
                    {
                        RecursivelyTravTransDecentraland(child, exportStr, additionalStr, resourceRecorder, layerUnderGLTFRoot + 1, statistics, warningRecorder, mFormat);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("Check the gameobject: " + child.name + " for errors.\n" + e);
                        return;
                    }
                }
            }
            //Warning gameObject name
            if (_sceneMeta && warningRecorder != null)
            {
                if (tra.gameObject.name.Contains(":"))
                {
                    warningRecorder.InvalidNamingWarnings.Add(new SceneWarningRecorder.InvalidNaming(tra.gameObject));
                }
            }
        }

#region Functions

        public static void ProcessText(Transform tra, string entityName, StringBuilder exportStr,
            SceneStatistics statistics, ResourceRecorder resourceRecorder)
        {
            if (!((tra.GetComponent<TextMesh>() || tra.GetComponent<TextMeshPro>()) && tra.GetComponent<MeshRenderer>())) return;
            
            if (tra.gameObject.GetComponent<DclObject>())
                tra.gameObject.GetComponent<DclObject>().dclNodeType = EDclNodeType.text;

            if (exportStr != null && tra.GetComponent<TextMesh>())
            {
                SceneTraverserUtils.ImportModuleECS(resourceRecorder, "TextShape, Font, TextAlignMode");
                //Create Text Shape
                exportStr.AppendFormat(NewTextShape, entityName);

                //Set Text Shape Value
                var tm = tra.GetComponent<TextMesh>();
                var str = System.Text.RegularExpressions.Regex.Escape(tm.text);
                exportStr.AppendFormat(TextShapeValue, str);

                //Set TextShape Color
                SceneTraverserUtils.ImportModuleMath(resourceRecorder, "Color4");
                exportStr.AppendFormat(TextShapeColor, SceneTraverserUtils.ToJsColor4Ctor(tm.color));

                //Set Size
                var rdrr = tra.GetComponent<MeshRenderer>();
                if (rdrr)
                {
                    float width = Math.Min(32, rdrr.bounds.size.x * 2 / tra.lossyScale.x); //rdrr.bounds.extents.x*2;
                    float height = Math.Min(32, rdrr.bounds.size.y * 2 / tra.lossyScale.y); //rdrr.bounds.extents.y * 2;

                    exportStr.AppendFormat(TextShapeWidth, SceneTraverserUtils.FloatToString(width));
                    exportStr.AppendFormat(TextShapeHeight, SceneTraverserUtils.FloatToString(height));
                }

                //Set Font
                exportStr.AppendFormat(TextShapeFontSize, tm.fontSize);

                //Set Alignment
                switch (tm.anchor)
                {
                    case TextAnchor.UpperLeft:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_TOP_LEFT");
                        break;
                    case TextAnchor.UpperCenter:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_TOP_CENTER");
                        break;
                    case TextAnchor.UpperRight:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_TOP_RIGHT");
                        break;
                    case TextAnchor.MiddleLeft:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_MIDDLE_LEFT");
                        break;
                    case TextAnchor.MiddleCenter:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_MIDDLE_CENTER");
                        break;
                    case TextAnchor.MiddleRight:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_MIDDLE_RIGHT");
                        break;
                    case TextAnchor.LowerLeft:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_BOTTOM_LEFT");
                        break;
                    case TextAnchor.LowerCenter:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_BOTTOM_CENTER");
                        break;
                    case TextAnchor.LowerRight:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_BOTTOM_RIGHT");
                        break;
                }
                //Close TextShape
                exportStr.AppendFormat(EndTextShape);
            }
            else if (exportStr != null && tra.GetComponent<TextMeshPro>())
            {
                //Create Text Shape
                exportStr.AppendFormat(NewTextShape, entityName);

                //Set TextShape text value
                var tm = tra.GetComponent<TextMeshPro>();
                var str = System.Text.RegularExpressions.Regex.Escape(tm.text);
                exportStr.AppendFormat(TextShapeValue, str);

                //Set TextShape Color
                SceneTraverserUtils.ImportModuleMath(resourceRecorder, "Color4");
                exportStr.AppendFormat(TextShapeColor, SceneTraverserUtils.ToJsColor4Ctor(tm.color));
                
                //Set Size
                var rdrr = tra.GetComponent<MeshRenderer>();
                if (rdrr)
                {
                    var width = tra.GetComponent<RectTransform>().sizeDelta.x - tm.margin.z; //rdrr.bounds.extents.x*2;
                    var height = tra.GetComponent<RectTransform>().sizeDelta.y - tm.margin.w; //rdrr.bounds.extents.y * 2;

                    exportStr.AppendFormat(TextShapeWidth, SceneTraverserUtils.FloatToString(width));
                    exportStr.AppendFormat(TextShapeHeight, SceneTraverserUtils.FloatToString(height));
                }
                
                //Set Font
                exportStr.AppendFormat(TextShapeFont, "F_SANS_SERIF"); //Default Dcl font
                exportStr.AppendFormat(TextShapeFontSize, tm.fontSize.ToString().Replace(",","."));

                //Set LineSpacing & Wrapping mode
                exportStr.AppendFormat(TextShapeSpacing, SceneTraverserUtils.FloatToString(tm.lineSpacing));
                exportStr.AppendFormat(TextShapeWrapping, tm.enableWordWrapping.ToString().ToLower());

                //Set Alignment
                switch (tm.alignment)
                {
                    case TextAlignmentOptions.BottomLeft:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_BOTTOM_LEFT");
                        break;
                    case TextAlignmentOptions.Bottom:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_BOTTOM_CENTER");
                        break;
                    case TextAlignmentOptions.BottomRight:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_BOTTOM_RIGHT");
                        break;
                    case TextAlignmentOptions.Left:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_MIDDLE_LEFT");
                        break;
                    case TextAlignmentOptions.Center:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_MIDDLE_CENTER");
                        break;
                    case TextAlignmentOptions.Right:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_MIDDLE_RIGHT");
                        break;
                    case TextAlignmentOptions.TopLeft:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_TOP_LEFT");
                        break;
                    case TextAlignmentOptions.Top:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_TOP_CENTER");
                        break;
                    case TextAlignmentOptions.TopRight:
                        exportStr.AppendFormat(TextShapeAlign, "TAM_TOP_RIGHT");
                        break;
                }

                //Set Outline 
                if (tm.fontSharedMaterial.IsKeywordEnabled("OUTLINE_ON"))
                {
                    SceneTraverserUtils.ImportModuleMath(resourceRecorder, "Color3");
                    
                    exportStr.AppendFormat(TextShapeOutlineWidth, SceneTraverserUtils.FloatToString(tm.outlineWidth));
                    exportStr.AppendFormat(TextShapeOutlineColor, SceneTraverserUtils.ToJsColor3Ctor(tm.outlineColor));
                }

                //Set Padding
                if(tm.margin != Vector4.zero)
                {
                    exportStr.AppendFormat(TextShapePadding, tm.margin.x, tm.margin.y, tm.margin.z, tm.margin.w);
                }

                //Close TextShape
                exportStr.AppendFormat(EndTextShape);
            }

            if (statistics != null)
            {
                statistics.triangleCount += 4;
                statistics.bodyCount += 2;
            }
        }

        public static void ProcessShape(Transform tra, string entityName, StringBuilder exportStr,
            ResourceRecorder resourceRecorder, SceneStatistics statistics, ExportFormat mFormat)
        {
            glbPlaceholder glbPlaceholder = tra.gameObject.GetComponent<glbPlaceholder>();
            if (glbPlaceholder && exportStr != null)
            {
                exportStr.AppendFormat(NewGLTFshape, entityName, glbPlaceholder.path);
                exportStr.Append(EndGLTFshape);
                return;
            }


            string formato = "gltf";
            switch (mFormat)
            {
                case ExportFormat.GLTFExternalTextures:
                    formato = "gltf";
                    break;
                case ExportFormat.GLBExternalTextures:
                    formato = "glb";
                    break;
                case ExportFormat.GLBCompressedTextures:
                    formato = "glb";
                    break;
            }
            var meshFilter = tra.GetComponent<MeshFilter>();
            
            if (!(meshFilter && (tra.GetComponent<MeshRenderer>() || tra.GetComponent<SkinnedMeshRenderer>())))
                return;
            

            if (tra.GetComponent<TextMeshPro>())
                return;
            
            var dclObject = tra.GetComponent<DclObject>();
            string shapeName = null;
            
            if (dclObject)
            {
                switch (dclObject.dclPrimitiveType)
                {
                    case DclPrimitiveType.box:
                        dclObject.dclNodeType = EDclNodeType.box;
                        shapeName = "Box";
                        statistics.primitiveCount += 1;
                    break;
                    case DclPrimitiveType.sphere:
                        dclObject.dclNodeType = EDclNodeType.sphere;
                        shapeName = "Sphere";
                        statistics.primitiveCount += 1;
                    break;
                    case DclPrimitiveType.plane:
                        dclObject.dclNodeType = EDclNodeType.plane;
                        shapeName = "Plane";
                        statistics.primitiveCount += 1;
                    break;
                    case DclPrimitiveType.cylinder:
                        dclObject.dclNodeType = EDclNodeType.cylinder;
                        shapeName = "Cylinder";
                        statistics.primitiveCount += 1;
                    break;
                    default:
                        shapeName = null;
                    break;
                }
                if (meshFilter.sharedMesh.name != "DCL " + shapeName)
                {
                    dclObject.dclPrimitiveType = DclPrimitiveType.other;
                    dclObject.dclNodeType = EDclNodeType.gltf;
                    shapeName = null;
                }
            }

            if (shapeName != null)
            {
                if (exportStr == null) return;
                SceneTraverserUtils.ImportModuleECS(resourceRecorder, "MeshRenderer, MeshCollider");
                
                //Primitive
                exportStr.AppendFormat(SetShape, shapeName, entityName);
                    
                if (dclObject && dclObject.withCollision)
                    exportStr.AppendFormat(SetCollider, shapeName, entityName);
            }
            else
            {
                //gltf - root
                if (dclObject.dclNodeType != EDclNodeType.gltf_forced && dclObject.dclNodeType != EDclNodeType.gltf_break && dclObject.dclNodeType != EDclNodeType.ChildOfGLTF)
                    dclObject.dclNodeType = EDclNodeType.gltf;

                if (exportStr == null) return;
                SceneTraverserUtils.ImportModuleECS(resourceRecorder, "GltfContainer");

                bool bIsOvewritePrefab = true;
                PrefabUtility.GetCorrespondingObjectFromSource(tra.gameObject);
                //It's Prefab & MeshRenderer
                if (PrefabUtility.GetPrefabInstanceHandle(tra.gameObject) != null && tra.gameObject.GetComponent<MeshFilter>() != null && tra.gameObject.GetComponent<MeshFilter>().sharedMesh)
                {
                    bIsOvewritePrefab = false;
                    if (tra.gameObject.GetComponent<ovewriteMesh_script>() != null && tra.gameObject.GetComponent<ovewriteMesh_script>().exportToSingleGTLF)
                    {
                        bIsOvewritePrefab = true;
                    }
                    if (PrefabUtility.GetAddedGameObjects(tra.gameObject).ToArray().Length > 0 && tra.gameObject.GetComponent<ovewriteMesh_script>() == null)
                    {
                        foreach (var gameObject in PrefabUtility.GetAddedGameObjects(tra.gameObject))
                        {
                            if (gameObject.instanceGameObject.GetComponent<MeshFilter>() != null)
                            {
                                bIsOvewritePrefab = true;
                            }
                        }
                    }

                }

                if (bIsOvewritePrefab)
                {
                        string gltfPath = string.Format("unity_assets/{0}." + formato, SceneTraverserUtils.GetIdentityName(tra.gameObject)); //gltf
                        if (resourceRecorder.blateLoadGTLF && (tra.gameObject.GetComponent<lateLoadMesh_script>() != null && tra.gameObject.GetComponent<lateLoadMesh_script>().bLateLoadMesh))
                        {
                            resourceRecorder.lateGTLFlines += string.Format(NewGLTFshape, entityName, gltfPath);

                            //Put here collision options if needed

                            resourceRecorder.lateGTLFlines += EndGLTFshape;
                        }
                        else
                        {
                            exportStr.AppendFormat(NewGLTFshape, entityName, gltfPath);

                            //Put here collision options if needed

                            exportStr.Append(EndGLTFshape);
                        }

                    //export as a glTF model
                    if (resourceRecorder != null)
                    {
                        resourceRecorder.meshesToExport.Add(tra.gameObject);
                    }
                }
                else
                {
                    string name = tra.gameObject.GetComponent<MeshFilter>().sharedMesh.name;
                    if (tra.gameObject.GetComponent<ovewriteMesh_script>() && tra.gameObject.GetComponent<ovewriteMesh_script>().export_custom_GTLF_name != null && tra.gameObject.GetComponent<ovewriteMesh_script>().export_custom_GTLF_name != "")
                    {
                        name = tra.gameObject.GetComponent<ovewriteMesh_script>().export_custom_GTLF_name;
                    }
                    int index = resourceRecorder.exportedModels.IndexOf(name);
                    if (exportStr != null)
                    {
                        string fileModenName = SceneTraverserUtils.GetIdentityName(tra.gameObject);
                        if (index > -1)
                        {
                            fileModenName = resourceRecorder.exportedModelsFileName[index];
                        }
                        string gltfPath = string.Format("unity_assets/{0}." + formato, fileModenName); //gltf
                        if (resourceRecorder.blateLoadGTLF && (tra.gameObject.GetComponent<lateLoadMesh_script>() != null && tra.gameObject.GetComponent<lateLoadMesh_script>().bLateLoadMesh))
                        {
                            resourceRecorder.lateGTLFlines += string.Format(NewGLTFshape, entityName, gltfPath); //GLTF

                            //Put here collision options if needed

                            resourceRecorder.lateGTLFlines += EndGLTFshape;
                        }
                        else
                        {
                            exportStr.AppendFormat(NewGLTFshape, entityName, gltfPath);
                            exportStr.Append(EndGLTFshape);
                        }
                    }

                    //export as a glTF model
                    if (resourceRecorder != null && index == -1)
                    {
                        resourceRecorder.exportedModelsFileName.Add(SceneTraverserUtils.GetIdentityName(tra.gameObject));
                        resourceRecorder.exportedModels.Add(name);
                        resourceRecorder.meshesToExport.Add(tra.gameObject);
                    }
                }
            }
            //Create Billboard
            var billboard = tra.gameObject.GetComponent<BillboardDcl>();
            if (billboard && exportStr != null)
            {
                SceneTraverserUtils.ImportModuleECS(resourceRecorder, "Billboard, BillboardMode");
                
                string billboardMode = "";
                switch (billboard.billboardMode)
                {
                    case BillboardMode.everyAxis:
                        billboardMode = "BM_ALL";
                        break;
                    case BillboardMode.axisX:
                        billboardMode = "BM_X";
                        break;
                    case BillboardMode.axisY:
                        billboardMode = "BM_Y";
                        break;
                    case BillboardMode.axisZ:
                        billboardMode = "BM_Z";
                        break;
                    case BillboardMode.none:
                        billboardMode = "BM_NONE";
                        break;
                }
                
                exportStr.AppendFormat(NewBillboard, entityName, billboardMode);
            }
        }
        public static void ProcessAnimation(Transform tra, string entityName, StringBuilder exportStr, ResourceRecorder resourceRecorder)
        {
            var animationComp = tra.gameObject.GetComponent<Animation>();
            var animatorComp = tra.gameObject.GetComponent<Animator>();
            var dclAnimator = tra.gameObject.GetComponent<AnimatorDcl>();

            if (animationComp)
            {
                Debug.LogError("Animations can't be handled\nAnimation Component not supported\nAdd an AnimatorDcl or an Animator Component to the gameObject \"" + tra.gameObject.name + "\" to handle animation clips in Decentraland and correctly export glTFs");
                return;
            }

            if (animatorComp && dclAnimator)
            {
                Debug.LogError("You are trying to export a GameObject: " + tra.gameObject.name + " with more than one Animator\nChoose between Animator or DclAnimator Component to handle animation clips in Decentraland and correctly export glTFs");
                return;
            }

            //Animator component
            if (animatorComp)
            {
                SceneTraverserUtils.ImportModuleECS(resourceRecorder, "Animator");
                //Create Dcl Animator
                exportStr.AppendFormat(NewAnimator, entityName);
                //Add default clip
                exportStr.AppendFormat(AddClip, animatorComp.runtimeAnimatorController.animationClips[0].name, "true", animatorComp.runtimeAnimatorController.animationClips[0].isLooping.ToString().ToLower(), 1f);

                //Create clips
                for (var i = 1; i < animatorComp.runtimeAnimatorController.animationClips.Length; i++)
                {
                    exportStr.AppendFormat(AddClip, animatorComp.runtimeAnimatorController.animationClips[i].name, "false", animatorComp.runtimeAnimatorController.animationClips[i].isLooping.ToString().ToLower(), 1f);
                }
                //Close Dcl Animator component
                exportStr.Append(EndAnimator);
            }
            // DclAnimation component
            if (dclAnimator)
            {
                SceneTraverserUtils.ImportModuleECS(resourceRecorder, "Animator");
                //Create Dcl Animator
                exportStr.AppendFormat(NewAnimator, entityName);
                //Add default clip
                exportStr.AppendFormat(AddClip, dclAnimator.defaultAnimation.clip.name, dclAnimator.startPlaying.ToString().ToLower(), dclAnimator.defaultAnimation.loop.ToString().ToLower(), 1f);
                //Create clips
                foreach (DclAnimation anim in dclAnimator.animations)
                {
                    exportStr.AppendFormat(AddClip, anim.clip.name, "false", anim.loop.ToString().ToLower(), 1f);
                }
                //Close Dcl Animator component
                exportStr.Append(EndAnimator);
            }
        }
        public static void ProcessAudio(Transform tra, string entityName, StringBuilder exportStr, ResourceRecorder resourceRecorder)
        {
            var audioSource = tra.GetComponent<AudioSourceDcl>();
            if (audioSource && audioSource.defaultClip != null && exportStr != null)
            {
                SceneTraverserUtils.ImportModuleECS(resourceRecorder, "AudioSource");
                
                string audioClipRelPath = string.Format("\"{0}\"", SceneTraverserUtils.GetAudioClipRelativePath(audioSource.defaultClip));
                //Add default Clip to the export list
                resourceRecorder.audioClipsToExport.Add(audioSource.defaultClip);
                //Add clip list to the export list if its not the default
                foreach (AudioClip clip in audioSource.clipsToExport)
                {
                    if (clip != audioSource.defaultClip)
                        resourceRecorder.audioClipsToExport.Add(clip);
                }
                //Create Audio Source in Dcl code
                exportStr.AppendFormat(NewAudioSource, entityName, audioClipRelPath, audioSource.playOnAwake.ToString().ToLower(), audioSource.loop.ToString().ToLower(), SceneTraverserUtils.FloatToString(audioSource.volume), SceneTraverserUtils.FloatToString(audioSource.pitch));
            }
        }
        public static void ProcessVideo(StreamingVideoDcl streamVideo, string entityName, StringBuilder exportStr, ResourceRecorder resourceRecorder)
        {
            if (exportStr == null) return;

            SceneTraverserUtils.ImportModuleECS(resourceRecorder, "Material");
            SceneTraverserUtils.ImportModuleECS(resourceRecorder, "VideoPlayer");
            SceneTraverserUtils.ImportModuleECS(resourceRecorder, "pointerEventsSystem, InputAction");

            exportStr.AppendFormat(NewVideoPlayer, entityName, streamVideo.videoURL, streamVideo.startPlaying.ToString().ToLower());
            exportStr.AppendFormat(NewMaterial, entityName);
            exportStr.AppendFormat(NewVideoTexture, entityName);
            exportStr.Append(EndMaterial);
            string func = string.Format(StreamVideoPointer, entityName);
            exportStr.AppendFormat(NewDclPointerDown, entityName, streamVideo.clickDistance , "IA_POINTER", "Play/Pause", func);
        }
        public static void ProcessImage(StreamImageDcl streamImage, string entityName, StringBuilder exportStr, ResourceRecorder resourceRecorder)
        {
            if (exportStr == null) return;

            SceneTraverserUtils.ImportModuleECS(resourceRecorder, "Material");
            
            exportStr.AppendFormat(NewMaterial, entityName);
            exportStr.AppendFormat(SetMaterialTexture, streamImage.imageUrl);
            exportStr.Append(EndMaterial);
        }
        public static void ProcessMaterial(Transform tra, bool isOnOrUnderGLTF, string entityName, StringBuilder exportStr, ResourceRecorder resourceRecorder, SceneStatistics statistics)
        {

            var rdrr = tra.GetComponent<MeshRenderer>();
            var smr = tra.GetComponent<SkinnedMeshRenderer>();
            if ((rdrr && tra.GetComponent<MeshFilter>()) || (smr && smr.sharedMesh))
            {
                if (tra.GetComponent<TextMeshPro>())
                {
                    return;
                }
                List<Material> materialList;
                if (isOnOrUnderGLTF)
                {
                    materialList = rdrr ? rdrr.sharedMaterials.ToList() : smr.sharedMaterials.ToList();
                }
                else
                {
                    materialList = new List<Material>();
                    if ((rdrr && rdrr.sharedMaterial) || (smr && smr.sharedMaterial)) materialList.Add(rdrr ? rdrr.sharedMaterial : smr.sharedMaterial);
                }
                foreach (var material in materialList)
                {
                    if (material && material != PrimitiveHelper.GetDefaultMaterial())
                    {
                        var albedoTex = material.HasProperty("_BaseMap") ? material.GetTexture("_BaseMap") : null;
                            
                        if (exportStr != null)
                        {
                            SceneTraverserUtils.ImportModuleECS(resourceRecorder, "Material");
                            SceneTraverserUtils.ImportModuleECS(resourceRecorder, "MaterialTransparencyMode");


                            //New Material
                            exportStr.AppendFormat(NewMaterial, entityName);

                            if (exportStr != null && albedoTex)
                            {
                                exportStr.AppendFormat(SetMaterialTexture, SceneTraverserUtils.GetTextureRelativePath(albedoTex));
                            }

                            SceneTraverserUtils.ImportModuleMath(resourceRecorder, "Color4");
                            
                            exportStr.AppendFormat(SetMaterialAlbedoColor, SceneTraverserUtils.ToJsColor4Ctor(material.color));
                            exportStr.AppendFormat(SetMaterialMetallic, SceneTraverserUtils.FloatToString(material.GetFloat("_Metallic")));
                            exportStr.AppendFormat(SetMaterialRoughness, SceneTraverserUtils.FloatToString(1.0f - material.GetFloat("_Smoothness")));
                            
                            if (material.HasProperty("_Surface") && material.GetFloat("_Surface") != 0)
                            {
                                if (material.HasProperty("_AlphaClip") && material.GetFloat("_AlphaClip") == 1)
                                {
                                    exportStr.AppendFormat(SetMaterialAlphaTest);
                                    exportStr.AppendFormat(SetMaterialAlpha, SceneTraverserUtils.FloatToString(material.GetFloat("_Cutoff")));
                                }
                                else if(material.HasProperty("_Blend") && material.GetFloat("_Blend") != 0)
                                {
                                    exportStr.Append(SetMaterialAlphaBlend);
                                }
                                else
                                {
                                    exportStr.AppendFormat(SetMaterialAlphaTrans);
                                }
                            }
                            else
                            {
                                exportStr.Append(SetMaterialOpaque);
                            }

                        }
                        
                        var bumpTexture = material.HasProperty("_BumpMap") ? material.GetTexture("_BumpMap") : null;
                        if (exportStr != null && bumpTexture)
                        {
                            exportStr.AppendFormat(SetMaterialBumptexture, SceneTraverserUtils.GetTextureRelativePath(bumpTexture));
                        }
                            
                        Texture emissiveTexture = null;
                        if (material.IsKeywordEnabled("_EMISSION") && exportStr != null)
                        {
                            SceneTraverserUtils.ImportModuleMath(resourceRecorder, "Color3");
                            exportStr.AppendFormat(SetMaterialEmissiveColor, SceneTraverserUtils.ToJsColor3Ctor(material.GetColor("_EmissionColor")));
                            
                            //Get intensity parameter from the emission color
                            var colour = material.GetColor("_EmissionColor");
                            exportStr.AppendFormat(SetMaterialEmissiveIntensity, SceneTraverserUtils.FloatToString(Mathf.Max(colour.r, colour.g, colour.b) * 1/2)); //Set intensity lower to match Emissive applyed in glTFs

                            emissiveTexture = material.HasProperty("_EmissionMap") ? material.GetTexture("_EmissionMap") : null;
                            if (emissiveTexture)
                            {
                                exportStr.AppendFormat(SetMaterialEmissiveTexture, SceneTraverserUtils.GetTextureRelativePath(emissiveTexture));
                            }
                        }

                        //End material lines
                        if (exportStr != null)
                            exportStr.Append(EndMaterial);

                        

                        var textureList = isOnOrUnderGLTF ? resourceRecorder.gltfTextures : resourceRecorder.primitiveTexturesToExport;
                        if (albedoTex && !textureList.Contains(albedoTex))
                        {
                            textureList.Add(albedoTex);
                        }

                        if (bumpTexture && !textureList.Contains(bumpTexture))
                        {
                            textureList.Add(bumpTexture);
                        }

                        if (emissiveTexture && !textureList.Contains(emissiveTexture))
                        {
                            textureList.Add(emissiveTexture);
                        }

                        if (!isOnOrUnderGLTF && statistics != null)
                        {
                            statistics.materialCount += 1;
                        }
                        
                    }
                }
            }
        }
        public static void ProcessTrigger(Transform tra, string entityName, StringBuilder exportStr, ResourceRecorder resourceRecorder)
        {
            TriggerDcl trigger = tra.GetComponent<TriggerDcl>();
            if (!trigger) return;
            
            if (!exportStr.ToString().Contains("import * as utils from \"@dcl-sdk/utils\""))
                exportStr.Prepend("import * as utils from \"@dcl-sdk/utils\"\n");

            StringBuilder action = new StringBuilder("");
            StringBuilder actionEnter = new StringBuilder("");
            StringBuilder actionExit = new StringBuilder("");
            
            if (trigger.actionListEnter.Count > 0)
            {
                    
                foreach (var dclAction in trigger.actionListEnter)
                {
                    string refName = SceneTraverserUtils.GetIdentityName(dclAction.refGO);
                    action.Clear();
                    switch (dclAction.actionType)
                    {
                        case TriggerActionType.TeleportInsideScene:
                            SceneTraverserUtils.ImportModuleRA(resourceRecorder, "movePlayerTo");

                            if (dclAction.fixedPosition)
                                action.AppendFormat(TpIn, SceneTraverserUtils.Vector3ToJSONString(dclAction.teleportPosition), SceneTraverserUtils.Vector3ToJSONString(dclAction.teleportLookAt));
                            else
                                action.AppendFormat(TpIn, SceneTraverserUtils.Vector3ToJSONString(dclAction.refGO.transform.position), SceneTraverserUtils.Vector3ToJSONString(dclAction.lookAt.transform.position));

                            break;
                        case TriggerActionType.PlayAnimation:
                            refName = SceneTraverserUtils.GetIdentityName(dclAction.refGO);
                            List<string> anims = new List<string>();
                            if (dclAction.refGO.GetComponent<AnimatorDcl>())
                            {
                                anims.Add(dclAction.refGO.GetComponent<AnimatorDcl>().defaultAnimation.clip.name);
                                foreach (var dclAnim in dclAction.refGO.GetComponent<AnimatorDcl>().animations)
                                {
                                    anims.Add(dclAnim.clip.name);
                                }
                            }
                            else if (dclAction.refGO.GetComponent<Animator>())
                            {
                                foreach (var dclAnim in dclAction.refGO.GetComponent<Animator>().runtimeAnimatorController.animationClips)
                                {
                                    anims.Add(dclAnim.name);
                                }
                            }

                            if (dclAction.modifyClipParams)
                            {
                                action.AppendFormat(GetClipAnim, refName, anims[dclAction.clipIndex]);
                                //action.AppendFormat(GetClipAnim, refName, dclAction.animClip.name);
                                action.Append("\t\t");
                                action.AppendFormat(SetAnimLoop, refName, dclAction.loop.ToString().ToLower());
                                action.Append("\t\t");
                                action.AppendFormat(SetAnimSpeed, refName, SceneTraverserUtils.FloatToString(dclAction.speed));
                                action.Append("\t\t");
                            }
                            action.AppendFormat(PlayAnimation, refName, anims[dclAction.clipIndex], dclAction.reset.ToString().ToLower());
                            //action.AppendFormat(PlayAnimation, refName, dclAction.animClip.name, dclAction.reset.ToString().ToLower());
                            break;
                        case TriggerActionType.StopAnimations:
                            action.AppendFormat(StopAnimations, refName);
                            break;
                        case TriggerActionType.PlayStopAudio:
                            action.AppendFormat(GetMutableAS, refName);
                            action.Append("\t\t");
                            action.AppendFormat(SetAudioClip, refName, SceneTraverserUtils.GetAudioClipRelativePath(dclAction.audioClip));
                            action.Append("\t\t");
                            action.AppendFormat(SetAudioLoop, refName, dclAction.loop.ToString().ToLower());
                            action.Append("\t\t");
                            action.AppendFormat(SetAudioVolume, refName, SceneTraverserUtils.FloatToString(dclAction.volume));
                            action.Append("\t\t");
                            action.AppendFormat(SetAudioPitch, refName, SceneTraverserUtils.FloatToString(dclAction.pitch));
                            action.Append("\t\t");
                            action.AppendFormat(PlayStopAudio, refName);
                            break;
                        case TriggerActionType.StopAudio:
                            action.AppendFormat(GetMutableAS, refName);
                            action.Append("\t\t");
                            action.AppendFormat(StopAudio, refName);
                            break;
                    }
                    actionEnter.AppendFormat("{0}\n\t\t", action.ToString());
                }
                //Remove last line break and indents from action string
                actionEnter = actionEnter.Remove(actionEnter.Length - 3, 3);

            }
            if (trigger.actionListExit.Count > 0)
            {
                foreach (var dclAction in trigger.actionListExit)
                {
                    string refName = SceneTraverserUtils.GetIdentityName(dclAction.refGO);
                    action.Clear();
                    switch (dclAction.actionType)
                    {
                        case TriggerActionType.TeleportInsideScene:
                            SceneTraverserUtils.ImportModuleRA(resourceRecorder, "movePlayerTo");

                            if (dclAction.fixedPosition)
                                action.AppendFormat(TpIn, SceneTraverserUtils.Vector3ToJSONString(dclAction.teleportPosition), SceneTraverserUtils.Vector3ToJSONString(dclAction.teleportLookAt));
                            else
                                action.AppendFormat(TpIn, SceneTraverserUtils.Vector3ToJSONString(dclAction.refGO.transform.position), SceneTraverserUtils.Vector3ToJSONString(dclAction.lookAt.transform.position));
                            break;
                        case TriggerActionType.PlayAnimation:
                            refName = SceneTraverserUtils.GetIdentityName(dclAction.refGO);
                            List<string> anims = new List<string>();
                            if (dclAction.refGO.GetComponent<AnimatorDcl>())
                            {
                                anims.Add(dclAction.refGO.GetComponent<AnimatorDcl>().defaultAnimation.clip.name);
                                foreach (var dclAnim in dclAction.refGO.GetComponent<AnimatorDcl>().animations)
                                {
                                    anims.Add(dclAnim.clip.name);
                                }
                            }
                            else if (dclAction.refGO.GetComponent<Animator>())
                            {
                                foreach (var dclAnim in dclAction.refGO.GetComponent<Animator>().runtimeAnimatorController.animationClips)
                                {
                                    anims.Add(dclAnim.name);
                                }
                            }

                            if (dclAction.modifyClipParams)
                            {
                                action.AppendFormat(GetClipAnim, refName, anims[dclAction.clipIndex]);
                                //action.AppendFormat(GetClipAnim, refName, dclAction.animClip.name);
                                action.Append("\t\t");
                                action.AppendFormat(SetAnimLoop, refName, dclAction.loop.ToString().ToLower());
                                action.Append("\t\t");
                                action.AppendFormat(SetAnimSpeed, refName, SceneTraverserUtils.FloatToString(dclAction.speed));
                                action.Append("\t\t");
                            }
                            action.AppendFormat(PlayAnimation, refName, anims[dclAction.clipIndex], dclAction.reset.ToString().ToLower());
                            //action.AppendFormat(PlayAnimation, refName, dclAction.animClip.name, dclAction.reset.ToString().ToLower());
                            break;
                        case TriggerActionType.StopAnimations:
                            action.AppendFormat(StopAnimations, refName);
                            break;
                        case TriggerActionType.PlayStopAudio:
                            action.AppendFormat(GetMutableAS, refName);
                            action.Append("\t\t");
                            action.AppendFormat(SetAudioClip, refName, SceneTraverserUtils.GetAudioClipRelativePath(dclAction.audioClip));
                            action.Append("\t\t");
                            action.AppendFormat(SetAudioLoop, refName, dclAction.loop.ToString().ToLower());
                            action.Append("\t\t");
                            action.AppendFormat(SetAudioVolume, refName, SceneTraverserUtils.FloatToString(dclAction.volume));
                            action.Append("\t\t");
                            action.AppendFormat(SetAudioPitch, refName, SceneTraverserUtils.FloatToString(dclAction.pitch));
                            action.Append("\t\t");
                            action.AppendFormat(PlayStopAudio, refName);
                            break;
                        case TriggerActionType.StopAudio:
                            action.AppendFormat(GetMutableAS, refName);
                            action.Append("\t\t");
                            action.AppendFormat(StopAudio, refName);
                            break;
                    }
                    actionExit.AppendFormat("{0}\n\t\t", action);
                }
                actionExit = actionExit.Remove(actionExit.Length - 3, 3);
            } 
            //Create Dcl Trigger
            exportStr.AppendFormat(NewTrigger, entityName, SceneTraverserUtils.Vector3ToJSONString(tra.lossyScale), actionEnter, actionExit);
        }
        public static void ProcessAreas(Transform tra, string entityName, StringBuilder exportStr, ResourceRecorder resourceRecorder)
        {
            ModifierAreaDcl modArea = tra.gameObject.GetComponent<ModifierAreaDcl>();
            if (!modArea) return;
            
            string modType = "";
            switch (modArea.modType)
            {
                case ModifierAreaDcl.modifierType.avatarMod:
                    SceneTraverserUtils.ImportModuleECS(resourceRecorder, "AvatarModifierArea, AvatarModifierType");
                    
                    switch (modArea.avatarModType)
                    {
                        case ModifierAreaDcl.avatarModification.hideAvatar:
                            modType = "HIDE_AVATARS";
                            break;
                        case ModifierAreaDcl.avatarModification.disablePassport:
                            modType = "DISABLE_PASSPORTS";
                            break;
                    }
                    exportStr.AppendFormat(NewModifierArea, entityName, SceneTraverserUtils.Vector3ToJSONString(tra.lossyScale), modType);
                break;
                case ModifierAreaDcl.modifierType.cameraMod:
                    SceneTraverserUtils.ImportModuleECS(resourceRecorder, "CameraModeArea, CameraType");
                    
                    switch (modArea.forceCam)
                    {
                        case ModifierAreaDcl.forceCamera.firstPerson:
                            modType = "FIRST_PERSON";
                            break;
                        case ModifierAreaDcl.forceCamera.thirdPerson:
                            modType = "THIRD_PERSON";
                            break;
                    }
                    exportStr.AppendFormat(NewCameraMod, entityName, SceneTraverserUtils.Vector3ToJSONString(tra.lossyScale), modType);
                break;
            }
        }
        public static void ProcessEvent(Transform tra, string entityName, StringBuilder exportStr, ResourceRecorder resourceRecorder)
        {
            var InputEventsDcl = tra.GetComponents<InputEventDcl>();
            bool pointerDown = false;
            bool pointerUp = false;
            if (InputEventsDcl.Length <= 0) return;

            SceneTraverserUtils.ImportModuleECS(resourceRecorder, "pointerEventsSystem, InputAction");
            
            foreach (var InputEventDcl in InputEventsDcl) 
            {
                if(InputEventDcl.actionList.Count <= 0) return;

                StringBuilder functionString = new StringBuilder();
                StringBuilder action = new StringBuilder();
                string actionButton = "";

                foreach (var dclAction in InputEventDcl.actionList)
                {
                        string refName = "";
                    
                    switch (dclAction.actionButton)
                    {
                        case ActionButtons.Click:
                            actionButton = "IA_POINTER";
                            break;
                        case ActionButtons.E:
                            actionButton = "IA_PRIMARY";
                            break;
                        case ActionButtons.F:
                            actionButton = "IA_SECONDARY";
                            break;
                        case ActionButtons.Num1:
                            actionButton = "IA_ACTION_3";
                            break;
                        case ActionButtons.Num2:
                            actionButton = "IA_ACTION_4";
                            break;
                        case ActionButtons.Num3:
                            actionButton = "IA_ACTION_5";
                            break;
                        case ActionButtons.Num4:
                            actionButton = "IA_ACTION_6";
                            break;
                    }

                    action.Clear();
                    switch (dclAction.actionType)
                    {
                        case ActionType.OpenLink:
                            SceneTraverserUtils.ImportModuleRA(resourceRecorder, "openExternalUrl");

                            action.AppendFormat(OpenUrl, dclAction.link);
                            break;
                        case ActionType.TeleportInsideScene:
                            SceneTraverserUtils.ImportModuleRA(resourceRecorder, "movePlayerTo");

                            if (dclAction.fixedPosition)
                                action.AppendFormat(TpIn, SceneTraverserUtils.Vector3ToJSONString(dclAction.teleportPosition), SceneTraverserUtils.Vector3ToJSONString(dclAction.teleportLookAt));
                            else
                                action.AppendFormat(TpIn, SceneTraverserUtils.Vector3ToJSONString(dclAction.refGO.transform.position), SceneTraverserUtils.Vector3ToJSONString(dclAction.lookAt.transform.position));

                            break;
                        case ActionType.TeleportToOtherScene:
                            SceneTraverserUtils.ImportModuleRA(resourceRecorder, "teleportTo");

                            action.AppendFormat(TpOut, SceneTraverserUtils.Vector2ToJSONString(dclAction.teleportSceneCoords));
                            break;
                        case ActionType.PlayAnimation:
                            //Anims from AnimatorDcl or Animator
                            refName = SceneTraverserUtils.GetIdentityName(dclAction.refGO);
                            List<string> anims = new List<string>();
                            if (dclAction.refGO.GetComponent<AnimatorDcl>())
                            {
                                anims.Add(dclAction.refGO.GetComponent<AnimatorDcl>().defaultAnimation.clip.name);
                                foreach (var dclAnim in dclAction.refGO.GetComponent<AnimatorDcl>().animations)
                                {
                                    anims.Add(dclAnim.clip.name);
                                }
                            }
                            else if (dclAction.refGO.GetComponent<Animator>())
                            {
                                foreach (var dclAnim in dclAction.refGO.GetComponent<Animator>().runtimeAnimatorController.animationClips)
                                {
                                    anims.Add(dclAnim.name);
                                }
                            }

                            if (dclAction.modifyClipParams)
                            {
                                action.AppendFormat(GetClipAnim, refName, anims[dclAction.clipIndex]);
                                //action.AppendFormat(GetClipAnim, refName, dclAction.animClip.name);
                                action.Append("\t\t\t");
                                action.AppendFormat(SetAnimLoop, refName, dclAction.loop.ToString().ToLower());
                                action.Append("\t\t\t");
                                action.AppendFormat(SetAnimSpeed, refName, SceneTraverserUtils.FloatToString(dclAction.speed));
                                action.Append("\t\t\t");
                            }
                            action.AppendFormat(PlayAnimation, refName, anims[dclAction.clipIndex], dclAction.reset.ToString().ToLower());
                                //action.AppendFormat(PlayAnimation, refName, dclAction.animClip.name, dclAction.reset.ToString().ToLower());
                            break;
                        case ActionType.StopAnimations:
                            refName = SceneTraverserUtils.GetIdentityName(dclAction.refGO);
                            action.AppendFormat(StopAnimations, refName);
                            break;
                        case ActionType.PlayStopAudio:
                            refName = SceneTraverserUtils.GetIdentityName(dclAction.refGO);
                            action.AppendFormat(GetMutableAS, refName);
                            action.Append("\t\t\t");
                            action.AppendFormat(SetAudioClip, refName, SceneTraverserUtils.GetAudioClipRelativePath(dclAction.audioClip));
                            action.Append("\t\t\t");
                            action.AppendFormat(SetAudioLoop, refName, dclAction.loop.ToString().ToLower());
                            action.Append("\t\t\t");
                            action.AppendFormat(SetAudioVolume, refName, SceneTraverserUtils.FloatToString(dclAction.volume));
                            action.Append("\t\t\t");
                            action.AppendFormat(SetAudioPitch, refName, SceneTraverserUtils.FloatToString(dclAction.pitch));
                            action.Append("\t\t\t");
                            action.AppendFormat(PlayStopAudio, refName);
                            break;
                        case ActionType.StopAudio:
                            refName = SceneTraverserUtils.GetIdentityName(dclAction.refGO);
                            action.AppendFormat(GetMutableAS, refName);
                            action.Append("\t\t\t");
                            action.AppendFormat(StopAudio, refName);
                            break;
                    }


                    functionString.AppendFormat(AddPointerButton, actionButton, action);
                }
                functionString.Remove(functionString.Length - 1, 1);

                switch (InputEventDcl.eventType)
                {
                    case EventType.PointerDown:
                        if (pointerDown)
                        {
                            Debug.LogError("Trying to export more than one pointer down event in the same gameObject: " + InputEventDcl.gameObject.name + "\nOnly the first one will be exported");
                            break;
                        }
                        pointerDown = true;
                        exportStr.AppendFormat(NewDclPointerDown, entityName, InputEventDcl.maxDistance, "IA_ANY", InputEventDcl.hoverText, functionString);
                        break;
                    case EventType.PointerUp:
                        if (pointerUp)
                        {
                            Debug.LogError("Trying to export more than one pointer Up event in the same gameObject: " + InputEventDcl.gameObject.name + "\nOnly the first one will be exported");
                            break;
                        }
                        pointerUp = true;
                        exportStr.AppendFormat(NewDclPointerUp, entityName, InputEventDcl.maxDistance, "IA_ANY", InputEventDcl.hoverText, functionString);
                        break;
                }
            }
        }

        //  Entity and Transform
        private const string AddEntity = "\n\n// Entity: {0} //\nconst {0} = engine.addEntity()\n";
        private const string SetTransform = "Transform.create({0}, {{\n\tposition: {1},\n\trotation: {2},\n\tscale: {3}\n}})\n";
        private const string SetTransformParent = "Transform.create({0}, {{\n\tposition: {1},\n\trotation: {2},\n\tscale: {3},\n\tparent: {4}\n}})\n";
        private const string SetShape = "MeshRenderer.set{0}({1})\n"; 
        private const string SetCollider = "MeshCollider.set{0}({1})\n";

        //  GLTFs
        private const string NewGLTFshape = "GltfContainer.create({0}, {{\n\tsrc: \"{1}\",\n";
        private const string EndGLTFshape = "})\n";

        //  Materials
        private const string NewMaterial = "Material.setPbrMaterial({0},{{\n";
        private const string EndMaterial = "})\n";
        private const string SetMaterialAlbedoColor = "\talbedoColor: {0},\n";
        private const string SetMaterialMetallic = "\tmetallic: {0},\n";
        private const string SetMaterialRoughness = "\troughness: {0},\n";
        private const string SetMaterialTexture = "\ttexture: Material.Texture.Common({{\n\tsrc:\"{0}\",\n\t}}),\n";
        private const string SetMaterialOpaque = "\ttransparencyMode: MaterialTransparencyMode.MTM_OPAQUE,\n";
        private const string SetMaterialAlphaTest = "\ttransparencyMode: MaterialTransparencyMode.MTM_ALPHA_TEST,\n";
        private const string SetMaterialAlpha = "\talphaTest: {0},\n";
        private const string SetMaterialAlphaBlend = "\ttransparencyMode: MaterialTransparencyMode.MTM_ALPHA_BLEND,\n";
        private const string SetMaterialAlphaTrans = "\ttransparencyMode: MaterialTransparencyMode.MTM_ALPHA_TEST_AND_ALPHA_BLEND,\n";
        private const string SetMaterialBumptexture = "\tbumpTexture: Material.Texture.Common({{\nsrc:\"{0}\",\n}}),\n";
        private const string SetMaterialEmissiveColor = "\temissiveColor: {0},\n";
        private const string SetMaterialEmissiveIntensity = "\temissiveIntensity: {0},\n";
        private const string SetMaterialEmissiveTexture = "\temissiveTexture: Material.Texture.Common({{\n\tsrc:\"{0}\",\n\t}}),\n";
        
        //  Animations
        private const string NewAnimator = "Animator.create({0},{{\n\tstates:[";
        private const string EndAnimator = "]\n})\n";
        private const string AddClip = "{{\n\tclip: \"{0}\",\n\tplaying: {1},\n\tloop: {2},\n\tspeed: {3}\n\t}},";
        private const string PlayAnimation = "Animator.playSingleAnimation({0}, \"{1}\", {2})";
        private const string StopAnimations = "Animator.stopAllAnimations({0})";
        private const string GetClipAnim = "var animClip_{0} = Animator.getClip({0}, \"{1}\")\n";
        private const string SetAnimLoop = "animClip_{0}.loop = {1}\n";
        private const string SetAnimSpeed = "animClip_{0}.speed = {1}\n";

        //  TextShapes
        private const string NewTextShape = "TextShape.create({0}, {{\n";
        private const string EndTextShape = "}})\n";
        private const string TextShapeValue = "\ttext: \"{0}\",\n";
        private const string TextShapeWidth = "\twidth: {0},\n";
        private const string TextShapeHeight = "\theight: {0},\n";
        private const string TextShapeColor = "\ttextColor: {0},\n";
        private const string TextShapeFontSize = "\tfontSize: {0},\n";
        private const string TextShapeFont = "\tfont: Font.{0},\n";
        private const string TextShapeAlign = "\ttextAlign: TextAlignMode.{0},\n";
        private const string TextShapeSpacing = "\tlineSpacing: {0},\n";
        private const string TextShapeWrapping = "\ttextWrapping: {0},\n";
        private const string TextShapeOutlineColor = "\toutlineColor: {0},\n";
        private const string TextShapeOutlineWidth = "\toutlineWidth: {0},\n";
        private const string TextShapePadding = "\tpaddingLeft: {0},\n\tpaddingTop: {1},\n\tpaddingRight: {2},\n\tpaddingBottom: {3},\n";

        // Dcl Pointer Event
        private const string NewDclPointerDown = "pointerEventsSystem.onPointerDown(\n\t{{entity: {0}, opts: {{button: InputAction.{2}, maxDistance: {1}, hoverText:\"{3}\"}}}},\n\tfunction(cmd){{\n{4}\n\t}}\n)\n";
        private const string NewDclPointerUp = "pointerEventsSystem.onPointerUp(\n\t{{entity: {0}, opts: {{button: InputAction.{2}, maxDistance: {1}, hoverText:\"{3}\"}}}},\n\tfunction(cmd){{\n{4}\n\t}}\n)\n";
        private const string AddPointerButton = "\t\tif(cmd.button === InputAction.{0}){{\n\t\t\t{1}\n\t\t}}\n";
        private const string OpenUrl = "openExternalUrl({{url: \"{0}\"}})";
        private const string TpIn = "movePlayerTo({{ newRelativePosition: {0}, cameraTarget: {1}}})";
        private const string TpOut = "teleportTo({{worldCoordinates: {0}}})";

        // Audio
        private const string NewAudioSource = "AudioSource.create({0}, {{\n\taudioClipUrl: {1},\n\tplaying: {2},\n\tloop: {3},\n\tvolume: {4},\n\tpitch: {5}\n}})\n";
        private const string GetMutableAS = "var audioSource_{0} = AudioSource.getMutable({0})\n";
        private const string SetAudioClip = "audioSource_{0}.audioClipUrl = \"{1}\"\n";
        private const string SetAudioLoop = "audioSource_{0}.loop = {1}\n";
        private const string SetAudioVolume = "audioSource_{0}.volume = {1}\n";
        private const string SetAudioPitch = "audioSource_{0}.pitch = {1}\n";
        private const string PlayStopAudio = "audioSource_{0}.playing = true";
        private const string StopAudio = "audioSource_{0}.playing = false";

        // Video Stream / Player
        private const string NewVideoPlayer = "VideoPlayer.create({0}, {{ src: \"{1}\", playing: {2}}})\n";
        private const string NewVideoTexture = "\ttexture: Material.Texture.Video({{ videoPlayerEntity: {0}}}),\n";
        private const string StreamVideoPointer = "\t\tVideoPlayer.getMutable({0}).playing = !VideoPlayer.getMutable({0}).playing";

        // Trigger Areas
        private const string NewTrigger = "utils.triggers.addTrigger({0},\n\tutils.NO_LAYERS,\n\tutils.LAYER_1,\n\t[{{  type: 'box',\n\t\tscale: {1},\n\t}}],\n\tfunction(){{\t//Enter\n\t\t{2}\n\t}},\n\tfunction(){{\t//Exit\n\t\t{3}\n\t}}\n)\n";

        //Billboard
        private const string NewBillboard = "Billboard.create({0}, {{\n\tbillboardMode: BillboardMode.{1},\n}})\n";

        //Areas
        private const string NewModifierArea = "AvatarModifierArea.create({0}, {{\n\tarea: {1},\n\tmodifiers: [AvatarModifierType.AMT_{2}],\n\texcludeIds:[]\n}})\n";
        private const string NewCameraMod = "CameraModeArea.create({0}, {{\n\tarea: {1},\n\tmode: CameraType.CT_{2},\n}})\n";

        #endregion
    }
}