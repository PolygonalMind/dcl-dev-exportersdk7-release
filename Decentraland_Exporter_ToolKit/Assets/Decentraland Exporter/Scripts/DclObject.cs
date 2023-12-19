using UnityEngine;
using UnityEditor;

namespace DCLExport
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [AddComponentMenu("Dcl Exporter ToolKit/DclObject")]
    public class DclObject : MonoBehaviour
    {
        public bool debugBounds = false;
        // If true this object will be ignored during the export
        public bool ignoreObject = false;

        // Used to identify the type of the object during the export
        [HideInInspector] public DclPrimitiveType dclPrimitiveType = DclPrimitiveType.other;
        
        //Used to display Hierarchy icons
        [HideInInspector] public EDclNodeType dclNodeType;
        
        //Entity name in Dcl
        [SerializeField] public string dclName;

        //Dcl primitive collision export
        [Tooltip("Only available for Dcl primitives")]
        [Header("Only for Dcl Primitives:")]
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
        #region Gizmos
        void OnDrawGizmosSelected()
        {
            if (dclNodeType == EDclNodeType.area)
            {
                Gizmos.color = Color.yellow;
                DrawGizmos();
            }
            else
            {
                if (!debugBounds) return;
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
                if (!DclExporter.showBoundingBoxes) return;
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
        #endregion
    }
}