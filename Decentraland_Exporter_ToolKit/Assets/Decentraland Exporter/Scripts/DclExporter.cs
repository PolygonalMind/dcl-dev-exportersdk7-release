using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace DCLExport
{
    public class DclExporter : EditorWindow
    {
        #region Variables

        //private string projectName;
        private string exportPath;
        private string newExportPath;
        internal static Color oriColor;
        public static string nodeVersion;
        public static string npmVersion;
        public static string gltfVersion;
        public static string dclCliVersion;
        public static bool nodeFound;
        public static bool npmFound;
        public static bool gltfFound;
        public static bool dclCliFound;

        private static Vector2 scrollPosition;
        
        private bool dclProjectExist;

        //Export function
        private DateTime initialTime;
        public bool exportAditionalCode = true;
        public enum ExportFormat
        {
            GLBCompressedTextures,
            GLTFExternalTextures,
            GLBExternalTextures
        }
        public ExportFormat mFormat = new ExportFormat();
        public static bool GlbExternalTexture = false;

        [SerializeField] public bool updateTrasverser;
        [SerializeField] public float timeUpdateTrasverser;

        private List<string> autoFindFolders = new List<string>();

        internal static GUIStyle warningStyle = new GUIStyle();
        private static GUIStyle labelStyle = new GUIStyle();

        private DclSceneMeta sceneMeta;
        private SerializedObject so;
        SerializedProperty customCode;
        SerializedProperty sceneSpawnPoints;
        private List<string> popupOptions = new List<string>();

        const int SPACE = 5;
        #endregion

        public static DclExporter Instance
        {
            get { return GetWindow<DclExporter>(); }
        }
        [MenuItem("Dcl Exporter ToolKit/Dcl Scene Exporter (Main Tool)", false, 100)]
        static void Init()
        {
            var window = GetWindow<DclExporter>();
            window.minSize = new Vector2(500, 400);
            window.maxSize = new Vector2(500, 1000);
            window.titleContent = new GUIContent("Decentraland Scene Exporter");
            window.Show();

            window.ShowNotification(new GUIContent("Checking for Dependencies"), 0.5f);

            RunCommand.DependencieCheck();
        }
        private void SetUp()
        {
            oriColor = GUI.backgroundColor;

            warningStyle.normal.textColor = Color.yellow;
            warningStyle.alignment = TextAnchor.UpperCenter;
            warningStyle.fontSize = 18;

            labelStyle.normal.textColor = Color.white;
            labelStyle.alignment = TextAnchor.UpperLeft;
            
            if (!sceneMeta)
            {
                CheckAndGetDclSceneMetaObject();
            }
            so = new SerializedObject(FindObjectOfType<DclSceneMeta>());
            customCode = so.FindProperty("customCode");
            sceneSpawnPoints = so.FindProperty("spawnPoints");
        }
        void OnInspectorUpdate()
        {
            dclProjectExist = Directory.Exists(exportPath + "/src") && File.Exists(exportPath + "/scene.json") && File.Exists(exportPath + "/package-lock.json");
            Repaint();
        }
        void OnGUI()
        {
            SetUp();

            exportPath = EditorPrefs.GetString("DclExportPath");

            GUILayout.Space(SPACE);
            
            if (string.IsNullOrEmpty(nodeVersion) || string.IsNullOrEmpty(npmVersion) || string.IsNullOrEmpty(gltfVersion) || string.IsNullOrEmpty(dclCliVersion))
            {
                MisingDependenciesGUI();

                return;
            }

            DependenciesGUI();

            GUILayout.Space(SPACE);
            
            DclPathGUI();

            GUILayout.Space(SPACE);
            
            if (!Directory.Exists(exportPath))
            {
                ShowNotification(new GUIContent("You need to select a valid Decentraland project folder"), 0.1f);
                return;
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            DclCliGUI();

            GUILayout.Space(SPACE);

            DclProjectGUI();

            GUILayout.Space(SPACE);

            StatGUI();

            GUILayout.Space(SPACE);

            SceneDataGUI();

            GUILayout.Space(SPACE);

            EditorGUILayout.EndScrollView();
            
            GUILayout.FlexibleSpace();

            ExportButtons();
        }
        private void ExportButtons()
        {
            if (!dclProjectExist) return;
            
            EditorGUILayout.BeginVertical("box");
            GUI.backgroundColor = new Color(1f, 0.8f, 1f);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Export 3D Models", GUILayout.Height(26)))
            {
                Export(false, true, false);
            }
            if (GUILayout.Button("Export Scripts", GUILayout.Height(26)))
            {
                Export(true, false, false);
            }
            if (GUILayout.Button("Export Metadata", GUILayout.Height(26)))
            {
                ExportSceneJson();
            }
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(SPACE);
            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            if (GUILayout.Button("Export", GUILayout.Height(32)))
            {
                Export();
            }
            GUI.backgroundColor = oriColor;
            EditorGUILayout.EndVertical();
        }
        private void MisingDependenciesGUI()
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Dependencies:\nNodeJS, Npm, Gltf-Pipeline and Dcl CLI\nNeeded to run this package correctly", warningStyle);
            EditorGUILayout.EndVertical();

            GUILayout.Space(SPACE);

            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("1.- Download and install NodeJS if you don't have it\n Npm will be installed automatically with the NodeJS installer", labelStyle);
            
            if (GUILayout.Button("Download NodeJS Installer", GUILayout.Height(32)))
            {
                if (EditorUtility.DisplayDialog("Confirm to open NodeJS website",
                    "This will open NodeJS website in your browser\nAre you sure?", "Yes", "No"))
                {
                    Application.OpenURL("https://nodejs.org");
                }
            }
            EditorGUILayout.EndVertical();

            GUILayout.Space(SPACE);

            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("2.- Install Gltf-Pipeline if you don't have it already (NodeJS & Npm needed)", labelStyle);
            
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
                gltfVersion = null;
                gltfFound = false;
            }
            GUI.backgroundColor = oriColor;
            GUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            GUILayout.Space(SPACE);

            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("3.- Install Dcl CLI if you don't have it already (NodeJS & Npm needed)", labelStyle);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Install Dcl CLI", GUILayout.Height(32)))
            {
                if (EditorUtility.DisplayDialog("Confirm to install Dcl CLI",
                    "Are you sure to install Dcl CLI?", "Yes", "No"))
                {
                    RunCommand.DclUpdateCLI(exportPath);
                }
            }

            GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
            if (GUILayout.Button("Uninstall", GUILayout.Height(32), GUILayout.Width(100)))
            {
                RunCommand.DclUninstallCLI(exportPath);
                dclCliVersion = null;
                dclCliFound = false;
            }
            GUI.backgroundColor = oriColor;
            GUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            
            GUILayout.Space(SPACE * 2);

            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("4.- Check if all the dependencies are installed to start using the tool", labelStyle);

            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            if (GUILayout.Button("Check Dependencies", GUILayout.Height(32)))
            {
                RunCommand.DependencieCheck();
                ShowNotification(new GUIContent("Checking for Dependencies"), 0.5f);
            }
            GUI.backgroundColor = oriColor;
            EditorGUILayout.EndVertical();
        }
        private void DependenciesGUI()
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.BeginHorizontal();
            GUILayout.Label(string.Format("NodeJS: <color=#A8EDA8>{0}</color>", nodeVersion), labelStyle);
            GUILayout.Space(SPACE);
            GUILayout.Label(string.Format("Npm: <color=#A8EDA8>{0}</color>", npmVersion), labelStyle);
            GUILayout.Space(SPACE);
            GUILayout.Label(string.Format("Gltf-Pipeline: <color=#A8EDA8>{0}</color>", gltfVersion), labelStyle);
            GUILayout.Space(SPACE);

            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            if (GUILayout.Button("Check", GUILayout.Height(16), GUILayout.Width(80)))
            {
                RunCommand.DependencieCheck();
            }
            
            GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
            if (GUILayout.Button("Clear", GUILayout.Height(16), GUILayout.Width(80)))
            {
                clearDeps();
            }
            GUI.backgroundColor = oriColor;
            GUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        private void DclPathGUI()
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label(string.Format("Dcl Project Path:   <color=#A8EDA8>{0}</color>", exportPath), labelStyle, GUILayout.Width(100));

            GUILayout.BeginHorizontal();
            newExportPath = EditorGUILayout.TextField(exportPath);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Wipe Path", GUILayout.Height(24)))
            {
                newExportPath = "";
            }
            if (GUILayout.Button("Set Folder", GUILayout.Height(24)))
            {
                newExportPath = EditorUtility.OpenFolderPanel("Select the Decentraland project folder", exportPath, "");
                if (string.IsNullOrEmpty(newExportPath)) newExportPath = exportPath;
            }
            //Check if the project contains /src and scene.json to avoid errors deleting an incorrect folder, this way only can be deleted if its a Dcl project initialiced
            if (dclProjectExist)
            {
                GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
                if (GUILayout.Button("Delete Content", GUILayout.Height(24), GUILayout.Width(100)))
                {
                    if (EditorUtility.DisplayDialog("Confirm to delete Dcl project directory content",
                        string.Format("Are you sure to delete all the Dcl project directory content?\n{0}", exportPath), "Yes", "No"))
                    {
                        RunCommand.DclDeleteFolderContent(exportPath);
                        ShowNotification(new GUIContent("Deleting Dcl project folder content"), 0.5f);
                    }
                }
                GUI.backgroundColor = oriColor;
            }

            if (newExportPath != exportPath)
            {
                exportPath = newExportPath;
                EditorPrefs.SetString("DclExportPath", newExportPath);
            }
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        private void DclCliGUI()
        {

            EditorGUILayout.BeginVertical("box");

            GUILayout.Label(string.Format("Decentraland CLI:   <color=#A8EDA8>{0}</color>", dclCliVersion), labelStyle);
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = new Color(1f, 0.8f, 1f);
            if (GUILayout.Button("Init Dcl Project", GUILayout.Height(32)))
            {
                if (Directory.GetFiles(exportPath).Length != 0)
                {
                    if (EditorUtility.DisplayDialog("Confirm to init Decentraland",
                        string.Format("This project folder is not empty\nAre you sure to init a dcl project in this path?\n{0}", exportPath), "Yes", "No"))
                    {
                        RunCommand.DclInit(exportPath);
                    }
                }
                else
                {
                    if (EditorUtility.DisplayDialog("Confirm to init Decentraland",
                        string.Format("Are you sure to init Decentraland in this path?\n{0}", exportPath), "Yes", "No"))
                    {
                        RunCommand.DclInit(exportPath);
                    }
                }
            }
            GUI.backgroundColor = oriColor;
            if (GUILayout.Button("Update CLI", GUILayout.Height(32)))
            {
                if (EditorUtility.DisplayDialog("Confirm to update the Dcl CLI",
                    "Are you sure to update the Dcl CLI?", "Yes", "No"))
                {
                    RunCommand.DclUpdateCLI(exportPath);
                }
            }
            if (dclProjectExist)
            {
                if (GUILayout.Button("Update Dcl SDK", GUILayout.Height(32)))
                {
                    if (EditorUtility.DisplayDialog("Confirm to update the Dcl SDK",
                        string.Format("Are you sure to update the dcl SDK in this path?\n{0}", exportPath), "Yes", "No"))
                    {
                        RunCommand.DclUpdateSdk(exportPath);
                    }
                } 
            }
            GUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        void StatGUI()
        {
            if (!dclProjectExist) return;
            
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();
            var foldout = EditorUtil.GUILayout.AutoSavedFoldout("DclFoldStat", "Statistics", true, null);

            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel = 1;
            if (foldout)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical(GUILayout.MinWidth(200));
                EditorGUILayout.Space(SPACE);
                if (GUILayout.Button("Refresh", GUILayout.MinHeight(25)))
                {
                    sceneMeta.RefreshStatistics();
                    sceneMeta.getParcelSetVolumes();
                }
                EditorGUILayout.Space(SPACE);
                updateTrasverser = EditorGUILayout.ToggleLeft("Autorefresh", updateTrasverser, GUILayout.MaxWidth(100));
                EditorGUILayout.Space(5);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space(0);
                GUILayout.Label("Refresh Interval:", GUILayout.MaxWidth(100));
                timeUpdateTrasverser = EditorGUILayout.FloatField(timeUpdateTrasverser, GUILayout.MaxWidth(60));
                GUILayout.Label("s");
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space(10);
                EditorGUILayout.BeginVertical();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Keep these numbers smaller than the righty", EditorStyles.centeredGreyMiniLabel);
                EditorGUILayout.EndHorizontal();
                var n = sceneMeta.parcels.Count;
                var sceneStatistics = sceneMeta.sceneStatistics;
                StatisticsLineGUI("Triangles", sceneStatistics.triangleCount, LimitationConfigs.GetMaxTriangles(n));
                StatisticsLineGUI("Bodies", sceneStatistics.bodyCount, LimitationConfigs.GetMaxBodies(n));
                StatisticsLineGUI("Entities", sceneStatistics.entityCount, LimitationConfigs.GetMaxTriangles(n));
                StatisticsLineGUI("Materials", (long)sceneStatistics.materialCount, LimitationConfigs.GetMaxMaterials(n));
                StatisticsLineGUI("Textures", (long)sceneStatistics.textureCount, LimitationConfigs.GetMaxTextures(n));
                StatisticsLineGUI("Height", (long)sceneStatistics.maxHeight, LimitationConfigs.GetMaxHeight(n));
                EditorGUILayout.LabelField("GLTFs", sceneStatistics.gltfCount.ToString());
                EditorGUILayout.LabelField("DCL Primitives", sceneStatistics.primitiveCount.ToString());
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndVertical();

                EditorGUILayout.Space(SPACE);
                mFormat = (ExportFormat)EditorGUILayout.EnumPopup("Gltf Type:", mFormat);
                EditorGUILayout.Space(SPACE);
            }

            WarningsGUI();
            EditorGUI.indentLevel = 0;
            EditorGUILayout.EndHorizontal();
        }
        private void DclProjectGUI()
        {
            if (!dclProjectExist) return;

            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Decentraland Project: ", labelStyle);
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Open Folder", GUILayout.Height(32)))
            {
                Application.OpenURL(exportPath);
            }
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Open scene.json", GUILayout.Height(32)))
            {
                Application.OpenURL(exportPath + "/scene.json");
            }
            if (GUILayout.Button("Open package.json", GUILayout.Height(32)))
            {
                Application.OpenURL(exportPath + "/package.json");
            }
            if (GUILayout.Button("Open index.ts", GUILayout.Height(32)))
            {
                Application.OpenURL(exportPath + "/src/index.ts");
            }

            GUILayout.EndHorizontal();

            GUILayout.Space(SPACE);

            GUILayout.BeginHorizontal();
            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            if (GUILayout.Button("Run Dcl", GUILayout.Height(32)))
            {
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Run Local"), false, RunMenu, "");
                menu.AddItem(new GUIContent("Run Local No Debug"), false, RunMenu, " --no-debug");
                menu.AddItem(new GUIContent("Run Web3"), false, RunMenu, " --web3");
                menu.AddItem(new GUIContent("Run Web3 No Debug"), false, RunMenu, " --web3 --no-debug");
                menu.ShowAsContext();
            }
            GUI.backgroundColor = oriColor;
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        private void SceneDataGUI()
        {
            if (!dclProjectExist) return;
            
            EditorGUILayout.BeginVertical("box");

            var oriFoldout = EditorPrefs.GetBool("DclBoldOwner");
            var foldout = EditorGUILayout.Foldout(oriFoldout, "Scene Metadata", true);
            if (foldout)
            {
                EditorGUI.indentLevel = 1;

                if (GUILayout.Button("Open Parcel Manager", GUILayout.Height(16)))
                {
                    ParcelManager pm = GetWindow<ParcelManager>();
                    pm.titleContent = new GUIContent("ParcelManager");
                    pm.minSize = new Vector2(325, 400);
                    pm.maxSize = new Vector2(325, 800);
                    pm.Show();
                }
                EditorGUILayout.LabelField("Land General Info", EditorStyles.boldLabel);
                sceneMeta.landTitle = EditorGUILayout.TextField("Land Name", sceneMeta.landTitle); // title of the land
                EditorGUILayout.LabelField("Land Description");
                GUIStyle style = new GUIStyle(EditorStyles.textArea);
                style.wordWrap = true;
                sceneMeta.landInfo = EditorGUILayout.TextArea(sceneMeta.landInfo, style);
                sceneMeta.landImg = EditorGUILayout.TextField("Land Thumbnail", sceneMeta.landImg); // url or uri to a jpg containing the thumbnail to be seen from the Atlas

                EditorGUILayout.LabelField("Land Owner Info", EditorStyles.boldLabel);
                sceneMeta.ethAddress = EditorGUILayout.TextField("Address", sceneMeta.ethAddress); //"owner" stands for ETH Address holding this LAND
                sceneMeta.contactName = EditorGUILayout.TextField("Name", sceneMeta.contactName); // Name of the owner
                sceneMeta.email = EditorGUILayout.TextField("Email", sceneMeta.email); // Email or contact form of the owner
                
                EditorGUILayout.LabelField("SpawnPoint, use size for Area", EditorStyles.boldLabel);
                if (so.targetObject != FindObjectOfType<DclSceneMeta>())
                {
                    so = new SerializedObject(FindObjectOfType<DclSceneMeta>());
                    sceneSpawnPoints = so.FindProperty("spawnPoints");
                }
                popupOptions.Clear();
                foreach (Transform transform in sceneMeta.spawnPoints)
                {
                    if (transform)
                        popupOptions.Add("SpawnPoint: " + SceneTraverserUtils.FloatToString(transform.position.x) + ", " + SceneTraverserUtils.FloatToString(transform.position.y) + ", " + SceneTraverserUtils.FloatToString(transform.position.z));
                }

                sceneMeta.currentSpawnPoint = EditorGUILayout.Popup("Current Spawn Point:", sceneMeta.currentSpawnPoint, popupOptions.ToArray());
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.PropertyField(sceneSpawnPoints);

                if (EditorGUI.EndChangeCheck())
                {
                    so.ApplyModifiedProperties(); // Remember to apply modified properties
                }

                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Use Custom Camera Target?", EditorStyles.boldLabel);
                sceneMeta.allowCCT = EditorGUILayout.Toggle(sceneMeta.allowCCT);
                GUILayout.EndHorizontal();
                sceneMeta.rotX = EditorGUILayout.FloatField("Set X", sceneMeta.rotX);
                sceneMeta.rotY = EditorGUILayout.FloatField("Set Y", sceneMeta.rotY);
                sceneMeta.rotZ = EditorGUILayout.FloatField("Set Z", sceneMeta.rotZ);

                EditorGUILayout.LabelField("Required Permissions", EditorStyles.boldLabel);  //DCL PERMISSIONS

                GUILayout.BeginHorizontal();
                GUILayout.BeginVertical();

                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Use Web3 Api?");
                sceneMeta.useWeb3Api = EditorGUILayout.Toggle(sceneMeta.useWeb3Api);
                GUILayout.EndHorizontal();

                GUILayout.Space(SPACE);

                //GUILayout.BeginHorizontal();
                //EditorGUILayout.LabelField("Enable voice chat?", EditorStyles.boldLabel);  //THIS IS NOT A PERMISSION BUT TOOGLES THE VOICE CHAT ENABLED/DISABLED
                //sceneMeta.voiceChatEnabled = EditorGUILayout.Toggle(sceneMeta.voiceChatEnabled);
                sceneMeta.voiceChatEnabled = true;
                //GUILayout.EndHorizontal();

                GUILayout.EndVertical();
                GUILayout.BeginVertical();

                //GUILayout.BeginHorizontal();
                //EditorGUILayout.LabelField("Use Fetch?");
                //sceneMeta.useFetch = EditorGUILayout.Toggle(sceneMeta.useFetch);
                sceneMeta.useFetch = true;
                //GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Use Websocket?");
                sceneMeta.useWebSocket = EditorGUILayout.Toggle(sceneMeta.useWebSocket);
                GUILayout.EndHorizontal();

                //GUILayout.BeginHorizontal();
                //EditorGUILayout.LabelField("Open external Links?");
                //sceneMeta.openExternalLink = EditorGUILayout.Toggle(sceneMeta.openExternalLink);
                sceneMeta.openExternalLink = true;
                //GUILayout.EndHorizontal();

                GUILayout.EndVertical();
                GUILayout.EndHorizontal();

                EditorGUI.indentLevel = 0;
            }

            if (foldout != oriFoldout) EditorPrefs.SetBool("DclBoldOwner", foldout);

            EditorGUILayout.EndVertical();
        }
        private void RunMenu(object str)
        {
            string type = (string)str;
            
            if (EditorUtility.DisplayDialog("Confirm to run Dcl",
                    "Are you sure to run the Dcl scene?", "Yes", "No"))
            {
                RunCommand.DclRunStart(exportPath, type);
                ShowNotification(new GUIContent("Decentraland is Starting"));
            }
        }
        
        public static void clearDeps()
        {
            nodeVersion = null;
            npmVersion = null;
            gltfVersion = null;
            dclCliVersion = null;

            nodeFound = false;
            npmFound = false;
            gltfFound = false;
            dclCliFound = false;
        }
        private void Export(bool exportScripts = true, bool exportModels = true, bool exportMetadata = true)
        {

            initialTime = DateTime.Now;
            if (string.IsNullOrEmpty(exportPath))
            {
                EditorUtility.DisplayDialog("Path Incorrect", "You must assign a valid path!", "OK");
                return;
            }

            if (!Directory.Exists(Path.Combine(exportPath, "src")))
            {
                Directory.CreateDirectory(Path.Combine(exportPath, "src"));
            }

            var statistics = new SceneStatistics();

            StringBuilder exportStr = new StringBuilder();
            StringBuilder additionalStr = new StringBuilder();
            var resourceRecorder = SceneTraverserDcl.TraverseDclScene(exportStr, additionalStr, statistics, null, mFormat);  //scenetraverse
            
            //// Add DCL Imports to index.ts
            SceneTraverserUtils.ImportDclModules(resourceRecorder, exportStr);

            exportStr.Append(additionalStr);

            if (exportAditionalCode)
            {
                int index = 0;
                if (exportStr.ToString().LastIndexOf("import") != -1)
                {
                    index = exportStr.ToString().Substring(0, exportStr.ToString().LastIndexOf("import")).Length;
                    index += exportStr.ToString().Substring(index, exportStr.ToString().Substring(exportStr.ToString().LastIndexOf("import")).IndexOf('\n')).Length;
                    exportStr.Insert(index, "\n");
                    exportStr.Append("\n");
                }
                for (int i = sceneMeta.customCode.Count - 1; i >= 0; i--)
                {
                    if (sceneMeta.customCode[i].enable && !string.IsNullOrEmpty(sceneMeta.customCode[i].code))
                    {
                        switch (sceneMeta.customCode[i].position)
                        {
                            case AditionalCode.Type.beggining:
                                if (index > 0)
                                    exportStr.Insert(index + "\n\n".Length, sceneMeta.customCode[i].code + "\n");
                                else
                                    exportStr.Insert(index, sceneMeta.customCode[i].code + "\n");
                                break;
                            case AditionalCode.Type.ending:
                                exportStr.Append(sceneMeta.customCode[i].code + "\n");
                                break;

                        }
                    }
                }
            }
            
            if (exportScripts)
                File.WriteAllText(Path.Combine(exportPath, "src/index.ts"), exportStr.ToString());
            
            if(exportModels)
                ExportModels(resourceRecorder);
            
            if(exportMetadata)
                ExportSceneJson();

            Debug.Log("===Export Complete " + DateTime.Now.ToString("HH:mm") + "===");
            Debug.Log("===Time spent: " + DateTime.Now.Subtract(initialTime).Hours + ":" + DateTime.Now.Subtract(initialTime).Minutes + ":" + DateTime.Now.Subtract(initialTime).Seconds + ":" + DateTime.Now.Subtract(initialTime).Milliseconds + "===");
            if (!EditorUtility.DisplayDialog("EXPORT COMPLETED", "Export Completed at: " + DateTime.Now.ToString("HH:mm") + "\n" +
                "Time spent: " + DateTime.Now.Subtract(initialTime).Hours + ":" + DateTime.Now.Subtract(initialTime).Minutes + ":" + DateTime.Now.Subtract(initialTime).Seconds + ":" + DateTime.Now.Subtract(initialTime).Milliseconds,
                "Ok", "Open Folder"))
            {
                Application.OpenURL(exportPath);
            }
            
            //Ask the user to delete external textures or keep them in the unity_assets folder
            if (mFormat == ExportFormat.GLBCompressedTextures && exportModels)
            {
                if (EditorUtility.DisplayDialog("Delete Texture Folder", "You selected compressed textures in glb format\nDo you want to delete the textures folder?", "Delete", "Keep textures"))
                {
                    RunCommand.deleteExternalTextures(exportPath);
                }
            }
        }
        private void ExportModels(ResourceRecorder resourceRecorder)
        {
            //Delete all files in exportPath/unity_assets/
            string unityAssetsFolderPath = Path.Combine(exportPath, "unity_assets/");
            //ClearFolder
            UnityEditor.FileUtil.DeleteFileOrDirectory(unityAssetsFolderPath);
            //Create folder again
            Directory.CreateDirectory(unityAssetsFolderPath);

            foreach (var go in resourceRecorder.primitivesToExport)
            {
                warnings(go);
            }
            foreach (var go in resourceRecorder.meshesToExport)
            {
                warnings(go);

                string tempPath = Path.Combine(unityAssetsFolderPath, SceneTraverserUtils.GetIdentityName(go) + ".gltf"); //gltf
                sceneMeta.sceneToGlTFWiz.ExportGameObjectAndChildren(go, tempPath, null, false, true, true, false);

                switch (mFormat)
                {
                    case ExportFormat.GLTFExternalTextures:
                        //Default export is glTF with external textures so this option don't need a cmd function
                        break;
                    case ExportFormat.GLBExternalTextures:
                        RunCommand.gltfGLBexternal(unityAssetsFolderPath, SceneTraverserUtils.GetIdentityName(go)); //Convert glTf to GLB External Texture
                        break;
                    case ExportFormat.GLBCompressedTextures:
                        RunCommand.gltfGLBcompress(unityAssetsFolderPath, SceneTraverserUtils.GetIdentityName(go)); //Convert glTf to GLB Compressed Texture
                        break;
                }
            }
            //textures
            foreach (var texture in resourceRecorder.primitiveTexturesToExport)
            {
                var relPath = AssetDatabase.GetAssetPath(texture);

                if (string.IsNullOrEmpty(relPath) || relPath.StartsWith("Library/"))
                {
                    //built-in asset
                    var bytes = ((Texture2D)texture).EncodeToPNG();
                    string str = Path.Combine(unityAssetsFolderPath, texture.name + ".png");
                    File.WriteAllBytes(str.Replace(" ", string.Empty), bytes);
                }
                else if (!relPath.Contains(".png") && !relPath.Contains(".jpeg") && !relPath.Contains(".jpg"))
                {
                    var bytes = ((Texture2D)texture).EncodeToPNG();
                    string toPath = unityAssetsFolderPath + Path.GetDirectoryName(relPath) + "/" + texture.name + ".png";
                    string directoryPath = Path.GetDirectoryName(toPath);
                    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                    File.WriteAllBytes(toPath, bytes);
                }
                else
                {
                    var path = Application.dataPath; //<path to project folder>/Assets
                    path = path.Remove(path.Length - 6, 6) + relPath;
                    var toPath = unityAssetsFolderPath + relPath;
                    toPath = toPath.Replace (" ", string.Empty);
                    var directoryPath = Path.GetDirectoryName(toPath);
                    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                    File.Copy(path, toPath, true);
                }
                //Debug.Log("Texture out " + relPath);
            }

            //audioClips
            foreach (var audioClip in resourceRecorder.audioClipsToExport)
            {
                var relPath = AssetDatabase.GetAssetPath(audioClip);
                var relName = Path.GetFileName(relPath);
                if (string.IsNullOrEmpty(relPath) || relPath.StartsWith("Library/"))
                {
                    Debug.LogError("AudioClip should not be built-in assets.");
                }
                else
                {
                    var path = Application.dataPath; //<path to project folder>/Assets
                    path = path.Remove(path.Length - 6, 6) + relPath;
                    var toPath = unityAssetsFolderPath + "AudioClips/" + relName;
                    var directoryPath = Path.GetDirectoryName(toPath);
                    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                    File.Copy(path, toPath, true);
                }
                //Debug.Log("Audio out " + relPath);
            }
            //font
            foreach (var font in resourceRecorder.fontToExport)
            {
                var relPath = AssetDatabase.GetAssetPath(font);
                if (string.IsNullOrEmpty(relPath) || relPath.StartsWith("Library/"))
                {
                    Debug.LogError("Font should not be built-in assets.");
                }
                else
                {
                    var path = Application.dataPath; //<path to project folder>/Assets
                    path = path.Remove(path.Length - 6, 6) + relPath;
                    var toPath = unityAssetsFolderPath + relPath;
                    var directoryPath = Path.GetDirectoryName(toPath);
                    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                    File.Copy(path, toPath, true);
                }
                Debug.Log("Font out " + relPath);
            }
        }
        private void ExportSceneJson()
        {
            //Write scene.json
            {
                var fileTxt = GetSceneJsonFileTemplate();
                fileTxt = fileTxt.Replace("{LAND_TITLE}", sceneMeta.landTitle);
                fileTxt = fileTxt.Replace("{LAND_INFO}", sceneMeta.landInfo);
                fileTxt = fileTxt.Replace("{LAND_IMG}", sceneMeta.landImg);

                fileTxt = fileTxt.Replace("{CONTACT_NAME}", sceneMeta.contactName);
                fileTxt = fileTxt.Replace("{CONTACT_EMAIL}", sceneMeta.email);
                fileTxt = fileTxt.Replace("{ETH_ADDRESS}", sceneMeta.ethAddress);

                if (sceneMeta.spawnPoints.Count == 0 || (sceneMeta.spawnPoints.Count != 0 && sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint] == null))
                {
                    fileTxt = fileTxt.Replace("{XSPAWN}", SceneTraverserUtils.FloatToString(sceneMeta.spawnX));
                    fileTxt = fileTxt.Replace("{YSPAWN}", SceneTraverserUtils.FloatToString(sceneMeta.spawnY));
                    fileTxt = fileTxt.Replace("{ZSPAWN}", SceneTraverserUtils.FloatToString(sceneMeta.spawnZ));

                    fileTxt = fileTxt.Replace("{XOR}", SceneTraverserUtils.FloatToString(sceneMeta.rotX));
                    fileTxt = fileTxt.Replace("{YOR}", SceneTraverserUtils.FloatToString(sceneMeta.rotY));
                    fileTxt = fileTxt.Replace("{ZOR}", SceneTraverserUtils.FloatToString(sceneMeta.rotZ));
                }
                else
                {
                    if (sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].localScale.x > 1.0f)
                    {
                        fileTxt = fileTxt.Replace("{XSPAWN}", "[" + SceneTraverserUtils.FloatToString(sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].position.x) + "," + SceneTraverserUtils.FloatToString(sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].localScale.x) + "]");
                    }
                    else
                    {
                        fileTxt = fileTxt.Replace("{XSPAWN}", SceneTraverserUtils.FloatToString(sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].position.x));
                    }
                    if (sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].localScale.y > 1.0f)
                    {
                        fileTxt = fileTxt.Replace("{YSPAWN}", "[" + SceneTraverserUtils.FloatToString(sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].position.y) + "," + SceneTraverserUtils.FloatToString(sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].localScale.y) + "]");
                    }
                    else
                    {
                        fileTxt = fileTxt.Replace("{YSPAWN}", SceneTraverserUtils.FloatToString(sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].position.y));
                    }
                    if (sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].localScale.z > 1.0f)
                    {
                        fileTxt = fileTxt.Replace("{ZSPAWN}", "[" + SceneTraverserUtils.FloatToString(sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].position.z) + "," + SceneTraverserUtils.FloatToString(sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].localScale.z) + "]");
                    }
                    else
                    {
                        fileTxt = fileTxt.Replace("{ZSPAWN}", SceneTraverserUtils.FloatToString(sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].position.z));
                    }
                }

                if (sceneMeta.allowCCT == true)
                {
                    fileTxt = fileTxt.Replace("{XOR}", SceneTraverserUtils.FloatToString(sceneMeta.rotX));
                    fileTxt = fileTxt.Replace("{YOR}", SceneTraverserUtils.FloatToString(sceneMeta.rotY));
                    fileTxt = fileTxt.Replace("{ZOR}", SceneTraverserUtils.FloatToString(sceneMeta.rotZ));
                }
                else
                    fileTxt = fileTxt.Replace("{XOR}", SceneTraverserUtils.FloatToString(sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].position.x) + 0f);
                fileTxt = fileTxt.Replace("{YOR}", SceneTraverserUtils.FloatToString(sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].position.y + 1f));
                fileTxt = fileTxt.Replace("{ZOR}", SceneTraverserUtils.FloatToString(sceneMeta.spawnPoints[sceneMeta.currentSpawnPoint].position.z + 16f));

                StringBuilder grants = new StringBuilder();

                if (sceneMeta.useWeb3Api == true)
                {
                    grants.Append("\"USE_WEB3_API\",\n");
                }

                if (sceneMeta.useFetch == true)
                {
                    grants.Append("\"USE_FETCH\",\n");
                }

                if (sceneMeta.useWebSocket == true)
                {
                    grants.Append("\"USE_WEBSOCKET\",\n");
                }

                if (sceneMeta.openExternalLink == true)
                {
                    grants.Append("\"OPEN_EXTERNAL_LINK\",\n");
                }
                grants = grants.Remove(grants.Length - 2, 2);
                
                fileTxt = fileTxt.Replace("{GRANTS}", grants.ToString());
                grants.Clear();

                if (sceneMeta.voiceChatEnabled == true)
                {
                    fileTxt = fileTxt.Replace("{VOICECHAT}", "\"enabled\"");
                }
                else
                    fileTxt = fileTxt.Replace("{VOICECHAT}", "\"disabled\"");


                var parcelsString = GetParcelsString();
                fileTxt = fileTxt.Replace("{PARCELS}", parcelsString);
                if (sceneMeta.parcels.Count > 0)
                {
                    fileTxt = fileTxt.Replace("{BASE}", ParcelToString(sceneMeta.parcels[0]));
                }

                var filePath = Path.Combine(exportPath, "scene.json");
                File.WriteAllText(filePath, fileTxt);
            }
        }
        string GetSceneJsonFileTemplate()
        {
            var guids = AssetDatabase.FindAssets("dcl_scene_json_template");
            if (guids.Length <= 0)
            {
                if (EditorUtility.DisplayDialog("Cannot find dcl_scene_json_template.txt in the project!",
                    "Please re-install Decentraland Exporter Toolkit Package to fix this problem.", "Ok"))
                {
                    //
                }

                return null;
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            var template = AssetDatabase.LoadAssetAtPath(path, typeof(TextAsset)) as TextAsset;
            return template.text;
        }

        string GetParcelsString()
        {
        /*"30,-15",
          "30,-16",
          "31,-15"*/
            var sb = new StringBuilder();
            if (sceneMeta.parcels.Count > 0)
            {
                const string indentUnit = "  ";
                sb.AppendIndent(indentUnit, 3).Append(ParcelToString(sceneMeta.parcels[0]));
                for (var i = 1; i < sceneMeta.parcels.Count; i++)
                {
                    sb.Append(",\n");
                    sb.AppendIndent(indentUnit, 3).Append(ParcelToString(sceneMeta.parcels[i]));
                }
            }

            return sb.ToString();
        }
        public static string ParcelToString(ParcelCoordinates parcel)
        {
            return string.Format("\"{0},{1}\"", parcel.x, parcel.y);
        }
        
        private void CheckAndGetDclSceneMetaObject()
        {
            var rootGameObjects = new List<GameObject>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                rootGameObjects.AddRange(SceneManager.GetSceneAt(i).GetRootGameObjects());
            }

            foreach (var go in rootGameObjects)
            {
                if (go.name == ".dclManager")
                {
                    sceneMeta = go.GetComponent<DclSceneMeta>();
                    if (!sceneMeta)
                    {
                        sceneMeta = go.AddComponent<DclSceneMeta>();
                        GameObject spawnPoint = new GameObject("spawnPoint0DCL");
                        spawnPoint.transform.position = new Vector3(0, 1, 0);
                        spawnPoint.transform.localScale = Vector3.zero;
                        spawnPoint.transform.parent = sceneMeta.transform;
                        sceneMeta.spawnPoints.Add(spawnPoint.transform);
                        EditorUtility.SetDirty(sceneMeta);
                        EditorSceneManager.MarkSceneDirty(go.scene);
                    }
                    return;
                }
            }

            //Did not find .dclManager, Create one.
            var o = new GameObject(".dclManager");
            sceneMeta = o.AddComponent<DclSceneMeta>();
            GameObject spwPoint = new GameObject("spawnPoint0DCL");
            spwPoint.transform.position = new Vector3(0, 1, 0);
            spwPoint.transform.localScale = Vector3.zero;
            spwPoint.transform.parent = sceneMeta.transform;
            sceneMeta.spawnPoints.Add(spwPoint.transform);
            EditorUtility.SetDirty(sceneMeta);
            EditorSceneManager.MarkSceneDirty(o.scene);
        }
        void StatisticsLineGUI(string indexName, long leftValue, long rightValue)
        {
            var oriColor = GUI.contentColor;
            EditorGUILayout.BeginHorizontal();
            if (leftValue > rightValue)
            {
                GUILayout.Label(DclEditorSkin.WarningIconSmall, GUILayout.Width(20));
                GUI.contentColor = Color.yellow;
            }
            EditorGUILayout.LabelField(indexName, string.Format("{0} / {1}", leftValue, rightValue));
            EditorGUILayout.EndHorizontal();
            GUI.contentColor = oriColor;
        }
        void WarningsGUI()
        {
            var foldout = EditorPrefs.GetBool("DclFoldStat", true);
            if (foldout)
            {
                var warningCount = sceneMeta.sceneWarningRecorder.OutOfLandWarnings.Count +
                                   sceneMeta.sceneWarningRecorder.AreaOutOfLandWarnings.Count +
                                   sceneMeta.sceneWarningRecorder.UnsupportedShaderWarnings.Count +
                                   sceneMeta.sceneWarningRecorder.InvalidTextureWarnings.Count +
                                   sceneMeta.sceneWarningRecorder.InvalidNamingWarnings.Count;

                if (warningCount > 0)
                {
                    GUILayout.Label("Click the warning to focus in the scene", EditorStyles.centeredGreyMiniLabel);
                    foreach (var areaOutOfLandWarning in sceneMeta.sceneWarningRecorder.AreaOutOfLandWarnings)
                    {
                        WarningLineGUI(string.Format("Out of land range : {0}", areaOutOfLandWarning.renderer.name),
                            null, areaOutOfLandWarning.renderer.gameObject);
                    }

                    foreach (var outOfLandWarning in sceneMeta.sceneWarningRecorder.OutOfLandWarnings)
                    {
                        WarningLineGUI(string.Format("Out of land range : {0}", outOfLandWarning.renderer.name),
                            null, outOfLandWarning.renderer.gameObject);
                    }

                    foreach (var warning in sceneMeta.sceneWarningRecorder.UnsupportedShaderWarnings)
                    {
                        var path = AssetDatabase.GetAssetPath(warning.renderer);
                        WarningLineGUI(string.Format("Unsupported shader : {0}", warning.renderer.name), "Only URP Lit Shader is supported", path, new Color(1, 0.7f, 0.1f, 1));
                    }
                    foreach (var warning in sceneMeta.sceneWarningRecorder.InvalidTextureWarnings)
                    {
                        var path = AssetDatabase.GetAssetPath(warning.renderer);
                        WarningLineGUI(string.Format("Invalid texture size : {0}", warning.renderer.name), "Texture sizes must be one of 1,2,4,8,..., 512 (1024 is not allowed, experimental)", path, Color.yellow);
                    }
                    foreach (var warning in sceneMeta.sceneWarningRecorder.InvalidNamingWarnings)
                    {
                        WarningLineGUI(string.Format("Invalid Naming : {0}", warning.renderer.name),
                            null, warning.renderer.gameObject);
                    }
                }
            }
        }

        void WarningLineGUI(string text, string hintMessage, GameObject gameObject)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(DclEditorSkin.WarningIconSmall, GUILayout.Width(20));
            var oriColor = GUI.contentColor;
            GUI.contentColor = Color.yellow;
            if (GUILayout.Button(text, EditorStyles.label))
            {
                if (hintMessage != null) ShowNotification(new GUIContent(hintMessage));
                EditorGUIUtility.PingObject(gameObject);
            }

            EditorGUILayout.EndHorizontal();
            GUI.contentColor = oriColor;
        }

        void WarningLineGUI(string text, string hintMessage, string assetPath, Color color)
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label(DclEditorSkin.WarningIconSmall, GUILayout.Width(20));
            var oriColor = GUI.contentColor;
            GUI.contentColor = color;
            if (GUILayout.Button(text, EditorStyles.label))
            {
                if (hintMessage != null) ShowNotification(new GUIContent(hintMessage));
                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(assetPath);
            }

            EditorGUILayout.EndHorizontal();
            GUI.contentColor = oriColor;
        }
        void warnings(GameObject go)
        {
            if (go.name.Contains(":") || go.name.Contains(".") || go.name.Contains(",") || go.name.Contains("*"))
            {
                Debug.LogError("EXPORT STOPPED\nGameObject: \"" + go.name + "\" has a non supported name\nYou shouldn't use special characters");
                if (EditorUtility.DisplayDialog("EXPORT STOPPED", "gameObject: \"" + go.name + "\" name is NOT suported!!\nYou shouldn't use special characters", "Ok"))
                {
                    return;
                }
            }
            if (go.GetComponent<MeshRenderer>())
            {
                if(go.GetComponent<TextMesh>() || go.GetComponent<TextMeshPro>()) return;
                
                var matRenderer = go.GetComponent<MeshRenderer>();
                if (!matRenderer.sharedMaterial.shader.name.Contains("Universal Render Pipeline/Lit"))
                {
                    Debug.LogError("WARNING\nGameObject: \"" + matRenderer.gameObject + "\" has a non supported shader: " + matRenderer.sharedMaterial.shader.name + "\nYou should use Polyshader instead");
                    if (EditorUtility.DisplayDialog("WARNING", "Material on \"" + matRenderer.gameObject + "\" is NOT suported!!\nYou should use POLYSHADER instead of \n" + matRenderer.sharedMaterial.shader.name, "Ok"))
                    {

                    }
                }
            }
            else if (go.GetComponent<SkinnedMeshRenderer>())
            {
                var matRenderer = go.GetComponent<SkinnedMeshRenderer>();
                if (!matRenderer.sharedMaterial.shader.name.Contains("Universal Render Pipeline/Lit"))
                {
                    Debug.LogError("WARNING\nGameObject: \"" + matRenderer.gameObject + "\" has a non supported shader: " + matRenderer.sharedMaterial.shader.name + "\nYou should use Polyshader instead");
                    if (EditorUtility.DisplayDialog("WARNING", "Material on \"" + matRenderer.gameObject + "\" is NOT suported!!\nYou should use POLYSHADER instead of \n" + matRenderer.sharedMaterial.shader.name, "Ok"))
                    {

                    }
                }
            }
        }
    }
}