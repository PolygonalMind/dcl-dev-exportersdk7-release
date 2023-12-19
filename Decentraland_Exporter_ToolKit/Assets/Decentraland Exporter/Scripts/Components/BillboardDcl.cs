using System;
using UnityEngine;


namespace DCLExport
{
    [Serializable]
    public enum BillboardMode
    {
        everyAxis,
        axisX,
        axisY,
        axisZ,
        none
    }

    [AddComponentMenu("Dcl Exporter ToolKit/Billboard")]
    public class BillboardDcl : MonoBehaviour
    {
        [SerializeField]
        public BillboardMode billboardMode = new BillboardMode();
    }
}
