using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using Object = UnityEngine.Object;

namespace DCLExport
{
    public enum EDclNodeType
    {
        _none,
        ignore,
        entity,
        area,
        box,
        sphere,
        plane,
        cylinder,
        cone,
        circle,
        text,
        gltf,
        ChildOfGLTF,
        nft,
        gltf_forced,
        gltf_break,
    }
    public enum FontStyles
    {
        SansSerif,
        SansSerif_Heavy,
        SansSerif_Semibold,
        LiberationSans
    }

    public class ResourceRecorder
    {
        //One list per import   MATH --> @dcl/sdk/math  ECS --> @dcl/sdk/math  RA --> ~system/RestrictedActions
        public List<string> importedModulesECS = new List<string>(1);
        public List<string> importedModulesMATH = new List<string>(1);
        public List<string> importedModulesRA = new List<string>(1);
        public bool blateLoadInit = false;
        public string lateGTLFlines = "";
        public List<string> exportedModels = new List<string>();
        public List<string> exportedModelsFileName = new List<string>();
        public List<GameObject> meshesToExport;
        public List<GameObject> primitivesToExport;
        public List<Material> primitiveMaterialsToExport;
        public List<Texture> primitiveTexturesToExport;
        public List<Texture> gltfTextures;
        public List<AudioClip> audioClipsToExport;
        public List<TMP_FontAsset> fontToExport;
        public List<string> audioSourceAddFunctions = new List<string>();
        public List<string> dependences = new List<string>();

        public ResourceRecorder()
        {
            meshesToExport = new List<GameObject>();
            primitivesToExport = new List<GameObject>();
            primitiveMaterialsToExport = new List<Material>();
            primitiveTexturesToExport = new List<Texture>();
            gltfTextures = new List<Texture>();
            audioClipsToExport = new List<AudioClip>();
            fontToExport = new List<TMP_FontAsset>();

        }
    }

    public static class SceneTraverserUtils
    {

        public static void ParseTextToCoordinates(string text, List<ParcelCoordinates> coordinates)
        {
            coordinates.Clear();
            var lines = text.Replace("\r", "").Split('\n');
            foreach (var line in lines)
            {
                var elements = line.Trim().Split(',');
                if (elements.Length == 0) continue;
                if (elements.Length != 2)
                {
                    throw new Exception("A line does not have exactly 2 elements!");
                }

                var x = int.Parse(elements[0]);
                var y = int.Parse(elements[1]);
                coordinates.Add(new ParcelCoordinates(x, y));
            }
        }
        public static StringBuilder ParcelToStringBuilder(ParcelCoordinates parcel)
        {
            return new StringBuilder().Append(parcel.x).Append(',').Append(parcel.y);
        }

        public static StringBuilder WarningToStringBuilder(string warning)
        {
            return new StringBuilder().Append(warning);
        }

        public static string ToJsColor3Ctor(Color color)
        {
            return string.Format("Color3.create({0}, {1}, {2})", FloatToString(color.r), FloatToString(color.g), FloatToString(color.b));
        }
        public static string ToJsColor4Ctor(Color color)
        {
            return string.Format("Color4.create({0}, {1}, {2}, {3})", FloatToString(color.r), FloatToString(color.g), FloatToString(color.b), FloatToString(color.a));
        }

        public static string GetIdentityName(GameObject go)
        {
            if (go.GetComponent<DclObject>().dclName != BuildIdentityName(go))
            {
                if (string.IsNullOrEmpty(go.GetComponent<DclObject>().dclName))
                    go.GetComponent<DclObject>().dclName = BuildIdentityName(go);
                return go.GetComponent<DclObject>().dclName;
            }
            else
                return BuildIdentityName(go);

        }
        public static string FloatToString(float f)
        {
            return f.ToString().Replace(",", ".");
        }
        public static string FormatEntityNameString(string tag)
        {
            string newTag = tag;
            newTag = newTag.Replace(":", "_");
            newTag = newTag.Replace(".", "_");
            newTag = newTag.Replace("*", "_");
            newTag = newTag.Replace(",", "_");
            newTag = newTag.Replace("\"", "_");
            newTag = newTag.Replace(" ", "_");
            newTag = newTag.Replace("(", "_");
            newTag = newTag.Replace(")", "_");

            return newTag;
        }
        public static string BuildIdentityName(GameObject go)
        {
            string entityName;
            entityName = go.name;

            int entityCount = 0;
            int entityId = 0;
            int sceneId = 0;
            foreach (GameObject aux in Object.FindObjectsOfType(typeof(GameObject)))
            {
                if (go == aux)
                {
                    entityId += entityCount;
                }
                if (string.Equals(go.name,aux.name))
                {
                    entityCount++;
                }
                
            }

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                if(SceneManager.GetSceneAt(i).GetRootGameObjects().Contains(go))
                {
                    sceneId = i;
                    break;
                }
            }

            entityName = "s" + sceneId + "_" + entityName + "_";
            if ((entityCount - entityId) < 100)
                entityName += (entityCount - entityId).ToString("00");
            else
                entityName += (entityCount - entityId).ToString();


            entityName = FormatEntityNameString(entityName);
            
            return entityName;
        }

