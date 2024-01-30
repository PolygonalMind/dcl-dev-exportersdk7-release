using UnityEngine;
using UnityEditor;

namespace DCLExport
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [AddComponentMenu("Dcl Exporter ToolKit/DclObject")]
    public class DclObject : MonoBehaviour
    {
        //If true this object will be exported as a single GLTF and not as prefab
        public bool exportToSingleGTLF = false;
        public bool glbPlaceholder = false;
        public string placeholderPath = "unity-assets/model.glb";

        public bool debugBounds = false;
        // If true this object will be ignored during the export
        public bool ignoreObject = false;

        // Used to identify the type of the object during the export
        public DclPrimitiveType dclPrimitiveType = DclPrimitiveType.other;

        //Used to display Hierarchy icons
        public EDclNodeType dclNodeType;

        //Entity name in Dcl
        public string dclName;

        //Dcl primitive collision export
        public bool withCollision = false;

        //Function used to order the object components in the inspector
        public void orderComps()
        {
            Component[] comps = this.gameObject.GetComponents<Component>();

            if (comps[1] != this && PrefabUtility.GetPrefabInstanceHandle(this.gameObject) == null)  //comp[1] because comp[0] is the transformer that cant be moved down]
            {
                UnityEditorInternal.ComponentUtility.MoveComponentUp(this);
                orderComps();
            }
        }
        void OnDrawGizmosSelected()
        {
            if (dclNodeType == EDclNodeType.area)
            {
                Gizmos.color = Color.yellow;
                DrawGizmos();
            }
            else
            {
                if (!DclExporter.showBoundingBoxes) return;
                Gizmos.color = Color.yellow;
                DrawGizmos();
            }
        }
        void OnDrawGizmos()
        {
            if (GetComponent<ModifierAreaDcl>())
            {
                Gizmos.color = Color.green;
                DrawGizmos();
            }
            else if (GetComponent<TriggerDcl>())
            {
                Gizmos.color = Color.magenta;
                DrawGizmos();
            }
            else
            {
                if (!debugBounds) return;
                Gizmos.color = Color.yellow;
                DrawGizmos();
            }
        }

        private void DrawGizmos()
        {
            var wr = FindObjectOfType<DclSceneMeta>().sceneWarningRecorder;

            foreach (var warn in wr.OutOfLandWarnings)
            {
                if (warn.renderer == GetComponent<Renderer>())
                    Gizmos.color = Color.red;
            }
            foreach (var warn in wr.AreaOutOfLandWarnings)
            {
                if (warn.renderer == this.gameObject)
                    Gizmos.color = Color.red;
            }
            foreach (var warn in wr.OutOfHeightLandWarnings)
            {
                if (warn.renderer == GetComponent<Renderer>())
                    Gizmos.color = Color.blue;
            }
            foreach (var warn in wr.AreaOutOfHeightLandWarnings)
            {
                if (warn.renderer == this.gameObject)
                    Gizmos.color = Color.blue;
            }

            if (GetComponent<MeshRenderer>())
            {
                var bb = GetComponent<MeshRenderer>().bounds;
                Gizmos.DrawWireCube(bb.center, bb.size);
            }
            else if (GetComponent<SkinnedMeshRenderer>())
            {
                var bb = GetComponent<SkinnedMeshRenderer>().bounds;
                Gizmos.DrawWireCube(bb.center, bb.size);
            }
            else if (dclNodeType == EDclNodeType.area)
            {
                Gizmos.DrawWireCube(transform.position, transform.lossyScale);
            }
        }

        public void AddForced_GLTF()
        {
            gameObject.AddComponent<Force_GLTF>();

            //Refresh
            var sceneMeta = FindFirstObjectByType<DclSceneMeta>();
            sceneMeta.RefreshStatistics();
            sceneMeta.getParcelSetVolumes();
        }
        public void DisableForcedGLTF()
        {
            if (GetComponent<Force_GLTF>())
                DestroyImmediate(GetComponent<Force_GLTF>());
            if (GetComponent<MeshFilter>() && GetComponent<MeshRenderer>())
            {
                DestroyImmediate(GetComponent<MeshFilter>());
                DestroyImmediate(GetComponent<MeshRenderer>());
            }

            //Refresh
            var sceneMeta = FindFirstObjectByType<DclSceneMeta>();
            sceneMeta.RefreshStatistics();
            sceneMeta.getParcelSetVolumes();
        }
        public void BreakChild()
        {
            gameObject.AddComponent<Break_Child>();
            //Refresh
            var sceneMeta = FindFirstObjectByType<DclSceneMeta>();
            sceneMeta.RefreshStatistics();
            sceneMeta.getParcelSetVolumes();
        }
        public void DisableBreakChild()
        {
            if (GetComponent<Break_Child>())
                DestroyImmediate(GetComponent<Break_Child>());
            //Refresh
            var sceneMeta = FindFirstObjectByType<DclSceneMeta>();
            sceneMeta.RefreshStatistics();
            sceneMeta.getParcelSetVolumes();
        }
    }
    
    [CustomEditor(typeof(DclObject))]

    public class DclObjectEditor : Editor
    {
        DclObject t;
        SerializedObject GetTarget;

        private int SPACE = 10;
        private static GUIStyle labelStyle = new GUIStyle();

        void OnEnable()
        {
            t = (DclObject)target;
            GetTarget = new SerializedObject(t);
        }
        private void SetUp()
        {
            labelStyle.normal.textColor = GUI.contentColor;
        }

        public override void OnInspectorGUI()
        {
            SetUp();
            
            //Type of event
            SerializedProperty dclName = GetTarget.FindProperty("dclName");
            SerializedProperty dclNodeType = GetTarget.FindProperty("dclNodeType");
            SerializedProperty ignoreObject = GetTarget.FindProperty("ignoreObject");
            SerializedProperty debugBounds = GetTarget.FindProperty("debugBounds");
            SerializedProperty glbPlaceholder = GetTarget.FindProperty("glbPlaceholder");
            SerializedProperty placeholderPath = GetTarget.FindProperty("placeholderPath");
            SerializedProperty withCollision = GetTarget.FindProperty("withCollision");
            SerializedProperty exportToSingleGTLF = GetTarget.FindProperty("exportToSingleGTLF");

            //Update our list
            GetTarget.Update();

            GUILayout.Space(SPACE);
            GUILayout.Label(string.Format("Dcl name:  <color=#A8EDA8>{0}</color>", dclName.stringValue), labelStyle);
            GUILayout.Label(string.Format("Node type:  <color=#A8EDA8>{0}</color>", dclNodeType.enumNames[dclNodeType.enumValueIndex]), labelStyle);
            debugBounds.boolValue = EditorGUILayout.ToggleLeft("Debug bounding box", debugBounds.boolValue);
            GUILayout.Space(SPACE);
            ignoreObject.boolValue = EditorGUILayout.ToggleLeft("Ignore this Object in export", ignoreObject.boolValue);

            if (ignoreObject.boolValue) // Is ignored, wont be exported
            {
                GUILayout.Label(string.Format("<color=#F0637F>This gameObject will be ignored in exports</color>"), labelStyle);

                GetTarget.ApplyModifiedProperties();
                return;
            }

            GUILayout.BeginHorizontal();
            glbPlaceholder.boolValue = EditorGUILayout.ToggleLeft("GLTF Placeholder", glbPlaceholder.boolValue, GUILayout.Width(120)); //Is a glb/gltf placeholder
            GUILayout.Label(new GUIContent("[ ? ]", "Set this entity as a placeholder, that means that this object will export only a transform and GTLF container to the given path"), labelStyle);
            GUILayout.EndHorizontal();
            if (glbPlaceholder.boolValue)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Path:", GUILayout.Width(40));
                GUILayout.TextField(placeholderPath.stringValue);
                GUILayout.EndHorizontal();
            }

            if (dclNodeType.enumValueIndex == 11 && PrefabUtility.IsPartOfAnyPrefab(t.gameObject))//Is gltf && is prefab gameObject
            {
                GUILayout.Space(SPACE);
                GUILayout.BeginHorizontal();
                exportToSingleGTLF.boolValue = EditorGUILayout.ToggleLeft("Export Single GLTF", exportToSingleGTLF.boolValue, GUILayout.Width(125));
                GUILayout.Label(new GUIContent("[ ? ]", "This option allow to export a prefab as a single GLTF instead of using the same gltf for each prefab"), labelStyle);
                GUILayout.EndHorizontal();
            }

            if (dclNodeType.enumValueIndex >= 4 && dclNodeType.enumValueIndex <=9) //Is dcl primitive entity
                withCollision.boolValue = EditorGUILayout.ToggleLeft("Add Collision to primitive object", withCollision.boolValue);

            GUILayout.Space(SPACE);

            // IF EMPTY ENTITY -> ADD FORCED_GLTF BUTTON
            if (dclNodeType.enumValueIndex == 2 && GUILayout.Button("Force GLTF")) //Is empty entity
            {
                t.AddForced_GLTF();
                return;
            }
            // IF FORCED GLTF -> DISABLE FORCED_GLTF
            if (dclNodeType.enumValueIndex == 14 && GUILayout.Button("Disable Forced GLTF")) //Is Forced GLTF
            {
                t.DisableForcedGLTF();
                return;
            }
            // IF GLTF CHILD -> ADD BREAK GLTF CHILD BUTTON
            if (dclNodeType.enumValueIndex == 12 && GUILayout.Button("Break GLTF Child")) //Is gltf child
            {
                t.BreakChild();
                return;
            }
            // IF GLTF CHILD BROKEN -> DISABLE BREAK GLTF CHILD
            if (t.GetComponent<Break_Child>() && GUILayout.Button("Disable Break GLTF Child")) //Has BreakChild component
            {
                t.DisableBreakChild();
                return;
            }

            GetTarget.ApplyModifiedProperties();
        }
    }
}