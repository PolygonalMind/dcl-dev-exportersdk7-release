using DCLExport;
using UnityEngine;

[AddComponentMenu("Dcl Exporter ToolKit/Break Child")]
public class Break_Child : MonoBehaviour
{
    private void OnEnable()
    {
        this.GetComponent<DclObject>().dclNodeType = EDclNodeType.gltf_break;
    }
}
