#if UNITY_EDITOR

using System.IO;
using UnityEngine;
using UnityEditor;

namespace DCLExport
{
    public class ExporterGLTF20 : EditorWindow {

        // Fields limits
        const int NAME_LIMIT = 48;
        const int DESC_LIMIT = 1024;
        const int TAGS_LIMIT = 50;

        const int SPACE_SIZE = 5;
        Vector2 DESC_SIZE = new Vector2(512, 64);

        GameObject mExporterGo;
        SceneToGlTFWiz mExporter;
        string mExportPath = "";

        string mStatus = "";
        string mResult = "";
        GUIStyle mTextAreaStyle;
        GUIStyle mStatusStyle;

        private bool mExportAnimation = true;
        private bool mExportPBR = true;
        private bool mBuildZip = false;

        private string mParamName = "";
        private string mParamDescription = "";
        private string mParamTags = "";

        private enum ExportFormat
        {
            GLTFBin,
            GLBExternalTextures,
            GLBCompressedTextures
        }
        private ExportFormat mFormat = new ExportFormat();

        void OnEnable() {
            this.minSize = new Vector2(400, 512);

            if (mExporterGo == null) {
                mExporterGo = new GameObject("Exporter");
                mExporter = mExporterGo.AddComponent<SceneToGlTFWiz>();
                mExporterGo.hideFlags = HideFlags.HideAndDontSave;
            }
        }

        void OnDisable() {
            if (mExporterGo != null) {
                GameObject.DestroyImmediate(mExporterGo);
                mExporterGo = null;
            }

        }

