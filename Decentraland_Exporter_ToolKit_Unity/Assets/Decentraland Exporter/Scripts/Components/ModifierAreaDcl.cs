using UnityEngine;
using UnityEditor;

[AddComponentMenu("Dcl Exporter ToolKit/Modifier Area")]
public class ModifierAreaDcl : MonoBehaviour
{
    [HideInInspector] public enum modifierType { cameraMod, avatarMod }
    [HideInInspector] public modifierType modType;

    [HideInInspector] public enum forceCamera { firstPerson, thirdPerson }
    [HideInInspector] public forceCamera forceCam;

    [HideInInspector] public enum avatarModification { hideAvatar, disablePassport }
    [HideInInspector] public avatarModification avatarModType;

    [HideInInspector] public bool debug = true;
    
}

[CustomEditor(typeof(ModifierAreaDcl))]
 [CanEditMultipleObjects]
public class DclObjectInspector : Editor
{
    public override void OnInspectorGUI()
    {
        ModifierAreaDcl obj = (ModifierAreaDcl)target;

        GUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Modifier Type:");
        obj.modType = (ModifierAreaDcl.modifierType)EditorGUILayout.EnumPopup(obj.modType);
        EditorGUILayout.EndHorizontal();
        GUILayout.Space(5);
        switch (obj.modType)
        {
            case ModifierAreaDcl.modifierType.cameraMod:
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Force Camera:");
                obj.forceCam = (ModifierAreaDcl.forceCamera)EditorGUILayout.EnumPopup(obj.forceCam);
                EditorGUILayout.EndHorizontal();
                break;
            case ModifierAreaDcl.modifierType.avatarMod:
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Avatar Modifier:");
                obj.avatarModType = (ModifierAreaDcl.avatarModification)EditorGUILayout.EnumPopup(obj.avatarModType);
                EditorGUILayout.EndHorizontal();
                break;
            default:
                break;
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Debug Mode:");
        obj.debug = EditorGUILayout.Toggle(obj.debug);
        EditorGUILayout.EndHorizontal();
    }
}