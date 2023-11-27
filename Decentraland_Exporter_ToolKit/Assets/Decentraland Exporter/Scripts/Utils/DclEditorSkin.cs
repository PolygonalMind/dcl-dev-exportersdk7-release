using UnityEditor;
using UnityEngine;

namespace DCLExport
{
    public static class DclEditorSkin
    {
        public static Texture2D InfoIcon
        {
            get
            {
                return EditorGUIUtility.FindTexture("d_console.infoicon");
            }
        }
        public static Texture2D InfoIconSmall
        {
            get
            {
                return EditorGUIUtility.FindTexture("d_console.infoicon.sml");
            }
        }
        public static Texture2D WarningIcon
        {
            get
            {
                return EditorGUIUtility.FindTexture("d_console.warnicon");
            }
        }
        public static Texture2D WarningIconSmall
        {
            get
            {
                return EditorGUIUtility.FindTexture("d_console.warnicon.sml");
            }
        }
        public static Texture2D ErrorIcon
        {
            get
            {
                return EditorGUIUtility.FindTexture("d_console.erroricon");
            }
        }
        public static Texture2D ErrorIconSmall
        {
            get
            {
                return EditorGUIUtility.FindTexture("d_console.erroricon.sml");
            }
        }

        #region Shapes

        public static Texture2D Cube
        {
            get
            {
                return EditorGUIUtility.FindTexture("PreMatCube");
            }
        }
        public static Texture2D Quad
        {
            get
            {
                return EditorGUIUtility.FindTexture("PreMatQuad");
            }
        }
        public static Texture2D Sphere
        {
            get
            {
                return EditorGUIUtility.FindTexture("PreMatSphere");
            }
        }
        public static Texture2D Cylinder
        {
            get
            {
                return EditorGUIUtility.FindTexture("PreMatCylinder");
            }
        }
        public static Texture2D Torus
        {
            get { return EditorGUIUtility.FindTexture("PreMatTorus"); }
        }
        public static Texture2D Mesh
        {
            get { return EditorGUIUtility.IconContent("Mesh Icon").image as Texture2D; }
        }
        public static Texture2D PrefabModel
        {
            get
            {
                return EditorGUIUtility.FindTexture("PrefabModel Icon");
            }
        }
        #endregion


        #region Only In DCL Package

        private static Texture2D _entity;
        public static Texture2D Entity
        {
            get
            {
                if (!_entity)
                {
                    var internalFolder = FileUtil.FindFolder("Decentraland Exporter/Icons");
                    if (internalFolder.EndsWith("/"))
                        internalFolder = internalFolder.Remove(internalFolder.LastIndexOf("/"), 1);
                    _entity = (Texture2D)AssetDatabase.LoadAssetAtPath(
                        string.Format("{0}/entity.png", internalFolder), typeof(Texture2D));
                }
                return _entity;
            }
        }
        private static Texture2D _area;
        public static Texture2D Area
        {
            get
            {
                if (!_area)
                {
                    var internalFolder = FileUtil.FindFolder("Decentraland Exporter/Icons");
                    if (internalFolder.EndsWith("/"))
                        internalFolder = internalFolder.Remove(internalFolder.LastIndexOf("/"), 1);
                    _area = (Texture2D)AssetDatabase.LoadAssetAtPath(
                        string.Format("{0}/area.png", internalFolder), typeof(Texture2D));
                }
                return _area;
            }
        }
        private static Texture2D _ignore;
        public static Texture2D Ignore
        {
            get
            {
                if (!_ignore)
                {
                    var internalFolder = FileUtil.FindFolder("Decentraland Exporter/Icons");
                    if (internalFolder.EndsWith("/"))
                        internalFolder = internalFolder.Remove(internalFolder.LastIndexOf("/"), 1);
                    _ignore = (Texture2D)AssetDatabase.LoadAssetAtPath(
                        string.Format("{0}/ignore.png", internalFolder), typeof(Texture2D));
                }
                return _ignore;
            }
        }
        private static Texture2D _nft;
        public static Texture2D Nft
        {
            get
            {
                if (!_nft)
                {
                    var internalFolder = FileUtil.FindFolder("Decentraland Exporter/Icons");
                    if (internalFolder.EndsWith("/"))
                        internalFolder = internalFolder.Remove(internalFolder.LastIndexOf("/"), 1);
                    _nft = (Texture2D)AssetDatabase.LoadAssetAtPath(
                        string.Format("{0}/nft.png", internalFolder), typeof(Texture2D));
                }
                return _nft;
            }
        }
        private static Texture2D _cone;
        public static Texture2D Cone
        {
            get
            {
                if (!_cone)
                {
                    var internalFolder = FileUtil.FindFolder("Decentraland Exporter/Icons");
                    if (internalFolder.EndsWith("/"))
                        internalFolder = internalFolder.Remove(internalFolder.LastIndexOf("/"), 1);
                    _cone = (Texture2D) AssetDatabase.LoadAssetAtPath(
                        string.Format("{0}/cone.png", internalFolder), typeof(Texture2D));
                }
                return _cone;
            }
        }
        private static Texture2D _followup;
        public static Texture2D FollowUp
        {
            get
            {
                if (!_followup)
                {
                    var internalFolder = FileUtil.FindFolder("Decentraland Exporter/Icons");
                    if (internalFolder.EndsWith("/"))
                        internalFolder = internalFolder.Remove(internalFolder.LastIndexOf("/"), 1);
                    _followup = (Texture2D)AssetDatabase.LoadAssetAtPath(
                        string.Format("{0}/followup.png", internalFolder), typeof(Texture2D));
                }
                return _followup;
            }
        }
        private static Texture2D _text;
        public static Texture2D Text
        {
            get
            {
                if (!_text)
                {
                    var internalFolder = FileUtil.FindFolder("Decentraland Exporter/Icons");
                    if (internalFolder.EndsWith("/"))
                        internalFolder = internalFolder.Remove(internalFolder.LastIndexOf("/"), 1);
                    _text = (Texture2D)AssetDatabase.LoadAssetAtPath(
                        string.Format("{0}/text.png", internalFolder), typeof(Texture2D));
                }
                return _text;
            }
        }

        #endregion
    }
}