        void OnSelectionChange() {
            updateExporterStatus();
            Repaint();
        }
        void OnInspectorUpdate()
        {
            Repaint();
        }
        void OnGUI() {


            if (mTextAreaStyle == null) {
                mTextAreaStyle = new GUIStyle(GUI.skin.textArea);
                mTextAreaStyle.fixedWidth = DESC_SIZE.x;
                mTextAreaStyle.fixedHeight = DESC_SIZE.y;
            }

            if (mStatusStyle == null) {
                mStatusStyle = new GUIStyle(EditorStyles.label);
                mStatusStyle.richText = true;
            }

            if (string.IsNullOrEmpty(DclExporter.gltfVersion))
            {
                GUILayout.Space(SPACE_SIZE);
                MissingDeps();
                return;
            }
            //Tryal Warning
            GUILayout.Space(SPACE_SIZE);
            GUILayout.Label("Warning", EditorStyles.boldLabel);
            GUILayout.Label("<color=#FFF00F>If you export different GLTF/GLB format types in the same folder\nsome files may get corrupted or lack some information or textures</color>", mStatusStyle);
            GUILayout.Space(SPACE_SIZE);
            
            // Export path
            GUILayout.Label("Export Path", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            mExportPath = EditorGUILayout.TextField(mExportPath);
            if (GUILayout.Button("Set Folder", GUILayout.Width(80), GUILayout.Height(16))) {
                mExportPath = EditorUtility.OpenFolderPanel("Export Path", mExportPath, "");
            }
            if (GUILayout.Button("Open Folder", GUILayout.Width(100), GUILayout.Height(16)))
            {
                Application.OpenURL(mExportPath);
            }
            GUILayout.EndHorizontal();

            // Model settings
            GUILayout.Label("Model properties", EditorStyles.boldLabel);

            // Model name
            GUILayout.Label("Name");
            mParamName = EditorGUILayout.TextField(mParamName);
            GUILayout.Label("(" + mParamName.Length + "/" + NAME_LIMIT + ")", EditorStyles.centeredGreyMiniLabel);
            EditorStyles.textField.wordWrap = true;
            GUILayout.Space(SPACE_SIZE);

            GUILayout.Label("Options", EditorStyles.boldLabel);
            GUILayout.BeginVertical();
            mFormat = (ExportFormat)EditorGUILayout.EnumPopup("Format", mFormat);
            mExportPBR = EditorGUILayout.Toggle("Export PBR Material", mExportPBR);
            mExportAnimation = EditorGUILayout.Toggle("Export animation", mExportAnimation);
            //mBuildZip = EditorGUILayout.Toggle("Build Zip", mBuildZip);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            
            GUILayout.Space(SPACE_SIZE);
            
            bool enable = updateExporterStatus();

            GUILayout.Label("Status", EditorStyles.boldLabel);

            if (enable)
                GUILayout.Label(string.Format("<color=#0FF00F>{0}</color>", mStatus), mStatusStyle);
            else
                GUILayout.Label(string.Format("<color=#F00F0FFF>{0}</color>", mStatus), mStatusStyle);

            if (mResult.Length > 0)
                GUILayout.Label(string.Format("<color=#0FF00F>{0}</color>", mResult), mStatusStyle);


            GUILayout.Space(SPACE_SIZE);
            GUI.enabled = enable;
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Export Selection", GUILayout.Width(220), GUILayout.Height(32))) {
                if (!enable) {
                    EditorUtility.DisplayDialog("Error", mStatus, "Ok");
                }
                else {
                    string exportFileName = Path.Combine(mExportPath, mParamName + ".gltf");
                    mExporter.ExportCoroutine(exportFileName, null, mBuildZip, mExportPBR, mExportAnimation);

                    switch (mFormat)
                    {
                        case ExportFormat.GLTFBin:
                            break;
                        case ExportFormat.GLBExternalTextures:
                            RunCommand.gltfGLBexternal(mExportPath, mParamName);
                            break;
                        case ExportFormat.GLBCompressedTextures:

                            RunCommand.gltfGLBcompress(mExportPath, mParamName); 
                            if (Directory.Exists(mExportPath + "/gltfTextures"))
                            {
                                Debug.Log("Delete: " + mExportPath + "gltfTextures");
                                UnityEditor.FileUtil.DeleteFileOrDirectory(mExportPath + "gltfTextures");
                            }
                            break;
                    }

                    mResult = string.Format("Export Finished: {0}", mExportPath);
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.Space(SPACE_SIZE * 2);
        }

        private bool updateExporterStatus() {
            mStatus = "";

            int nbSelectedObjects = Selection.GetTransforms(SelectionMode.Deep).Length;
            if (nbSelectedObjects == 0) {
                mStatus = "No object selected to export";
                return false;
            }

            if (mExportPath.Length == 0) {
                mStatus = "Please set export path";
                return false;
            }

            if (mParamName.Length > NAME_LIMIT) {
                mStatus = "Model name is too long";
                return false;
            }


            if (mParamName.Length == 0) {
                mStatus = "Please give a name to your model";
                return false;
            }


            if (mParamDescription.Length > DESC_LIMIT) {
                mStatus = "Model description is too long";
                return false;
            }


            if (mParamTags.Length > TAGS_LIMIT) {
                mStatus = "Model tags are too long";
                return false;
            }
            
            if(nbSelectedObjects > 1)
            {
                mStatus = "Export " + nbSelectedObjects + " objects, Please select only one";
                return false;
            }

            mStatus = "Export " + nbSelectedObjects + " object";
            return true;
        }

        [MenuItem("Dcl Exporter ToolKit/GLTF Exporter")]
        static void Init() {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX // edit: added Platform Dependent Compilation - win or osx standalone
            ExporterGLTF20 window = (ExporterGLTF20)EditorWindow.GetWindow(typeof(ExporterGLTF20));
            window.titleContent.text = "GLTF Exporter";
            window.Show();
#else // and error dialog if not standalone
		EditorUtility.DisplayDialog("Error", "Your build target must be set to standalone", "Okay");
#endif
        }
        private void MissingDeps()
        {
            GUILayout.Label("<color=#FFF00F>Gltf-Pipeline needed to export Gltf and Glb files</color>", mStatusStyle);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Install Gltf-Pipeline", GUILayout.Height(32)))
            {
                if (EditorUtility.DisplayDialog("Confirm to install Gltf-pipeline",
                    "Are you sure to install Gltf-pipeline?", "Yes", "No"))
                {
                    RunCommand.GltfPipeline();
                }
            }

            GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
            if (GUILayout.Button("Uninstall", GUILayout.Height(32), GUILayout.Width(100)))
            {
                RunCommand.UninstallGltfPipeline();
                DclExporter.gltfVersion = null;
                DclExporter.gltfFound = false;
            }
            GUI.backgroundColor = DclExporter.oriColor;
            GUILayout.EndHorizontal();

            GUILayout.Label("Check if all the dependencies are installed to start using the tool", mStatusStyle);

            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            if (GUILayout.Button("Check Dependencies", GUILayout.Height(32)))
            {
                RunCommand.DependencieCheck();
                ShowNotification(new GUIContent("Checking for Dependencies"), 0.5f);
            }
            GUI.backgroundColor = DclExporter.oriColor;
        }
    }
} 
#endif