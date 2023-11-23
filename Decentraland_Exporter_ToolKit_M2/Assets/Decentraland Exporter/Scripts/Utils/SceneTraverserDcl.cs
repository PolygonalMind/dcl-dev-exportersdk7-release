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
                if (resourceRecorder.importedModulesECS.IndexOf("engine") < 0)
                    resourceRecorder.importedModulesECS.Add("engine");
                
                //Add New Entity
                exportStr.AppendFormat(AddEntity, entityName, tra.name);
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
            if (tra.gameObject.GetComponent<SkinnedBased_Mesh>())
            {
                tra.gameObject.GetComponent<DclObject>().dclNodeType = EDclNodeType.gltf;
                tra.gameObject.GetComponent<SkinnedBased_Mesh>().BuildMesh();
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

                if (resourceRecorder.importedModulesECS.IndexOf("Transform") < 0)
                    resourceRecorder.importedModulesECS.Add("Transform");
                
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
                
                ProcessMaterial(tra, false, entityName, exportStr, resourceRecorder, statistics);
                

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

                    //Warnings
                    if (warningRecorder != null)
                    {
                        //OutOfLand
                        if (_sceneMeta)
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

                //Warnings
                if (warningRecorder != null)
                {
                    //OutOfLand
                    if (_sceneMeta)
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
            if (warningRecorder != null)
            {
                //GameObject name
                if (_sceneMeta)
                {
                    if (tra.gameObject.name.Contains(":"))
                    {
                        warningRecorder.InvalidNamingWarnings.Add(new SceneWarningRecorder.InvalidNaming(tra.gameObject));
                    }
                }
            }
        }

#region Functions

        public static void ProcessShape(Transform tra, string entityName, StringBuilder exportStr,
            ResourceRecorder resourceRecorder, SceneStatistics statistics, ExportFormat mFormat)
        {
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
            
            if (!(meshFilter && tra.GetComponent<MeshRenderer>() || tra.GetComponent<SkinnedMeshRenderer>()))
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
            }

            if (shapeName != null)
            {
                if (exportStr == null) return;
                if (resourceRecorder.importedModulesECS.IndexOf("MeshRenderer, MeshCollider") < 0)
                    resourceRecorder.importedModulesECS.Add("MeshRenderer, MeshCollider");
                
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
                if (resourceRecorder.importedModulesECS.IndexOf("GltfContainer") < 0)
                    resourceRecorder.importedModulesECS.Add("GltfContainer");

                bool bIsOvewritePrefab = true;
                PrefabUtility.GetCorrespondingObjectFromSource(tra.gameObject);
                //It's Prefab & MeshRenderer
                if (PrefabUtility.GetPrefabInstanceHandle(tra.gameObject) != null && tra.gameObject.GetComponent<MeshFilter>() != null && tra.gameObject.GetComponent<MeshFilter>().sharedMesh)
                {
                    bIsOvewritePrefab = false;
                    if (tra.gameObject.GetComponent<overwriteMesh_script>() != null && tra.gameObject.GetComponent<overwriteMesh_script>().exportToSingleGTLF)
                    {
                        bIsOvewritePrefab = true;
                    }
                    if (PrefabUtility.GetAddedGameObjects(tra.gameObject).ToArray().Length > 0 && tra.gameObject.GetComponent<overwriteMesh_script>() == null)
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
                    if (tra.gameObject.GetComponent<overwriteMesh_script>() && tra.gameObject.GetComponent<overwriteMesh_script>().export_custom_GTLF_name != null && tra.gameObject.GetComponent<overwriteMesh_script>().export_custom_GTLF_name != "")
                    {
                        name = tra.gameObject.GetComponent<overwriteMesh_script>().export_custom_GTLF_name;
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
                    if (resourceRecorder != null)
                    {
                        if (index == -1)
                        {
                            resourceRecorder.exportedModelsFileName.Add(SceneTraverserUtils.GetIdentityName(tra.gameObject));
                            resourceRecorder.exportedModels.Add(name);
                            resourceRecorder.meshesToExport.Add(tra.gameObject);
                        }
                    }
                }
            }
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
                            if (resourceRecorder.importedModulesECS.IndexOf("Material") < 0)
                                resourceRecorder.importedModulesECS.Add("Material");
                            if(resourceRecorder.importedModulesECS.IndexOf("MaterialTransparencyMode") < 0)
                                resourceRecorder.importedModulesECS.Add("MaterialTransparencyMode");


                            //New Material
                            exportStr.AppendFormat(NewMaterial, entityName);

                            if (exportStr != null && albedoTex)
                            {
                                exportStr.AppendFormat(SetMaterialTexture, SceneTraverserUtils.GetTextureRelativePath(albedoTex));
                            }

                            if (resourceRecorder.importedModulesMATH.IndexOf("Color4") < 0)
                                resourceRecorder.importedModulesMATH.Add("Color4");
                            
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
                            if (resourceRecorder.importedModulesMATH.IndexOf("Color3") < 0)
                                resourceRecorder.importedModulesMATH.Add("Color3");
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
        

        #endregion
    }
}