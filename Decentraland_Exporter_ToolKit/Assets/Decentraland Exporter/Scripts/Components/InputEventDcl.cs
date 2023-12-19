using UnityEngine;
using System;
using System.Collections.Generic; // Import the System.Collections.Generic class to give us access to List<>
using UnityEditor;

namespace DCLExport
{
    public enum ActionButtons
    {
        Click,
        E,
        F,
        Num1,
        Num2,
        Num3,
        Num4
    }
    public enum ActionType
    {
        OpenLink,
        TeleportInsideScene,
        TeleportToOtherScene,
        PlayAnimation,
        StopAnimations,
        PlayStopAudio,
        StopAudio
    }
    public enum EventType
    {
        PointerDown,
        PointerUp
    }
    
    //This is our custom class with our variables
    [Serializable]
    public class DclAction
    {
        public ActionButtons actionButton = new ActionButtons();
        public ActionType actionType = new ActionType();

        //General
        public GameObject refGO;
        //OpenLink
        public string link = "";
        //Tp inside scene
        public GameObject lookAt;
        public bool fixedPosition = false;
        public Vector3 teleportPosition;
        public Vector3 teleportLookAt;
        //Tp out scene
        public Vector2 teleportSceneCoords;
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

    [AddComponentMenu("Dcl Exporter ToolKit/InputEventDcl")]
    public class InputEventDcl : MonoBehaviour
    {
        //
        public EventType eventType = new EventType();

        public int maxDistance = 8;
        public string hoverText = "Interact";

        //List of actions for this event
        public List<DclAction> actionList = new List<DclAction>(1);
    }

    [CustomEditor(typeof(InputEventDcl))]

    public class CustomInputEditor : Editor
    {
        InputEventDcl t;
        SerializedObject GetTarget;
        SerializedProperty ActionList;
        int ListSize;
        internal static GUIStyle warningStyle = new GUIStyle();

        void OnEnable()
        {
            t = (InputEventDcl)target;
            GetTarget = new SerializedObject(t);
            ActionList = GetTarget.FindProperty("actionList"); // Find the List in our script and create a refrence of it

            warningStyle.normal.textColor = new Color(1f,0.4f,0.4f,1f);
            warningStyle.fontSize = 12;
        }

