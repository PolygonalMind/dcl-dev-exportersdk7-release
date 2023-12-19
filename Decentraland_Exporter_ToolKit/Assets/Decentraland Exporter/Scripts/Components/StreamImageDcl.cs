using UnityEngine;


[AddComponentMenu("Dcl Exporter ToolKit/Stream Image")]
public class StreamImageDcl : MonoBehaviour
{
    [Header("This component only works with Dcl primitives")]
    [Tooltip("Write here the image URL or the relative path to the image inside the Decentraland project folder")]
    [SerializeField] public string imageUrl = "images/image.jpg";
}
