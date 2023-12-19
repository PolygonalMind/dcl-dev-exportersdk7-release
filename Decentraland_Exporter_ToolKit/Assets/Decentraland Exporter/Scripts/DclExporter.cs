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
        private List<string> autoFindFolders = new List<string>();

        //Export
        internal static Color oriColor;
        private static Vector2 scrollPosition;
        private string exportPath;
        private string newExportPath;
        public static string nodeVersion;
        public static string npmVersion;
        public static string gltfVersion;
        public static string dclCliVersion;
        public static bool nodeFound;
        public static bool npmFound;
        public static bool gltfFound;
        public static bool dclCliFound;
        public static bool GlbExternalTexture = false;
        private bool dclProjectExist;
        public bool updateTrasverser;
        public float timeUpdateTrasverser;
        const int SPACE = 5;
        internal static GUIStyle warningStyle = new GUIStyle();
        private static GUIStyle labelStyle = new GUIStyle();

        //Skybox
        private Quaternion lightRotation;
        private Color lightColor;
        private Color skyColor;
        private Color equatorColor;
        private Color groundColor;
        private Color fogColor;

        //Bounding box
        public static bool showBoundingBoxes = false;

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
        private DclSceneMeta sceneMeta;

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
        }
        void OnInspectorUpdate()
        {
            dclProjectExist = Directory.Exists(exportPath + "/src") && File.Exists(exportPath + "/scene.json") && File.Exists(exportPath + "/package-lock.json");
            Repaint();
        }
        void OnGUI()
        {
            SetUp();
            
            EditorGUI.BeginChangeCheck();

            exportPath = EditorPrefs.GetString("DclExportPath");

            if (string.IsNullOrEmpty(nodeVersion) || string.IsNullOrEmpty(npmVersion) || string.IsNullOrEmpty(gltfVersion) || string.IsNullOrEmpty(dclCliVersion))
            {
                MissingDependenciesGUI();

                GUILayout.FlexibleSpace();

                LinkButtons();

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(sceneMeta);
                    EditorSceneManager.MarkSceneDirty(sceneMeta.gameObject.scene);
                }
                return;
            }
            
            DependenciesGUI();

            GUILayout.Space(SPACE);

            if (!Directory.Exists(exportPath) || !dclProjectExist)
            {
                InitialGUI();
                
                ShowNotification(new GUIContent("You need to select a valid Decentraland project folder"), 0.1f);

                GUILayout.FlexibleSpace();

                LinkButtons();
                
                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(sceneMeta);
                    EditorSceneManager.MarkSceneDirty(sceneMeta.gameObject.scene);
                }
                return;
            }

            DclPathGUI();

            GUILayout.Space(SPACE);
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            DclCliGUI();

            GUILayout.Space(SPACE);

            DclProjectGUI();

            GUILayout.Space(SPACE);

            SetDayTime();

            GUILayout.Space(SPACE);

            StatGUI();

            GUILayout.Space(SPACE);

            SceneDataGUI();

            GUILayout.Space(SPACE);

            EditorGUILayout.EndScrollView();

            GUILayout.FlexibleSpace();

            ExportButtons();

            LinkButtons();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(sceneMeta);
                EditorSceneManager.MarkSceneDirty(sceneMeta.gameObject.scene);
            }
        }
        private void ExportButtons()
        {
            if (!dclProjectExist) return;
            
            EditorGUILayout.BeginVertical("box");
            GUI.backgroundColor = new Color(1f, 0.8f, 1f);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Export 3D Models", GUILayout.Height(26)))
            {
                saveOpenScenes();
                Export(false, true, false);
            }
            if (GUILayout.Button("Export Scripts", GUILayout.Height(26)))
            {
                saveOpenScenes();
                Export(true, false, false);
            }
            if (GUILayout.Button("Export Metadata", GUILayout.Height(26)))
            {
                saveOpenScenes();
                Export(false, false, true);
            }
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(SPACE);
            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            if (GUILayout.Button("Export", GUILayout.Height(32)))
            {
                saveOpenScenes();
                Export();
            }
            GUI.backgroundColor = oriColor;
            EditorGUILayout.EndVertical();
        }
        private void saveOpenScenes()
        {
            if (EditorUtility.DisplayDialog("Save Open Scenes?", "Do you want to save the open scenes before the export?", "Yes", "No"))
            {
                EditorSceneManager.SaveOpenScenes();
                Debug.Log("===Open scenes saved===");
            }
        }
        private void LinkButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(string.Format("Github Repository:\n{0}", "https://github.com/PolygonalMind/dcl-dev-exportersdk7-release"), EditorStyles.helpBox))
            {
                Application.OpenURL("https://github.com/PolygonalMind/dcl-dev-exportersdk7-release");
            }
            if (GUILayout.Button(string.Format("By PolygonalMind:\n{0}", "https://www.polygonalmind.com/"), EditorStyles.helpBox))
            {
                Application.OpenURL("https://www.polygonalmind.com/");
            }
            EditorGUILayout.EndHorizontal();
        }
        private void MissingDependenciesGUI()
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
            
            GUILayout.Space(SPACE);

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
        private void InitialGUI()
        {
            string folder = Path.GetFullPath(Path.Combine(Application.dataPath, "../")) + "Decentraland/";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            
            EditorGUILayout.BeginVertical("box");
            warningStyle.normal.textColor = Color.green;
            GUILayout.Label("Welcome to the Decentraland SDK7 Toolkit!\nSelect an option to continue...", warningStyle);
            warningStyle.normal.textColor = Color.yellow;
            EditorGUILayout.EndVertical();

            GUILayout.Space(SPACE*4);

            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("1.- Use an existing Decentraland project", labelStyle);

            if (GUILayout.Button("Locate Dcl Project Root", GUILayout.Height(32)))
            {
                newExportPath = EditorUtility.OpenFolderPanel("Select the Decentraland project folder", folder, "");
                if (string.IsNullOrEmpty(newExportPath)) newExportPath = exportPath;
                autoFindFolders.Clear();
            }

            GUILayout.Space(SPACE*4);
            
            GUILayout.Label("2.- Autofind Decentraland projects in '.../UnityRoot/Decentraland/'", labelStyle);
            
            if (GUILayout.Button("Autofind", GUILayout.Height(32)))
            {
                AutoFindExport();
            }

            GUI.backgroundColor = new Color(1.0f, 0.8f, 0.5f);
            if (autoFindFolders.Count > 0)
            {
                GUILayout.Space(SPACE * 4);
                for (int i = 0; i < autoFindFolders.Count; i++)
                {
                    if (GUILayout.Button(Path.GetFileName(Path.GetDirectoryName(autoFindFolders[i])) + "\\" + Path.GetFileName(autoFindFolders[i]), GUILayout.Height(25)))
                    {
                        newExportPath = autoFindFolders[i];
                        exportPath = newExportPath;
                        EditorPrefs.SetString("DclExportPath", newExportPath);
                        ShowNotification(new GUIContent("Path: " + Path.GetFileName(Path.GetDirectoryName(autoFindFolders[i])) + "\\" + Path.GetFileName(autoFindFolders[i])));
                        autoFindFolders.Clear();
                    }
                }
                GUILayout.Space(SPACE * 4);
            }
            GUI.backgroundColor = oriColor;

            GUILayout.Space(SPACE*4);
            
            GUILayout.Label("3.- Create a new Decentraland project", labelStyle);

            if (GUILayout.Button("Create Decentraland project", GUILayout.Height(32)))
            {
                newExportPath = EditorUtility.OpenFolderPanel("Select the folder location", folder, "");
                if (!string.IsNullOrEmpty(newExportPath))
                {
                    recursiveDirectoryCreation(0);

                    if (EditorUtility.DisplayDialog("Confirm to init Decentraland",
                            string.Format("Are you sure to init Decentraland in this path?\n{0}", newExportPath), "Yes", "No"))
                    {
                        RunCommand.DclInit(newExportPath);
                        autoFindFolders.Clear();
                    }
                    else
                    {
                        Directory.Delete(newExportPath);
                        newExportPath = exportPath;
                    }
                }
                else
                {
                    newExportPath = exportPath;
                }
            }
            EditorGUILayout.EndVertical();
            
            if (newExportPath != exportPath)
            {
                exportPath = newExportPath;
                EditorPrefs.SetString("DclExportPath", newExportPath);
            }
        }
        private void DclPathGUI()
        {
            if (!dclProjectExist) return;
            
            string folder = Path.GetFullPath(Path.Combine(Application.dataPath, "../")) + "Decentraland/";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label(string.Format("Dcl Project Path:  <color=#A8EDA8>{0}</color>", newExportPath), labelStyle, GUILayout.Width(100));

            GUILayout.BeginHorizontal();
            newExportPath = EditorGUILayout.TextField(exportPath);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Wipe Path", GUILayout.Height(24)))
            {
                newExportPath = "";
            }
            if (GUILayout.Button("Set Dcl Project Root", GUILayout.Height(24)))
            {
                newExportPath = EditorUtility.OpenFolderPanel("Select the Decentraland project folder", folder, "");
                if (string.IsNullOrEmpty(newExportPath)) newExportPath = exportPath;
                autoFindFolders.Clear();
            }
            if (GUILayout.Button("Create Dcl Project", GUILayout.Height(24)))
            {
                newExportPath = EditorUtility.OpenFolderPanel("Select the folder location", folder, "");
                if (!string.IsNullOrEmpty(newExportPath))
                {
                    recursiveDirectoryCreation(0);

                    if (EditorUtility.DisplayDialog("Confirm to init Decentraland",
                            string.Format("Are you sure to init Decentraland in this path?\n{0}", newExportPath), "Yes", "No"))
                    {
                        RunCommand.DclInit(newExportPath);
                        autoFindFolders.Clear();
                    }
                    else
                    {
                        Directory.Delete(newExportPath);
                        newExportPath = exportPath;
                    }
                }
                else
                {
                    newExportPath = exportPath;
                }
            }
            //Check if the project contains /src and scene.json to avoid errors deleting an incorrect folder, this way only can be deleted if its a Dcl project initialiced

            //if (dclProjectExist)
            //{
            //    GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
            //    if (GUILayout.Button("Delete Content", GUILayout.Height(24), GUILayout.Width(100)))
            //    {
            //        if (EditorUtility.DisplayDialog("Confirm to delete Dcl project directory content",
            //            string.Format("Are you sure to delete all the Dcl project directory content?\n{0}", exportPath), "Yes", "No"))
            //        {
            //            RunCommand.DclDeleteFolderContent(exportPath);
            //            ShowNotification(new GUIContent("Deleting Dcl project folder content"), 0.5f);
            //        }
            //    }
            //    GUI.backgroundColor = oriColor;
            //}

            //
            GUI.backgroundColor = new Color(1.0f, 0.8f, 0.5f);

            if (GUILayout.Button("Auto Find", GUILayout.Height(24)))
            {
                AutoFindExport();
            }

            if (newExportPath != exportPath)
            {
                exportPath = newExportPath;
                EditorPrefs.SetString("DclExportPath", newExportPath);
            }
            GUILayout.EndHorizontal();
            
            //AutoFind folders show the project list in the UnityProjectRoot/Decentraland folder
            if (autoFindFolders.Count > 0)
            {
                for (int i = 0; i < autoFindFolders.Count; i++)
                {
                    if (GUILayout.Button(Path.GetFileName(Path.GetDirectoryName(autoFindFolders[i])) + "\\" + Path.GetFileName(autoFindFolders[i]), GUILayout.Height(25)))
                    {
                        newExportPath = autoFindFolders[i];
                        exportPath = newExportPath;
                        EditorPrefs.SetString("DclExportPath", newExportPath);
                        ShowNotification(new GUIContent("Path: " + Path.GetFileName(Path.GetDirectoryName(autoFindFolders[i])) + "\\" + Path.GetFileName(autoFindFolders[i])));
                        autoFindFolders.Clear();
                    }
                }
            }

            GUI.backgroundColor = oriColor;
            GUILayout.EndVertical();
        }
        private void recursiveDirectoryCreation(int i)
        {
            if (string.IsNullOrEmpty(newExportPath))
                return;

            newExportPath = newExportPath + "/Export_" + i.ToString();

            if (Directory.Exists(newExportPath))
            {
                newExportPath = newExportPath.Replace("/Export_" + i.ToString(), "");
                i++;
                recursiveDirectoryCreation(i);
                return;
            }
            Directory.CreateDirectory(newExportPath);
        }
        private void DclCliGUI()
        {
            if (!dclProjectExist) return;
            
            EditorGUILayout.BeginVertical("box");

            GUILayout.Label(string.Format("Decentraland CLI:  <color=#A8EDA8>{0}</color>", dclCliVersion), labelStyle);
            GUILayout.BeginHorizontal();
            GUI.backgroundColor = new Color(1f, 0.8f, 1f);
            if (GUILayout.Button("Init Dcl Project", GUILayout.Height(32)))
            {
                if (Directory.GetFiles(exportPath).Length != 0 || Directory.GetDirectories(exportPath).Length != 0)
                {
                    Debug.LogError("The selected directory: " + exportPath + "\nIs not empty, select a valid path to Init a Decentraland project");
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

            GUILayout.Label(string.Format("Statistics:", dclCliVersion), labelStyle);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical(GUILayout.MinWidth(200));
            EditorGUILayout.Space(SPACE);

            GUI.backgroundColor = new Color(0.6f, 1.0f, 0.6f);
            if (GUILayout.Button("Refresh", GUILayout.MinHeight(25)))
            {
                sceneMeta.RefreshStatistics();
                sceneMeta.getParcelSetVolumes();
            }
            GUI.backgroundColor = oriColor;
            EditorGUILayout.Space(SPACE);
            updateTrasverser = EditorGUILayout.ToggleLeft("Autorefresh", updateTrasverser, GUILayout.MaxWidth(100));
            EditorGUILayout.Space(SPACE);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Refresh Interval:", GUILayout.MaxWidth(100));
            timeUpdateTrasverser = EditorGUILayout.FloatField(timeUpdateTrasverser, GUILayout.MaxWidth(60));
            GUILayout.Label("s");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(SPACE);
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
            showBoundingBoxes = EditorGUILayout.ToggleLeft("Show bounding boxes", showBoundingBoxes);
            EditorGUILayout.Space(SPACE);

            WarningsGUI();
            EditorGUILayout.EndHorizontal();
        }
        private void DclProjectGUI()
        {
            if (!dclProjectExist) return;

            EditorGUILayout.BeginVertical("box");
            GUILayout.Label(string.Format("Decentraland Project:  <color=#A8EDA8>{0}</color>", Path.GetFileName(exportPath)), labelStyle);
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Open Folder", GUILayout.Height(32)))
            {
                Application.OpenURL(exportPath);
            }
            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            if (GUILayout.Button("Run Local Preview", GUILayout.Height(32)))
            {
                RunMenu("");
                //GenericMenu menu = new GenericMenu();
                //menu.AddItem(new GUIContent("Run Local"), false, RunMenu, "");
                //menu.AddItem(new GUIContent("Run Local No Debug"), false, RunMenu, " --no-debug");
                //menu.AddItem(new GUIContent("Run Web3"), false, RunMenu, " --web3");
                //menu.AddItem(new GUIContent("Run Web3 No Debug"), false, RunMenu, " --web3 --no-debug");
                //menu.ShowAsContext();
            }
            GUI.backgroundColor = oriColor;
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


            GUILayout.EndVertical();
        }
        private void SetDayTime()
        {
            if (!dclProjectExist) return;

            EditorGUILayout.BeginVertical("Box");
            GUILayout.Label("Skybox & Illumination Time:", labelStyle);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("00:00"))
            {
                lightRotation = Quaternion.Euler(183, -45, -90);
                lightColor = new Color(0.686f, 0.655f, 0.738f);
                skyColor = new Color(0.604f, 0.530f, 0.943f);
                equatorColor = new Color(0.726f, 0.541f, 0.932f);
                groundColor = new Color(0.213f, 0.040f, 0.471f);
                fogColor = new Color(0.161f, 0.093f, 0.330f);
                    
                SetSkyboxTime(00, 0.2f, false);
            }
            if (GUILayout.Button("04:00"))
            {
                lightRotation = Quaternion.Euler(183, -45, -90);
                lightColor = new Color(0.692f, 0.538f, 0.934f);
                skyColor = new Color(0.899f, 0.878f, 0.917f);
                equatorColor = new Color(0.880f, 0.661f, 0.531f);
                groundColor = new Color(0.528f, 0.324f, 0.512f);
                fogColor = new Color(0.432f, 0.427f, 0.389f);


                SetSkyboxTime(04, 0.652f, true);
            }
            if (GUILayout.Button("08:00"))
            {
                lightRotation = Quaternion.Euler(152, -45, -90);
                lightColor = new Color(0.767f, 0.732f, 0.638f);
                skyColor = new Color(0.992f, 0.816f, 0.640f);
                equatorColor = new Color(0.718f, 0.774f, 0.841f);
                groundColor = new Color(0.818f, 0.523f, 0.272f);
                fogColor = new Color(0.517f, 0.572f, 0.382f);
                
                SetSkyboxTime(08, 1.1f, true);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("12:00"))
            {
                lightRotation = Quaternion.Euler(89, -45, -90);
                lightColor = new Color(0.852f, 0.838f, 0.797f);
                skyColor = new Color(0.954f, 0.789f, 0.678f);
                equatorColor = new Color(0.942f, 0.858f, 0.783f);
                groundColor = new Color(0.304f, 0.320f, 0.352f);
                fogColor = new Color(0.414f, 0.528f, 0.312f);

                SetSkyboxTime(12, 1.3f, true);
            }
            if (GUILayout.Button("16:00"))
            {
                lightRotation = Quaternion.Euler(46, -45, -90);
                lightColor = new Color(0.859f, 0.788f, 0.641f);
                skyColor = new Color(0.917f, 0.785f, 0.683f);
                equatorColor = new Color(0.866f, 0.797f, 0.721f);
                groundColor = new Color(0.740f, 0.550f, 0.414f);
                fogColor = new Color(0.484f, 0.517f, 0.409f);

                SetSkyboxTime(16, 1.2f, true);
            }
            if (GUILayout.Button("20:00"))
            {
                lightRotation = Quaternion.Euler(6, -45, -90);
                lightColor = new Color(0.798f, 0.583f, 0.284f);
                skyColor = new Color(0.819f, 0.715f, 0.744f);
                equatorColor = new Color(0.824f, 0.712f, 0.732f);
                groundColor = new Color(0.742f, 0.575f, 0.725f);
                fogColor = new Color(0.420f, 0.391f, 0.437f);

                SetSkyboxTime(20, 1f, true);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
        private void SceneDataGUI()
        {
            if (!dclProjectExist) return;
            
            EditorGUILayout.BeginVertical("box");

            var oriFoldout = EditorPrefs.GetBool("DclMetadata");
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

                EditorGUILayout.BeginVertical("box");

                var oriFoldout2 = EditorPrefs.GetBool("DclSpawn");
                var spawnFoldout = EditorGUILayout.Foldout(oriFoldout2, "SpawnPoints, use size for Area", true, EditorStyles.foldout);
                if (spawnFoldout)
                {
                    EditorGUI.indentLevel = 2;
                    foreach (Transform transform in sceneMeta.spawnPoints)
                    {
                        EditorGUILayout.LabelField(transform.name + ": ", EditorStyles.boldLabel);
                        GUILayout.BeginHorizontal("Box");
                        transform.localPosition = EditorGUILayout.Vector3Field("Position", transform.localPosition);
                        transform.localScale = EditorGUILayout.Vector3Field("Size", transform.localScale);
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Add Spawn", EditorStyles.miniButtonRight))
                    {
                        GameObject go = new GameObject();
                        go.name = "spawnPoint" + (sceneMeta.spawnPoints.Count>0? sceneMeta.spawnPoints.Count : 0) + "DCL";
                        go.transform.parent = sceneMeta.gameObject.transform;
                        sceneMeta.spawnPoints.Add(go.transform);
                    }
                    if(sceneMeta.spawnPoints.Count > 1)
                    {
                        if (GUILayout.Button("Remove Last", EditorStyles.miniButtonRight))
                        {
                            DestroyImmediate(sceneMeta.spawnPoints[sceneMeta.spawnPoints.Count - 1].gameObject);
                            sceneMeta.spawnPoints.RemoveAt(sceneMeta.spawnPoints.Count - 1);
                            sceneMeta.spawnPoints.Capacity -= 1;
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Use Custom Camera Target (Look at direction vector)", GUILayout.Width(320));
                    sceneMeta.allowCCT = EditorGUILayout.Toggle(sceneMeta.allowCCT);
                    GUILayout.EndHorizontal();
                    if (sceneMeta.allowCCT)
                    {
                        sceneMeta.rotX = EditorGUILayout.FloatField("Set X", sceneMeta.rotX);
                        sceneMeta.rotY = EditorGUILayout.FloatField("Set Y", sceneMeta.rotY);
                        sceneMeta.rotZ = EditorGUILayout.FloatField("Set Z", sceneMeta.rotZ);
                    }
                }
                if (spawnFoldout != oriFoldout2) EditorPrefs.SetBool("DclSpawn", spawnFoldout);

                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel = 0;
            }

            if (foldout != oriFoldout) EditorPrefs.SetBool("DclMetadata", foldout);

            EditorGUILayout.EndVertical();
        }
        private void RunMenu(object str)
        {
            string type = (string)str;
            
            if (EditorUtility.DisplayDialog("Confirm to run Dcl", "Are you sure to run the Dcl scene?", "Yes", "No"))
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

                StringBuilder spawnStr = new StringBuilder();
                if (sceneMeta.spawnPoints.Count <= 0)
                {
                    spawnStr.AppendFormat("\t{{\n\t\"name\": \"{0}\",\n\t\"default\": true,\n\t\"position\": {{\n", "defaultSpawn");
                    spawnStr.AppendFormat("\t\t\"x\": {0},\n", SceneTraverserUtils.FloatToString(sceneMeta.spawnX));
                    spawnStr.AppendFormat("\t\t\"y\": {0},\n", SceneTraverserUtils.FloatToString(sceneMeta.spawnY));
                    spawnStr.AppendFormat("\t\t\"z\": {0}\n", SceneTraverserUtils.FloatToString(sceneMeta.spawnZ));

                    spawnStr.Append("\t},\n\t\"cameraTarget\": {\n");
                    spawnStr.AppendFormat("\t\t\"x\": {0},\n", SceneTraverserUtils.FloatToString(sceneMeta.rotX));
                    spawnStr.AppendFormat("\t\t\"y\": {0},\n", SceneTraverserUtils.FloatToString(sceneMeta.rotY));
                    spawnStr.AppendFormat("\t\t\"z\": {0}\n", SceneTraverserUtils.FloatToString(sceneMeta.rotZ));
                    spawnStr.Append("\t}\n\t}");
                }
                else
                {
                    foreach (Transform spawn in sceneMeta.spawnPoints)
                    {
                        spawnStr.AppendFormat("{{\n\t\"name\": \"{0}\",\n\t\"default\": true,\n\t\"position\": {{\n", spawn.name);
                        if (spawn.localScale.x > 1.0f || spawn.localScale.y > 1.0f || spawn.localScale.z > 1.0f)
                        {
                            spawnStr.AppendFormat("\t\t\"x\": [{0},{1}],\n", SceneTraverserUtils.FloatToString(spawn.position.x - spawn.localScale.x / 2), SceneTraverserUtils.FloatToString(spawn.position.x + spawn.localScale.x / 2));
                            spawnStr.AppendFormat("\t\t\"y\": [{0},{1}],\n", SceneTraverserUtils.FloatToString(spawn.position.y - spawn.localScale.y / 2), SceneTraverserUtils.FloatToString(spawn.position.y + spawn.localScale.y / 2));
                            spawnStr.AppendFormat("\t\t\"z\": [{0},{1}]\n", SceneTraverserUtils.FloatToString(spawn.position.z - spawn.localScale.z / 2), SceneTraverserUtils.FloatToString(spawn.position.z + spawn.localScale.z / 2));
                        }
                        else
                        {
                            spawnStr.AppendFormat("\t\t\"x\": {0},\n", SceneTraverserUtils.FloatToString(spawn.position.x));
                            spawnStr.AppendFormat("\t\t\"y\": {0},\n", SceneTraverserUtils.FloatToString(spawn.position.y));
                            spawnStr.AppendFormat("\t\t\"z\": {0}\n", SceneTraverserUtils.FloatToString(spawn.position.z));
                        }

                        spawnStr.Append("\t},\n\t\"cameraTarget\": {\n");

                        if (!sceneMeta.allowCCT)
                        {
                            spawnStr.AppendFormat("\t\t\"x\": {0},\n", SceneTraverserUtils.FloatToString(spawn.position.x + 10f));
                            spawnStr.AppendFormat("\t\t\"y\": {0},\n", SceneTraverserUtils.FloatToString(spawn.position.y + 0f));
                            spawnStr.AppendFormat("\t\t\"z\": {0}\n", SceneTraverserUtils.FloatToString(spawn.position.z + 0f));
                        }
                        else
                        {
                            spawnStr.AppendFormat("\t\"x\": {0},\n", SceneTraverserUtils.FloatToString(spawn.position.x + sceneMeta.rotX));
                            spawnStr.AppendFormat("\t\"y\": {0},\n", SceneTraverserUtils.FloatToString(spawn.position.y + sceneMeta.rotY));
                            spawnStr.AppendFormat("\t\"z\": {0}\n", SceneTraverserUtils.FloatToString(spawn.position.z + sceneMeta.rotZ));
                        }
                        spawnStr.Append("\t}\n\t},\n\t");
                    }
                    spawnStr = spawnStr.Remove(spawnStr.Length - 3, 3); //remove ",\n\t" from the last spawnpoint
                }

                fileTxt = fileTxt.Replace("{SPAWN}", spawnStr.ToString());
                spawnStr.Clear();

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
                        GameObject cam = new GameObject("Camera");
                        cam.transform.parent = sceneMeta.transform;
                        cam.AddComponent<Camera>();
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
            GameObject camera = new GameObject("Camera");
            camera.transform.parent = sceneMeta.transform;
            camera.AddComponent<Camera>();
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
        public void AutoFindExport()
        {
            string projectFolder = Path.Combine(Application.dataPath, "../");
            projectFolder = Path.GetFullPath(projectFolder);

            string exportchild = projectFolder + "Decentraland/";
            
            if (!Directory.Exists(exportchild))
                Directory.CreateDirectory(exportchild);
            
            exportchild = Path.GetFullPath(exportchild);
            autoFindFolders.Clear();
            findInDclDirectory(exportchild);
        }
        private void findInDclDirectory(string directory)
        {
            string[] directories = System.IO.Directory.GetDirectories(directory);
            foreach (string dir in directories)
            {
                if (File.Exists(dir + "/scene.json") && File.Exists(dir + "/src/index.ts"))
                {
                    autoFindFolders.Add(dir);
                }
                else
                {
                    findInDclDirectory(dir);
                }
            }
        }
        private void SetSkyboxTime(int time, float intensity, bool softShadow)
        {
            Light light = FindFirstObjectByType<Light>();
            light.transform.rotation = lightRotation;
            light.color = lightColor;
            light.intensity = intensity;
            light.shadows = softShadow ? LightShadows.Soft : LightShadows.None;

            RenderSettings.skybox.SetTexture("_Tex", Resources.Load<Cubemap>(string.Format("Skybox/{0}h", time.ToString("00"))));
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = skyColor;
            RenderSettings.ambientEquatorColor = equatorColor;
            RenderSettings.ambientGroundColor = groundColor;
            RenderSettings.fog = true;
            RenderSettings.fogStartDistance = 0;
            RenderSettings.fogEndDistance = 800;
            RenderSettings.fogColor = fogColor;
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
                        if (!areaOutOfLandWarning.renderer) return;
                        WarningLineGUI(string.Format("Out of land range : {0}", areaOutOfLandWarning.renderer.name), null, areaOutOfLandWarning.renderer.gameObject);
                    }

                    foreach (var outOfLandWarning in sceneMeta.sceneWarningRecorder.OutOfLandWarnings)
                    {
                        if (!outOfLandWarning.renderer) return;
                        WarningLineGUI(string.Format("Out of land range : {0}", outOfLandWarning.renderer.name), null, outOfLandWarning.renderer.gameObject);
                    }

                    foreach (var warning in sceneMeta.sceneWarningRecorder.UnsupportedShaderWarnings)
                    {
                        if (!warning.renderer) return;
                        var path = AssetDatabase.GetAssetPath(warning.renderer);
                        WarningLineGUI(string.Format("Unsupported shader : {0}", warning.renderer.name), "Only URP Lit Shader is supported", path, new Color(1, 0.7f, 0.1f, 1));
                    }
                    foreach (var warning in sceneMeta.sceneWarningRecorder.InvalidTextureWarnings)
                    {
                        if (!warning.renderer) return;
                        var path = AssetDatabase.GetAssetPath(warning.renderer);
                        WarningLineGUI(string.Format("Invalid texture size : {0}", warning.renderer.name), "Texture sizes must be one of 1,2,4,8,..., 512 (1024 is not allowed, experimental)", path, Color.yellow);
                    }
                    foreach (var warning in sceneMeta.sceneWarningRecorder.InvalidNamingWarnings)
                    {
                        if (!warning.renderer) return;
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
            if (go.GetComponent<TextMesh>() || go.GetComponent<TextMeshPro>()) return;

            var matRenderer = go.GetComponent<MeshRenderer>();
            var matSkRenderer = go.GetComponent<SkinnedMeshRenderer>();

            if (matRenderer && !matRenderer.sharedMaterial.shader.name.Contains("Universal Render Pipeline/Lit"))
            {
                Debug.LogError("WARNING\nGameObject: \"" + matRenderer.gameObject + "\" has a non supported shader: " + matRenderer.sharedMaterial.shader.name + "\nYou should use Polyshader instead");
                if (EditorUtility.DisplayDialog("WARNING", "Material on \"" + matRenderer.gameObject + "\" is NOT suported!!\nYou should use POLYSHADER instead of \n" + matRenderer.sharedMaterial.shader.name, "Ok"))
                {
                }
            }
            else if (matSkRenderer && !matSkRenderer.sharedMaterial.shader.name.Contains("Universal Render Pipeline/Lit"))
            {
                Debug.LogError("WARNING\nGameObject: \"" + matSkRenderer.gameObject + "\" has a non supported shader: " + matSkRenderer.sharedMaterial.shader.name + "\nYou should use Polyshader instead");
                if (EditorUtility.DisplayDialog("WARNING", "Material on \"" + matSkRenderer.gameObject + "\" is NOT suported!!\nYou should use POLYSHADER instead of \n" + matSkRenderer.sharedMaterial.shader.name, "Ok"))
                {
                }
            }
        }
    }
}