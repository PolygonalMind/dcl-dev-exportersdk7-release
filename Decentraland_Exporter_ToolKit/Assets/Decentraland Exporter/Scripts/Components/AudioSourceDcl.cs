using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[AddComponentMenu("Dcl Exporter ToolKit/AudioSourceDcl")]
public class AudioSourceDcl : MonoBehaviour
{
    [HideInInspector] public AudioClip defaultClip;
    [SerializeField] public AudioClip[] clipsToExport;
    [HideInInspector] public bool playOnAwake;
    [HideInInspector] public bool loop;
    [Range(0.0F, 1.0F)]
    [HideInInspector] public float volume = 1;
    [Range(-3.0F, 3.0F)]
    [HideInInspector] public float pitch = 1;
}

[CustomEditor(typeof(AudioSourceDcl))]
public class AudioClipEditor : Editor
{

    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        AudioSourceDcl myHandler = target as AudioSourceDcl;

        AudioClip defaultClip = myHandler.defaultClip;
        bool playOnAwake = myHandler.playOnAwake;
        bool loop = myHandler.loop;
        float volume = myHandler.volume;
        float pitch = myHandler.pitch;

        EditorGUILayout.Space();
        GUILayout.BeginVertical("Box");
        defaultClip = (AudioClip)EditorGUILayout.ObjectField("Default Clip", defaultClip, typeof(AudioClip), true);

        loop = EditorGUILayout.Toggle("Loop", loop);
        
        volume = EditorGUILayout.Slider("Volume", volume, 0f, 1f);
        
        pitch = EditorGUILayout.Slider("Pitch", pitch, 0f, 5f);
        
        playOnAwake = EditorGUILayout.Toggle("Play On Awake?", playOnAwake);
        GUILayout.EndVertical();

        myHandler.defaultClip = defaultClip;
        myHandler.playOnAwake = playOnAwake;
        myHandler.loop = loop;
        myHandler.volume = volume;
        myHandler.pitch = pitch;
    }
}