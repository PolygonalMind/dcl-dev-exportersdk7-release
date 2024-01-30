using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

//STRUCTS
namespace DCLExport
{
    public enum TriggerActionType
    {
        TeleportInsideScene,
        PlayAnimation,
        StopAnimations,
        PlayStopAudio,
        StopAudio,
        SetTransform,
        AddTransform
    }
    
    [Serializable]
    public class DclTriggerAction
    {
        public TriggerActionType actionType = new TriggerActionType();

        //General
        public GameObject refGO;
        //Tp inside scene
        public GameObject lookAt;
        public bool fixedPosition = false;
        public bool Pos;
        public bool Rot;
        public bool Sca;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public Vector3 teleportLookAt;
        //Anim
        public bool modifyClipParams = false;
        public bool reset = true;
        public float speed = 1;
        public AnimationClip animClip;
        public int clipIndex = 0;
        //Audio
        public AudioClip audioClip;
        public bool loop;
        public float volume = 1;
        public float pitch = 1;
    }

    [AddComponentMenu("Dcl Exporter ToolKit/Trigger Area")]
    public class TriggerDcl : MonoBehaviour
    {
        //Exporter Functionality
        public List<DclTriggerAction> actionListEnter = new List<DclTriggerAction>(1);
        public List<DclTriggerAction> actionListExit = new List<DclTriggerAction>(1);
    }

    [CustomEditor(typeof(TriggerDcl))]
    public class CustomTriggerEditor : Editor
    {
        internal static GUIStyle warningStyle = new GUIStyle();
        TriggerDcl t;
        SerializedObject GetTarget;
        SerializedProperty listEnter;
        SerializedProperty listExit;
        int ListSizeEnter;
        int ListSizeExit;

        void OnEnable()
        {
            t = (TriggerDcl)target;
            GetTarget = new SerializedObject(t);
            listEnter = GetTarget.FindProperty("actionListEnter"); // Find the List in our script and create a refrence of it
            listExit = GetTarget.FindProperty("actionListExit");
            
            warningStyle.normal.textColor = new Color(1f, 0.4f, 0.4f, 1f);
            warningStyle.fontSize = 12;
        }

