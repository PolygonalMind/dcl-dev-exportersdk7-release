using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DCLExport
{
    public static class DclPrimitiveHelper
    {
		public static void ConvertToDclPrimitive(DclObject dclObject, PrimitiveType primitiveType){
			
			switch (primitiveType) {
			case PrimitiveType.Cube:
				dclObject.dclPrimitiveType = DclPrimitiveType.box;
				break;
			case PrimitiveType.Cylinder:
				dclObject.dclPrimitiveType = DclPrimitiveType.cylinder;
				break;
			case PrimitiveType.Sphere:
				dclObject.dclPrimitiveType = DclPrimitiveType.sphere;
				break;
			case PrimitiveType.Quad:
				dclObject.dclPrimitiveType = DclPrimitiveType.plane;
				break;
			}

			SetDclPrimitiveMesh (dclObject, dclObject.dclPrimitiveType);
		}

		public static void SetDclPrimitiveMesh(DclObject dclObject, DclPrimitiveType primitiveType){
			var meshFilter = dclObject.GetComponent<MeshFilter> ();
			switch (primitiveType) {
			case DclPrimitiveType.cylinder:
				{
					meshFilter.sharedMesh = DclPrimitiveMeshBuilder.BuildCylinder (50, 0.5f, 0.5f, 1f, 0f, true, false); 
				}
				break;
			case DclPrimitiveType.box:
				{
					meshFilter.sharedMesh = DclPrimitiveMeshBuilder.BuildCube (1f);
				}
				break;
			case DclPrimitiveType.plane:
				{
					meshFilter.sharedMesh = DclPrimitiveMeshBuilder.BuildPlane (1f);
				}
				break;
			case DclPrimitiveType.sphere:
				{
                   meshFilter.sharedMesh = DclPrimitiveMeshBuilder.BuildSphere (0.5f);
                }
				break;

			}
		}

        public static GameObject CreateDclPrimitive(DclPrimitiveType type, bool withCollider = true,
            bool putOnFocusPosition = true)
        {
            GameObject gameObject = new GameObject(type.ToString());
			gameObject.AddComponent<MeshFilter>();
            if (putOnFocusPosition)
            {
                gameObject.transform.position = SceneView.lastActiveSceneView.pivot;
                Selection.objects = new Object[] {gameObject};
                EditorUtility.SetDirty(gameObject);
                EditorSceneManager.MarkSceneDirty(gameObject.scene);
            }
            //gameObject.transform.rotation = new Quaternion(0,0,1,0);

            var meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = PrimitiveHelper.GetDefaultMaterial();

            var dclObj = gameObject.AddComponent<DclObject>();
            dclObj.withCollision = withCollider;
			dclObj.dclPrimitiveType = type;


			SetDclPrimitiveMesh (dclObj, dclObj.dclPrimitiveType);

            return gameObject;
        }

        internal static T LoadAssetAtPath<T>(string InPath) where T : UnityEngine.Object
        {
            return (T)AssetDatabase.LoadAssetAtPath(InPath, typeof(T));
        }

        [MenuItem("GameObject/DCL Object/Box", false, -100)]
        static void CreateBox()
        {
            CreateDclPrimitive(DclPrimitiveType.box);
        }
        [MenuItem("GameObject/DCL Object/Plane", false, -99)]
        static void CreatePlane()
        {
            CreateDclPrimitive(DclPrimitiveType.plane);
        }
        [MenuItem("GameObject/DCL Object/Sphere", false, -98)]
        static void CreateSphere()
        {
            CreateDclPrimitive(DclPrimitiveType.sphere);
        }
        [MenuItem("GameObject/DCL Object/Cylinder", false, -97)]
        static void CreateCylinder()
        {
            CreateDclPrimitive(DclPrimitiveType.cylinder);
        }
    }

    public enum DclPrimitiveType
    {
        box=0,
        plane,
        sphere,
        cylinder,
		other,
    }

    public enum DclAreaType
    {
        TriggerArea = 0,
        ModifierArea,
    }
    
    public static class DclAreaHelper
    {
        public static GameObject CreateDclArea(DclAreaType type, bool putOnFocusPosition = false)
        {
            GameObject gameObject = new GameObject(type.ToString());
            if (putOnFocusPosition)
            {
                gameObject.transform.position = SceneView.lastActiveSceneView.pivot;
                Selection.objects = new Object[] { gameObject };
                EditorUtility.SetDirty(gameObject);
                EditorSceneManager.MarkSceneDirty(gameObject.scene);
            }
            else
            {
                gameObject.transform.position = new Vector3(8, 1.5f, 8);
                gameObject.transform.localScale = new Vector3(4, 3, 4);

            }
            
            return gameObject;
        }

        [MenuItem("GameObject/DCL Area/Trigger", false, -100)]
        static void CreateTriggerArea()
        {
            CreateDclArea(DclAreaType.TriggerArea);
        }
        [MenuItem("GameObject/DCL Area/Modifier Area", false, -98)]
        static void CreateModifierArea()
        {
            CreateDclArea(DclAreaType.ModifierArea);
        }
    }
}