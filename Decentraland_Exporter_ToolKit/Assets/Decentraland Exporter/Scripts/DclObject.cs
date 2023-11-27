using UnityEngine;
using UnityEditor;

namespace DCLExport
{
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [AddComponentMenu("Dcl Exporter ToolKit/DclObject")]
    public class DclObject : MonoBehaviour
    {
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
    }
}