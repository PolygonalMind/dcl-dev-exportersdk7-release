using UnityEngine;
using System;

[Serializable]
public class DclAnimation
{
    [SerializeField] public AnimationClip clip;
    [SerializeField] public bool loop;
}
    
[AddComponentMenu("Dcl Exporter ToolKit/AnimatorDcl")]
public class AnimatorDcl : MonoBehaviour
{
    [SerializeField] public bool startPlaying;
    [SerializeField] public DclAnimation defaultAnimation;
    [SerializeField] public DclAnimation[] animations;
}