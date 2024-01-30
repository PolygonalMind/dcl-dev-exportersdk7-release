using UnityEngine;

[AddComponentMenu("Dcl Exporter ToolKit/Stream Video")]
public class StreamingVideoDcl : MonoBehaviour
{
    [Header("This component only works with Dcl primitives")]
    [Tooltip("Write here the stream URL or the relative path to a video inside the Decentraland project folder")]
    [SerializeField] public string videoURL = "videos/clip.mp4";
    [SerializeField] public bool startPlaying = true;
    [SerializeField] public int clickDistance = 10;
}