        public static string GetTextureRelativePath(Texture texture)
        {
            var relPath = AssetDatabase.GetAssetPath(texture);
            if (string.IsNullOrEmpty(relPath))
            {
                //this is a built-in asset
                relPath = texture.name + ".png";
            }
            else if(relPath.Contains(".tif"))
            {
                relPath = relPath.Replace(".tif", ".png");
            }

            string str = "unity_assets/" + relPath;
            return str;
        }

        public static string GetAudioClipRelativePath(AudioClip audioClip)
        {
            var relPath = AssetDatabase.GetAssetPath(audioClip);
            var relName = Path.GetFileName(relPath);
            if (string.IsNullOrEmpty(relPath))
            {
                //this is a built-in asset
                Debug.LogError("AudioClip should not be built-in assets!");
            }
            
            string str = "unity_assets/AudioClips/" + relName;
            return str;
        }
        public static string Vector3ToJSONString(Vector3 v)
        {
            return string.Format("{{x:{0},y:{1},z:{2}}}", FloatToString(v.x), FloatToString(v.y), FloatToString(v.z));
        }
        public static string QuaternionToJSONString(Quaternion q)
        {
            return string.Format("{{x:{0},y:{1},z:{2},w:{3}}}", FloatToString(q.x), FloatToString(q.y), FloatToString(q.z), FloatToString(q.w));
        }
        public static string Vector2ToJSONString(Vector2 v)
        {
            return string.Format("{{x:{0},y:{1}}}", FloatToString(v.x), FloatToString(v.y));
        }

        /// <summary>
        /// Color to HEX string(e.g. #AAAAAA)
        /// </summary>
        public static string ToHexString(Color color)
        {
            var color256 = (Color32)color;
            return String.Format("#{0:X2}{1:X2}{2:X2}", color256.r, color256.g, color256.b);
        }

        public static string BoolToString(bool b)
        {
            return b ? "true" : "false";
        }

        public static string ParcelToString(ParcelCoordinates parcel)
        {
            return string.Format("\"{0},{1}\"", parcel.x, parcel.y);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="texture"></param>
        /// <returns>false if </returns>
        static void CheckTextureValidity(Texture texture, SceneWarningRecorder warningRecorder)
        {
            if (!IsTextureSizeValid(texture.width) || !IsTextureSizeValid(texture.height))
            {
                warningRecorder.InvalidTextureWarnings.Add(new SceneWarningRecorder.InvalidTexture(texture));
            }
        }

        static bool IsTextureSizeValid(int x)
        {
            if ((x != 0) && ((x & (x - 1)) == 0))
            {
                if (1 <= x && x <= 512)
                {
                    return true;
                }
            }

            return false;
        }

        public static StringBuilder Prepend(this StringBuilder sb, string content)
        {
            return sb.Insert(0, content);
        }
        public static string EscapeString(string text)
        {
            string aux = text;
            aux = aux.Replace("\'", "\\\'");
            aux = aux.Replace("\"", "\\\"");
            aux = aux.Replace("\r\n", "\\n");
            aux = aux.Replace("\n", "\\n");
            return aux;
        }
        public static void ImportModuleECS(ResourceRecorder res, string index)
        {
            if (res.importedModulesECS.IndexOf(index) < 0)
                res.importedModulesECS.Add(index);
        }
        public static void ImportModuleMath(ResourceRecorder res, string index)
        {
            if (res.importedModulesMATH.IndexOf(index) < 0)
                res.importedModulesMATH.Add(index);
        }
        public static void ImportModuleRA(ResourceRecorder res, string index)
        {
            if (res.importedModulesRA.IndexOf(index) < 0)
                res.importedModulesRA.Add(index);
        }
        public static void ImportDclModules(ResourceRecorder _resourceRecorder, StringBuilder exportStr)
        {
            // RestrictedActions
            var modulesRA = _resourceRecorder.importedModulesRA.ToArray();
            if (modulesRA.Length > 0)
            {
                string modules = "";
                for (int i = 0; i < modulesRA.Length; i++)
                {
                    modules += modulesRA[i];
                    if (i < modulesRA.Length - 1)
                    {
                        modules += ", ";
                    }

                }
                exportStr.Prepend("import { " + modules + " } from \"~system/RestrictedActions\"\n");
            }
            // @dcl/sdk/math
            var modulesMATH = _resourceRecorder.importedModulesMATH.ToArray();
            if (modulesMATH.Length > 0)
            {
                string modules = "";
                for (int i = 0; i < modulesMATH.Length; i++)
                {
                    modules += modulesMATH[i];
                    if (i < modulesMATH.Length - 1)
                    {
                        modules += ", ";
                    }

                }
                exportStr.Prepend("import { " + modules + " } from \"@dcl/sdk/math\"\n");
            }
            // @dcl/sdk/ecs modules
            var modulesECS = _resourceRecorder.importedModulesECS.ToArray();
            if (modulesECS.Length > 0)
            {
                string modules = "";
                for (int i = 0; i < modulesECS.Length; i++)
                {
                    modules += modulesECS[i];
                    if (i < modulesECS.Length - 1)
                    {
                        modules += ", ";
                    }

                }
                exportStr.Prepend("import { " + modules + " } from \"@dcl/sdk/ecs\"\n");
            }
        }
    }
}
