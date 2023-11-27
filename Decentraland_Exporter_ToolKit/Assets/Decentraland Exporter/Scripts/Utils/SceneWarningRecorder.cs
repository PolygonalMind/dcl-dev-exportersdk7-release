using System.Collections.Generic;
using UnityEngine;

namespace DCLExport
{
    public class SceneWarningRecorder
    {
        //parcels
        public class OutOfLand
        {
            public Renderer renderer;

            public OutOfLand(Renderer renderer)
            {
                this.renderer = renderer;
            }
        }
        public class OutOfLandArea
        {
            public GameObject renderer;

            public OutOfLandArea(GameObject renderer)
            {
                this.renderer = renderer;
            }
        }
        //Shader
        public class UnsupportedShader
        {
            public Material renderer;

            public UnsupportedShader(Material renderer)
            {
                this.renderer = renderer;
            }
        }
        
        public class InvalidTexture
        {
            public Texture renderer;

            public InvalidTexture(Texture renderer)
            {
                this.renderer = renderer;
            }
        }
        public class InvalidNaming
        {
            public GameObject renderer;

            public InvalidNaming(GameObject renderer)
            {
                this.renderer = renderer;
            }
        }

        public readonly List<OutOfLandArea> AreaOutOfLandWarnings = new List<OutOfLandArea>();
        public readonly List<OutOfLand> OutOfLandWarnings = new List<OutOfLand>();
        public readonly List<OutOfLand> OutOfHeightLandWarnings = new List<OutOfLand>();
        public readonly List<OutOfLandArea> AreaOutOfHeightLandWarnings = new List<OutOfLandArea>();
        public readonly List<UnsupportedShader> UnsupportedShaderWarnings = new List<UnsupportedShader>();
        public readonly List<InvalidTexture> InvalidTextureWarnings = new List<InvalidTexture>();
        public readonly List<InvalidNaming> InvalidNamingWarnings = new List<InvalidNaming>();

        //单次---
        //6种指标超出
    }
}