        public override void OnInspectorGUI()
        {
            //Type of event
            SerializedProperty eventType = GetTarget.FindProperty("eventType");
            SerializedProperty maxDistance = GetTarget.FindProperty("maxDistance");
            SerializedProperty hoverText = GetTarget.FindProperty("hoverText");

            //Update our list
            GetTarget.Update();

            //Choose how to display the list<> Example purposes only
            EditorGUILayout.Space();

            eventType.enumValueIndex = EditorGUILayout.Popup(eventType.enumValueIndex, eventType.enumDisplayNames);
            maxDistance.intValue = EditorGUILayout.IntField("Max Distance", maxDistance.intValue);
            hoverText.stringValue = EditorGUILayout.TextField("Hover Text", hoverText.stringValue);

            EditorGUILayout.Space(30);

            GUILayout.BeginVertical("Box");
            ListSize = ActionList.arraySize;
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Event List", EditorStyles.boldLabel);
            ListSize = EditorGUILayout.IntField(ListSize);
            GUILayout.EndHorizontal();

            if (ListSize != ActionList.arraySize)
            {
                while (ListSize > ActionList.arraySize)
                {
                    ActionList.InsertArrayElementAtIndex(ActionList.arraySize);
                }
                while (ListSize < ActionList.arraySize)
                {
                    ActionList.DeleteArrayElementAtIndex(ActionList.arraySize - 1);
                }
            }
            EditorGUILayout.Space();

            //Display our list to the inspector window

            for (int i = 0; i < ActionList.arraySize; i++)
            {
                SerializedProperty MyListRef = ActionList.GetArrayElementAtIndex(i);

                //Lists
                SerializedProperty actionType = MyListRef.FindPropertyRelative("actionType");
                SerializedProperty actionButton = MyListRef.FindPropertyRelative("actionButton");
                //OpenLink
                SerializedProperty link = MyListRef.FindPropertyRelative("link");
                //Tp inside scene
                SerializedProperty fixedPosition = MyListRef.FindPropertyRelative("fixedPosition");
                SerializedProperty teleportPosition = MyListRef.FindPropertyRelative("teleportPosition");
                SerializedProperty teleportLookAt = MyListRef.FindPropertyRelative("teleportLookAt");
                //Tp out scene
                SerializedProperty teleportSceneCoords = MyListRef.FindPropertyRelative("teleportSceneCoords");
                //Animation
                SerializedProperty modifyClip = MyListRef.FindPropertyRelative("modifyClipParams");
                SerializedProperty reset = MyListRef.FindPropertyRelative("reset");
                SerializedProperty speed = MyListRef.FindPropertyRelative("speed");
                SerializedProperty refGO = MyListRef.FindPropertyRelative("refGO");
                SerializedProperty lookAt = MyListRef.FindPropertyRelative("lookAt");
                //SerializedProperty animClip = MyListRef.FindPropertyRelative("animClip");
                SerializedProperty clipIndex = MyListRef.FindPropertyRelative("clipIndex");

                //Audio
                SerializedProperty audioClip = MyListRef.FindPropertyRelative("audioClip");
                SerializedProperty loop = MyListRef.FindPropertyRelative("loop");
                SerializedProperty volume = MyListRef.FindPropertyRelative("volume");
                SerializedProperty pitch = MyListRef.FindPropertyRelative("pitch");

                GUILayout.BeginHorizontal("Button");
                GUILayout.BeginVertical();
                var foldout = EditorUtil.GUILayout.AutoSavedFoldout("DclFoldInput" + i.ToString(), "Input " + i.ToString(), true, null);
                if (foldout)
                {
                    actionButton.enumValueIndex = EditorGUILayout.Popup("Button", actionButton.enumValueIndex, actionButton.enumDisplayNames);
                    actionType.enumValueIndex = EditorGUILayout.Popup("Action", actionType.enumValueIndex, actionType.enumDisplayNames);

                    EditorGUILayout.Space();

                    switch (actionType.enumValueIndex)
                    {
                        case 0: //Open Link
                            link.stringValue = EditorGUILayout.TextField("Link", link.stringValue);
                            break;
                        case 1: //Tp In
                            fixedPosition.boolValue = EditorGUILayout.Toggle("Fixed Position", fixedPosition.boolValue);
                            if (fixedPosition.boolValue)
                            {
                                teleportPosition.vector3Value = EditorGUILayout.Vector3Field("Position", teleportPosition.vector3Value);
                                teleportLookAt.vector3Value = EditorGUILayout.Vector3Field("Look at", teleportLookAt.vector3Value);
                            }else
                            {
                                GUILayout.BeginHorizontal();
                                refGO.objectReferenceValue = EditorGUILayout.ObjectField("GameObject Position", refGO.objectReferenceValue, typeof(GameObject), true);
                                if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                                {
                                    refGO.objectReferenceValue = t.gameObject;
                                }
                                GUILayout.EndHorizontal();

                                lookAt.objectReferenceValue = EditorGUILayout.ObjectField("Look at Position", lookAt.objectReferenceValue, typeof(GameObject), true);
                            }
                            break;
                        case 2: //Tp out
                            teleportSceneCoords.vector2Value = EditorGUILayout.Vector2Field("Scene Coords", teleportSceneCoords.vector2Value);
                            break;
                        case 3: //Play Anim
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
                                if(go.GetComponent<Animator>().runtimeAnimatorController == null)
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
                            foreach(var dclAnim in go.GetComponent<AnimatorDcl>().animations)
                            {
                                anims.Add(dclAnim.clip.name);
                            }
                            
                            if(anims != null && anims.Count > 0)
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
                        case 4: //Stop Anim
                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Object to animate", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            break;
                        case 5: //Play Stop Audio
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
                        case 6: //Stop Audio
                            GUILayout.BeginHorizontal();
                            refGO.objectReferenceValue = EditorGUILayout.ObjectField("Audio Source Object", refGO.objectReferenceValue, typeof(GameObject), true);
                            if (GUILayout.Button("Self", EditorStyles.miniButtonRight, GUILayout.Width(30)))
                            {
                                refGO.objectReferenceValue = t.gameObject;
                            }
                            GUILayout.EndHorizontal();
                            break;

                    }
                    EditorGUILayout.Space();
                }
                GUILayout.EndVertical();
                if (GUILayout.Button("X", EditorStyles.miniButtonRight, GUILayout.Width(20)))
                {
                    ActionList.DeleteArrayElementAtIndex(i);
                }
                GUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Add Input", EditorStyles.miniButtonRight, GUILayout.MinWidth(80)))
            {
                t.actionList.Add(new DclAction());
            }
            GUILayout.EndVertical();

            //Apply the changes to our list
            GetTarget.ApplyModifiedProperties();
        }
    }
}