        public override void OnInspectorGUI()
        {

            //Update our list
            GetTarget.Update();

            //Choose how to display the list<> Example purposes only
            EditorGUILayout.Space();

            //ENTER
            GUILayout.BeginVertical("Box");
            ListSizeEnter = listEnter.arraySize;
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Trigger Enter List", EditorStyles.boldLabel);
            ListSizeEnter = EditorGUILayout.IntField(ListSizeEnter);
            GUILayout.EndHorizontal();

            if (ListSizeEnter != listEnter.arraySize)
            {
                while (ListSizeEnter > listEnter.arraySize)
                {
                    listEnter.InsertArrayElementAtIndex(listEnter.arraySize);
                }
                while (ListSizeEnter < listEnter.arraySize)
                {
                    listEnter.DeleteArrayElementAtIndex(listEnter.arraySize - 1);
                }
            }
            EditorGUILayout.Space();

            //Display Enter Event list to the inspector window
            for (int i = 0; i < listEnter.arraySize; i++)
            {
                SerializedProperty MyListRef = listEnter.GetArrayElementAtIndex(i);

                //Lists
                SerializedProperty actionType = MyListRef.FindPropertyRelative("actionType");
                //Tp inside scene
                SerializedProperty fixedPosition = MyListRef.FindPropertyRelative("fixedPosition");
                SerializedProperty position = MyListRef.FindPropertyRelative("position");
                SerializedProperty teleportLookAt = MyListRef.FindPropertyRelative("teleportLookAt");
                //Animation
                SerializedProperty modifyClip = MyListRef.FindPropertyRelative("modifyClipParams");
                SerializedProperty reset = MyListRef.FindPropertyRelative("reset");
                SerializedProperty speed = MyListRef.FindPropertyRelative("speed");
                SerializedProperty refGO = MyListRef.FindPropertyRelative("refGO");
                SerializedProperty lookAt = MyListRef.FindPropertyRelative("lookAt");
                SerializedProperty animClip = MyListRef.FindPropertyRelative("animClip");
                SerializedProperty clipIndex = MyListRef.FindPropertyRelative("clipIndex");
                //Audio
                SerializedProperty audioClip = MyListRef.FindPropertyRelative("audioClip");
                SerializedProperty loop = MyListRef.FindPropertyRelative("loop");
                SerializedProperty volume = MyListRef.FindPropertyRelative("volume");
                SerializedProperty pitch = MyListRef.FindPropertyRelative("pitch");

                //SetTransform
                SerializedProperty rotation = MyListRef.FindPropertyRelative("rotation");
                SerializedProperty scale = MyListRef.FindPropertyRelative("scale");
                SerializedProperty Pos = MyListRef.FindPropertyRelative("Pos");
                SerializedProperty Rot = MyListRef.FindPropertyRelative("Rot");
                SerializedProperty Sca = MyListRef.FindPropertyRelative("Sca");

                GUILayout.BeginHorizontal("Button");
                GUILayout.BeginVertical();
                var foldout = EditorUtil.GUILayout.AutoSavedFoldout("DclFoldEnter" + i.ToString(), "Enter " + i.ToString(), true, null);
                if (foldout)
                {
                
                    actionType.enumValueIndex = EditorGUILayout.Popup("Action", actionType.enumValueIndex, actionType.enumDisplayNames);

                    EditorGUILayout.Space();
                
                    switch (actionType.enumValueIndex)
                    {
                        case 0: //Tp In
                            fixedPosition.boolValue = EditorGUILayout.Toggle("Fixed Position", fixedPosition.boolValue);
                            if (fixedPosition.boolValue)
                            {
                                position.vector3Value = EditorGUILayout.Vector3Field("Position", position.vector3Value);
                                teleportLookAt.vector3Value = EditorGUILayout.Vector3Field("Look at", teleportLookAt.vector3Value);
                            }
                            else
                            {
                                refGO.objectReferenceValue = EditorGUILayout.ObjectField("GameObject Position", refGO.objectReferenceValue, typeof(GameObject), true);
                                lookAt.objectReferenceValue = EditorGUILayout.ObjectField("Look at Position", lookAt.objectReferenceValue, typeof(GameObject), true);
                            }
                            break;
                        case 1: //Play Anim
                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Object to animate", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            //animClip.objectReferenceValue = EditorGUILayout.ObjectField("Animation Clip", animClip.objectReferenceValue, typeof(AnimationClip), true);

                            ///Get Anims from AnimatorDcl
                            var go = refGO.objectReferenceValue as GameObject;
                            if (go == null) break;
                            if (!go.GetComponent<AnimatorDcl>() && !go.GetComponent<Animator>() && !go.GetComponent<Animation>())
                            {
                                GUILayout.Label(string.Format("The object to animate: \"{0}\"\nis not exporting anim clips to the Dcl code\nAdd an Animator or AnimatorDcl", go.name), warningStyle);
                                break;
                            }
                            else if (!go.GetComponent<AnimatorDcl>() && go.GetComponent<Animator>())
                            {
                                if (go.GetComponent<Animator>().runtimeAnimatorController == null)
                                {
                                    GUILayout.Label(string.Format("Animator without controller\nThe object to animate \"{0}\"\ndon't have an animator controller.", go.name), warningStyle);
                                    break;
                                }
                                else if (go.GetComponent<Animator>().runtimeAnimatorController.animationClips.Length <= 0)
                                {
                                    GUILayout.Label(string.Format("Animator controller without clips\nThe object to animate \"{0}\"\ndon't have clips.", go.name), warningStyle);
                                    break;
                                }

                                List<string> animatorAnims = new List<string>();
                                foreach (var dclAnim in go.GetComponent<Animator>().runtimeAnimatorController.animationClips)
                                {
                                    animatorAnims.Add(dclAnim.name);
                                }
                                if (animatorAnims.Count > 0)
                                {
                                    clipIndex.intValue = EditorGUILayout.Popup("Animation Clip", clipIndex.intValue, animatorAnims.ToArray());

                                    reset.boolValue = EditorGUILayout.Toggle("Reset", reset.boolValue);
                                    modifyClip.boolValue = EditorGUILayout.Toggle("Modify Clip params", modifyClip.boolValue);

                                    if (!modifyClip.boolValue) break;

                                    loop.boolValue = EditorGUILayout.Toggle("Loop", loop.boolValue);
                                    speed.floatValue = EditorGUILayout.FloatField("Speed", speed.floatValue);
                                }
                                //animClip.objectReferenceValue = EditorGUILayout.ObjectField("Animation Clip", animClip.objectReferenceValue, typeof(AnimationClip), true);
                                break;
                            }
                            else if (!go.GetComponent<AnimatorDcl>() && go.GetComponent<Animation>())
                            {
                                GUILayout.Label(string.Format("Animation component not supported\nThe object to animate \"{0}\"\nis not exporting anim clips to the Dcl code\nAdd an Animator or AnimatorDcl.", go.name), warningStyle);
                                break;
                            }
                            else if (go.GetComponent<AnimatorDcl>() && go.GetComponent<AnimatorDcl>().defaultAnimation.clip == null)
                            {
                                GUILayout.Label(string.Format("No clips found in the \"{0}\" AnimatorDcl\nSet the animation clips", go.name), warningStyle);
                                break;
                            }

                            List<string> anims = new List<string>();
                            anims.Add(go.GetComponent<AnimatorDcl>().defaultAnimation.clip.name);
                            foreach (var dclAnim in go.GetComponent<AnimatorDcl>().animations)
                            {
                                anims.Add(dclAnim.clip.name);
                            }

                            if (anims != null && anims.Count > 0)
                            {
                                clipIndex.intValue = EditorGUILayout.Popup("Animation Clip", clipIndex.intValue, anims.ToArray());

                                reset.boolValue = EditorGUILayout.Toggle("Reset", reset.boolValue);
                                modifyClip.boolValue = EditorGUILayout.Toggle("Modify Clip params", modifyClip.boolValue);

                                if (!modifyClip.boolValue) break;

                                loop.boolValue = EditorGUILayout.Toggle("Loop", loop.boolValue);
                                speed.floatValue = EditorGUILayout.FloatField("Speed", speed.floatValue);
                            }
                            ///
                            break;
                        case 2: //Stop Anim
                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Object to animate", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            break;
                        case 3: //Play Stop Audio
                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Audio Source Object", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            audioClip.objectReferenceValue = EditorGUILayout.ObjectField("Audio Clip", audioClip.objectReferenceValue, typeof(AudioClip), true);
                            loop.boolValue = EditorGUILayout.Toggle("Loop", loop.boolValue);
                            volume.floatValue = EditorGUILayout.Slider("Volume", volume.floatValue, 0, 1);
                            pitch.floatValue = EditorGUILayout.Slider("Pitch", pitch.floatValue, 0, 5);
                            break;
                        case 4: //Stop Audio
                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Audio Source Object", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            break;
                        case 5: //Set Transform
                            fixedPosition.boolValue = EditorGUILayout.Toggle("Fixed Values", fixedPosition.boolValue);
                            EditorGUILayout.Space();

                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Target GameObject", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            EditorGUILayout.Space();
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Position");
                            Pos.boolValue = EditorGUILayout.Toggle(Pos.boolValue);
                            GUILayout.Label("Rotation");
                            Rot.boolValue = EditorGUILayout.Toggle(Rot.boolValue);
                            GUILayout.Label("Scale");
                            Sca.boolValue = EditorGUILayout.Toggle(Sca.boolValue);
                            GUILayout.EndHorizontal();
                            EditorGUILayout.Space();

                            if (fixedPosition.boolValue)
                            {
                                if (Pos.boolValue)
                                    position.vector3Value = EditorGUILayout.Vector3Field("Position", position.vector3Value);
                                if (Rot.boolValue)
                                    rotation.vector3Value = EditorGUILayout.Vector3Field("Rotation", rotation.vector3Value);
                                if (Sca.boolValue)
                                    scale.vector3Value = EditorGUILayout.Vector3Field("Scale", scale.vector3Value);
                            }
                            else
                            {
                                if (Pos.boolValue || Rot.boolValue || Sca.boolValue)
                                    lookAt.objectReferenceValue = EditorGUILayout.ObjectField("Transform Reference", lookAt.objectReferenceValue, typeof(GameObject), true);
                            }
                            break;
                        case 6: //Add Transform
                            fixedPosition.boolValue = EditorGUILayout.Toggle("Fixed Values", fixedPosition.boolValue);
                            EditorGUILayout.Space();

                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Target GameObject", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            EditorGUILayout.Space();
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Position");
                            Pos.boolValue = EditorGUILayout.Toggle(Pos.boolValue);
                            GUILayout.Label("Rotation");
                            Rot.boolValue = EditorGUILayout.Toggle(Rot.boolValue);
                            GUILayout.Label("Scale");
                            Sca.boolValue = EditorGUILayout.Toggle(Sca.boolValue);
                            GUILayout.EndHorizontal();
                            EditorGUILayout.Space();

                            if (fixedPosition.boolValue)
                            {
                                if (Pos.boolValue)
                                    position.vector3Value = EditorGUILayout.Vector3Field("Position", position.vector3Value);
                                if (Rot.boolValue)
                                    rotation.vector3Value = EditorGUILayout.Vector3Field("Rotation", rotation.vector3Value);
                                if (Sca.boolValue)
                                    scale.vector3Value = EditorGUILayout.Vector3Field("Scale", scale.vector3Value);
                            }
                            else
                            {
                                if (Pos.boolValue || Rot.boolValue || Sca.boolValue)
                                    lookAt.objectReferenceValue = EditorGUILayout.ObjectField("Transform Reference", lookAt.objectReferenceValue, typeof(GameObject), true);
                            }
                            break;
                    }
                    EditorGUILayout.Space();
                }
                GUILayout.EndVertical();
                
                if (GUILayout.Button("X", EditorStyles.miniButtonRight, GUILayout.Width(20)))
                {
                   listEnter.DeleteArrayElementAtIndex(i);
                }
                GUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add Enter Event", EditorStyles.miniButtonRight, GUILayout.MinWidth(80)))
            {
                t.actionListEnter.Add(new DclTriggerAction());
            }
            GUILayout.EndVertical();
            
            ////////////
            EditorGUILayout.Space(2);
            ////////////
            //EXIT

            GUILayout.BeginVertical("Box");
            ListSizeExit = listExit.arraySize;
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Trigger Exit List", EditorStyles.boldLabel);
            ListSizeExit = EditorGUILayout.IntField(ListSizeExit);
            GUILayout.EndHorizontal();

            if (ListSizeExit != listExit.arraySize)
            {
                while (ListSizeExit > listExit.arraySize)
                {
                    listExit.InsertArrayElementAtIndex(listExit.arraySize);
                }
                while (ListSizeExit < listExit.arraySize)
                {
                    listExit.DeleteArrayElementAtIndex(listExit.arraySize - 1);
                }
            }
            
            EditorGUILayout.Space();

            //Display Enter Event list to the inspector window
            for (int i = 0; i < listExit.arraySize; i++)
            {
                SerializedProperty MyListRef = listExit.GetArrayElementAtIndex(i);

                //Lists
                SerializedProperty actionType = MyListRef.FindPropertyRelative("actionType");
                //Tp inside scene
                SerializedProperty fixedPosition = MyListRef.FindPropertyRelative("fixedPosition");
                SerializedProperty position = MyListRef.FindPropertyRelative("position");
                SerializedProperty teleportLookAt = MyListRef.FindPropertyRelative("teleportLookAt");
                //Animation
                SerializedProperty modifyClip = MyListRef.FindPropertyRelative("modifyClipParams");
                SerializedProperty reset = MyListRef.FindPropertyRelative("reset");
                SerializedProperty speed = MyListRef.FindPropertyRelative("speed");
                SerializedProperty refGO = MyListRef.FindPropertyRelative("refGO");
                SerializedProperty lookAt = MyListRef.FindPropertyRelative("lookAt");
                SerializedProperty animClip = MyListRef.FindPropertyRelative("animClip");
                SerializedProperty clipIndex = MyListRef.FindPropertyRelative("clipIndex");
                //Audio
                SerializedProperty audioClip = MyListRef.FindPropertyRelative("audioClip");
                SerializedProperty loop = MyListRef.FindPropertyRelative("loop");
                SerializedProperty volume = MyListRef.FindPropertyRelative("volume");
                SerializedProperty pitch = MyListRef.FindPropertyRelative("pitch");

                //SetTransform
                SerializedProperty rotation = MyListRef.FindPropertyRelative("rotation");
                SerializedProperty scale = MyListRef.FindPropertyRelative("scale");
                SerializedProperty Pos = MyListRef.FindPropertyRelative("Pos");
                SerializedProperty Rot = MyListRef.FindPropertyRelative("Rot");
                SerializedProperty Sca = MyListRef.FindPropertyRelative("Sca");

                GUILayout.BeginHorizontal("Button");
                GUILayout.BeginVertical();
                var foldout = EditorUtil.GUILayout.AutoSavedFoldout("DclFoldExit" + i.ToString(), "Exit " + i.ToString(), true, null);
                if (foldout)
                {
                    actionType.enumValueIndex = EditorGUILayout.Popup("Action", actionType.enumValueIndex, actionType.enumDisplayNames);

                    EditorGUILayout.Space();
                    switch (actionType.enumValueIndex)
                    {
                        case 0: //Tp In
                            fixedPosition.boolValue = EditorGUILayout.Toggle("Fixed Position", fixedPosition.boolValue);
                            if (fixedPosition.boolValue)
                            {
                                position.vector3Value = EditorGUILayout.Vector3Field("Position", position.vector3Value);
                                teleportLookAt.vector3Value = EditorGUILayout.Vector3Field("Look at", teleportLookAt.vector3Value);
                            }
                            else
                            {
                                refGO.objectReferenceValue = EditorGUILayout.ObjectField("GameObject Position", refGO.objectReferenceValue, typeof(GameObject), true);
                                lookAt.objectReferenceValue = EditorGUILayout.ObjectField("Look at Position", lookAt.objectReferenceValue, typeof(GameObject), true);
                            }
                            break;
                        case 1: //Play Anim
                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Object to animate", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            //animClip.objectReferenceValue = EditorGUILayout.ObjectField("Animation Clip", animClip.objectReferenceValue, typeof(AnimationClip), true);

                            ///Get Anims from AnimatorDcl
                            var go = refGO.objectReferenceValue as GameObject;
                            if (go == null) break;
                            if (!go.GetComponent<AnimatorDcl>() && !go.GetComponent<Animator>() && !go.GetComponent<Animation>())
                            {
                                GUILayout.Label(string.Format("The object to animate: \"{0}\"\nis not exporting anim clips to the Dcl code\nAdd an Animator or AnimatorDcl", go.name), warningStyle);
                                break;
                            }
                            else if (!go.GetComponent<AnimatorDcl>() && go.GetComponent<Animator>())
                            {
                                if (go.GetComponent<Animator>().runtimeAnimatorController == null)
                                {
                                    GUILayout.Label(string.Format("Animator without controller\nThe object to animate \"{0}\"\ndon't have an animator controller.", go.name), warningStyle);
                                    break;
                                }
                                else if (go.GetComponent<Animator>().runtimeAnimatorController.animationClips.Length <= 0)
                                {
                                    GUILayout.Label(string.Format("Animator controller without clips\nThe object to animate \"{0}\"\ndon't have clips.", go.name), warningStyle);
                                    break;
                                }

                                List<string> animatorAnims = new List<string>();
                                foreach (var dclAnim in go.GetComponent<Animator>().runtimeAnimatorController.animationClips)
                                {
                                    animatorAnims.Add(dclAnim.name);
                                }
                                if (animatorAnims.Count > 0)
                                {
                                    clipIndex.intValue = EditorGUILayout.Popup("Animation Clip", clipIndex.intValue, animatorAnims.ToArray());

                                    reset.boolValue = EditorGUILayout.Toggle("Reset", reset.boolValue);
                                    modifyClip.boolValue = EditorGUILayout.Toggle("Modify Clip params", modifyClip.boolValue);

                                    if (!modifyClip.boolValue) break;

                                    loop.boolValue = EditorGUILayout.Toggle("Loop", loop.boolValue);
                                    speed.floatValue = EditorGUILayout.FloatField("Speed", speed.floatValue);
                                }
                                //animClip.objectReferenceValue = EditorGUILayout.ObjectField("Animation Clip", animClip.objectReferenceValue, typeof(AnimationClip), true);
                                break;
                            }
                            else if (!go.GetComponent<AnimatorDcl>() && go.GetComponent<Animation>())
                            {
                                GUILayout.Label(string.Format("Animation component not supported\nThe object to animate \"{0}\"\nis not exporting anim clips to the Dcl code\nAdd an Animator or AnimatorDcl.", go.name), warningStyle);
                                break;
                            }
                            else if (go.GetComponent<AnimatorDcl>() && go.GetComponent<AnimatorDcl>().defaultAnimation.clip == null)
                            {
                                GUILayout.Label(string.Format("No clips found in the \"{0}\" AnimatorDcl\nSet the animation clips", go.name), warningStyle);
                                break;
                            }

                            List<string> anims = new List<string>();
                            anims.Add(go.GetComponent<AnimatorDcl>().defaultAnimation.clip.name);
                            foreach (var dclAnim in go.GetComponent<AnimatorDcl>().animations)
                            {
                                anims.Add(dclAnim.clip.name);
                            }

                            if (anims != null && anims.Count > 0)
                            {
                                clipIndex.intValue = EditorGUILayout.Popup("Animation Clip", clipIndex.intValue, anims.ToArray());

                                reset.boolValue = EditorGUILayout.Toggle("Reset", reset.boolValue);
                                modifyClip.boolValue = EditorGUILayout.Toggle("Modify Clip params", modifyClip.boolValue);

                                if (!modifyClip.boolValue) break;

                                loop.boolValue = EditorGUILayout.Toggle("Loop", loop.boolValue);
                                speed.floatValue = EditorGUILayout.FloatField("Speed", speed.floatValue);
                            }
                            ///
                            break;
                        case 2: //Stop Anim
                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Object to animate", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            break;
                        case 3: //Play Stop Audio
                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Audio Source Object", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            audioClip.objectReferenceValue = EditorGUILayout.ObjectField("Audio Clip", audioClip.objectReferenceValue, typeof(AudioClip), true);
                            loop.boolValue = EditorGUILayout.Toggle("Loop", loop.boolValue);
                            volume.floatValue = EditorGUILayout.Slider("Volume", volume.floatValue, 0, 1);
                            pitch.floatValue = EditorGUILayout.Slider("Pitch", pitch.floatValue, 0, 5);
                            break;
                        case 4: //Stop Audio
                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Audio Source Object", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            break;
                        case 5: //Set Transform
                            fixedPosition.boolValue = EditorGUILayout.Toggle("Fixed Values", fixedPosition.boolValue);
                            EditorGUILayout.Space();

                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Target GameObject", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            EditorGUILayout.Space();
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Position");
                            Pos.boolValue = EditorGUILayout.Toggle(Pos.boolValue);
                            GUILayout.Label("Rotation");
                            Rot.boolValue = EditorGUILayout.Toggle(Rot.boolValue);
                            GUILayout.Label("Scale");
                            Sca.boolValue = EditorGUILayout.Toggle(Sca.boolValue);
                            GUILayout.EndHorizontal();
                            EditorGUILayout.Space();

                            if (fixedPosition.boolValue)
                            {
                                if (Pos.boolValue)
                                    position.vector3Value = EditorGUILayout.Vector3Field("Position", position.vector3Value);
                                if (Rot.boolValue)
                                    rotation.vector3Value = EditorGUILayout.Vector3Field("Rotation", rotation.vector3Value);
                                if (Sca.boolValue)
                                    scale.vector3Value = EditorGUILayout.Vector3Field("Scale", scale.vector3Value);
                            }
                            else
                            {
                                if (Pos.boolValue || Rot.boolValue || Sca.boolValue)
                                    lookAt.objectReferenceValue = EditorGUILayout.ObjectField("Transform Reference", lookAt.objectReferenceValue, typeof(GameObject), true);
                            }
                            break;
                        case 6: //Add Transform
                            fixedPosition.boolValue = EditorGUILayout.Toggle("Fixed Values", fixedPosition.boolValue);
                            EditorGUILayout.Space();

                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Target GameObject", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            EditorGUILayout.Space();
                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Position");
                            Pos.boolValue = EditorGUILayout.Toggle(Pos.boolValue);
                            GUILayout.Label("Rotation");
                            Rot.boolValue = EditorGUILayout.Toggle(Rot.boolValue);
                            GUILayout.Label("Scale");
                            Sca.boolValue = EditorGUILayout.Toggle(Sca.boolValue);
                            GUILayout.EndHorizontal();
                            EditorGUILayout.Space();

                            if (fixedPosition.boolValue)
                            {
                                if (Pos.boolValue)
                                    position.vector3Value = EditorGUILayout.Vector3Field("Position", position.vector3Value);
                                if (Rot.boolValue)
                                    rotation.vector3Value = EditorGUILayout.Vector3Field("Rotation", rotation.vector3Value);
                                if (Sca.boolValue)
                                    scale.vector3Value = EditorGUILayout.Vector3Field("Scale", scale.vector3Value);
                            }
                            else
                            {
                                if (Pos.boolValue || Rot.boolValue || Sca.boolValue)
                                    lookAt.objectReferenceValue = EditorGUILayout.ObjectField("Transform Reference", lookAt.objectReferenceValue, typeof(GameObject), true);
                            }
                            break;
                    }
                    EditorGUILayout.Space();
                }
                GUILayout.EndVertical();
                if (GUILayout.Button("X", EditorStyles.miniButtonRight, GUILayout.Width(20)))
                {
                    listExit.DeleteArrayElementAtIndex(i);
                }
                GUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add Exit Event", EditorStyles.miniButtonRight, GUILayout.MinWidth(80)))
            {
                t.actionListExit.Add(new DclTriggerAction());
            }
            GUILayout.EndVertical();
            
            //Apply the changes to our list
            GetTarget.ApplyModifiedProperties();
        }
    }
}