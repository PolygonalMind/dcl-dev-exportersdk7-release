using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using UnityEditor.SearchService;

namespace DCLExport
{
    public class DclExporter : EditorWindow
    {
        #region Variables

        //private string projectName;
        private List<string> autoFindFolders = new List<string>();

        //Export
        private Texture2D thumbnailImage;
        internal static string version = "v240105.1";
        private string exportPath;
        private string newExportPath;
        private string stringColor;
        public static bool GlbExternalTexture = false;

        //Depenencies
        private bool dclProjectExist;
        public static string nodeVersion = "not found";
        public static string npmVersion = "not found";
        public static string gltfVersion = "not found";
        public static string dclCliVersion = "not found";
        public static bool nodeFound;
        public static bool npmFound;
        public static bool gltfFound;
        public static bool dclCliFound;
        public bool updateTrasverser;
        public float timeUpdateTrasverser;

        //GUI
        private int toolBarIndex = 0;
        private const int SPACE = 10;
        private const int defaultWidth = 400;
        private static Vector2 scrollPosition;
        private static GUIStyle titleStyle = new GUIStyle();
        private static GUIStyle labelStyle = new GUIStyle();
        private static GUIStyle blackLabelStyle = new GUIStyle();
        public static Color oriColor;

        //Skybox
        private Quaternion lightRotation;
        private Color lightColor;
        private Color skyColor;
        private Color equatorColor;
        private Color groundColor;
        private Color fogColor;
        
        public static bool showBoundingBoxes = false;
        public static bool showMaxHeight = false;

        //Export function
        private DateTime initialTime;
        public bool exportAditionalCode = true;
        public enum ExportFormat
        {
            GLBCompressedTextures,
            GLTFBin,
            GLBExternalTextures
        }
        public ExportFormat mFormat = new ExportFormat();
        private DclSceneMeta sceneMeta;
        private DateTime nextTimeRefresh;
        private DateTime timmerTraverser;
        #endregion

        public static DclExporter Instance
        {
            get { return GetWindow<DclExporter>(); }
        }
        [MenuItem("Dcl Exporter ToolKit/Control Panel", false, 100)]
        public static void Init()
        {
            var window = GetWindow<DclExporter>();
            window.minSize = new Vector2(450, 400);
            window.maxSize = new Vector2(450, 800);
            window.titleContent = new GUIContent("Control Panel");
            window.Show();

            window.ShowNotification(new GUIContent("Checking for Dependencies"), 0.2f);

            RunCommand.DependencieCheck();
        }
        private void SetUp()
        {
            //Styles Config
            oriColor = GUI.backgroundColor;

            titleStyle.normal.textColor = Color.white;
            titleStyle.alignment = TextAnchor.MiddleCenter;
            titleStyle.fontSize = 32;
            titleStyle.margin = new RectOffset(0, 0, 0, 0);

            labelStyle.normal.textColor = Color.white;
            labelStyle.alignment = TextAnchor.MiddleLeft;

            blackLabelStyle.normal.textColor = Color.white;
            blackLabelStyle.alignment = TextAnchor.MiddleCenter;
            exportPath = EditorPrefs.GetString("DclExportPath");

            //Get Scene Metadata Object
            if (!sceneMeta)
            {
                CheckAndGetDclSceneMetaObject();
            }
        }
        private void Update()
        {
            //Auto Refresh Counter
            if (DateTime.Now > nextTimeRefresh)
            {
                if (updateTrasverser)
                {
                    if (DateTime.Now >= timmerTraverser)
                    {
                        if (!sceneMeta)
                        {
                            CheckAndGetDclSceneMetaObject();
                        }

                        sceneMeta.RefreshStatistics();
                        Repaint();
                        nextTimeRefresh = DateTime.Now.AddSeconds(1);
                        timmerTraverser = DateTime.Now.AddSeconds(timeUpdateTrasverser);
                    }
                }
                else
                {
                    if (timmerTraverser.Second > 0.0f)
                    {
                        timmerTraverser = DateTime.MinValue;
                    }
                }

            }
        }
        void OnInspectorUpdate()
        {
            //Check if the given path exist and is a DCL Project
            dclProjectExist = Directory.Exists(exportPath + "/src") && File.Exists(exportPath + "/scene.json") && File.Exists(exportPath + "/package-lock.json");
            Repaint();
        }
        void OnGUI()
        {
            SetUp();
            
            EditorGUI.BeginChangeCheck();

            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            toolBarIndex = GUILayout.Toolbar(toolBarIndex, new string[] { "Config", "Manager", "Builder", "Exporter"}, GUILayout.Height(25));
            GUI.backgroundColor = oriColor;
            
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            switch (toolBarIndex)
            {
                // CONFIG //
                case 0:
                    TitleGUI();
                    GUILayout.Space(SPACE);
                    HandleDependenciesGUI();
                    GUILayout.Space(SPACE);
                    DependenciesGUI();
                    GUILayout.Space(SPACE);
                    ProjectSetUp();
                    break;
                // MANAGER //
                case 1:
                    if (!dclProjectExist)
                    {
                        ShowNotification(new GUIContent("Please, set up a project folder or create one first"), 0.1f);
                        ProjectSetUp();
                        break;
                    }
                    if (nodeVersion == "not found" || npmVersion == "not found" || gltfVersion == "not found" || dclCliVersion == "not found")
                    {
                        ShowNotification(new GUIContent("Please, check dependencies first"), 0.1f);
                        DependenciesGUI();
                        break;
                    }

                    SceneDataGUI();
                    GUILayout.Space(SPACE);
                    SpawnPointsGUI();
                    GUILayout.Space(SPACE);
                    permissionsGUI();
                    break;
                // BUILDER //
                case 2:
                    if (!dclProjectExist)
                    {
                        ShowNotification(new GUIContent("Please, set up a project folder or create one first"), 0.1f);
                        ProjectSetUp();
                        break;
                    }
                    if (nodeVersion == "not found" || npmVersion == "not found" || gltfVersion == "not found" || dclCliVersion == "not found")
                    {
                        ShowNotification(new GUIContent("Please, check dependencies first"), 0.1f);
                        DependenciesGUI();
                        break;
                    }

                    StatsGUI();
                    GUILayout.Space(SPACE);
                    SkyboxGUI();
                    break;
                // EXPORTER //
                case 3:
                    if (!dclProjectExist)
                    {
                        ShowNotification(new GUIContent("Please, set up a project folder or create one first"), 0.1f);
                        ProjectSetUp();
                        break;
                    }
                    if (nodeVersion == "not found" || npmVersion == "not found" || gltfVersion == "not found" || dclCliVersion == "not found")
                    {
                        ShowNotification(new GUIContent("Please, check dependencies first"), 0.1f);
                        DependenciesGUI();
                        break;
                    }

                    DclPathGUI();
                    GUILayout.Space(SPACE);
                    ProjectCoreGUI();
                    GUILayout.Space(SPACE);
                    ExportRunDebugGUI();
                    break;
            }
            
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndScrollView();
            
            //Footer buttons
            LinkButtons();
            
            if (EditorGUI.EndChangeCheck())
            {
                //EditorUtility.SetDirty(sceneMeta);
                //EditorSceneManager.MarkSceneDirty(sceneMeta.gameObject.scene);
            }
        }

        // SAVE UNITY OPEN SCENES
        private void saveOpenScenes()
        {
            if (EditorUtility.DisplayDialog("Save Open Scenes?", "Do you want to save the open scenes before the export?", "Yes", "No"))
            {
                EditorSceneManager.SaveOpenScenes();
                Debug.Log("===Open scenes saved===");
            }
        }
        
        // FOOTER BUTTONS GUI
        private void LinkButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(Resources.Load<Texture>("Icons/dclLogo"), EditorStyles.helpBox, GUILayout.Height(32), GUILayout.Width(32)))
            {
                Application.OpenURL("https://decentraland.org/");
            }
            if (GUILayout.Button(string.Format("Github Repository:\n{0}", "https://github.com/PolygonalMind/dcl-dev-exportersdk7-release"), EditorStyles.helpBox, GUILayout.Height(32)))
            {
                Application.OpenURL("https://github.com/PolygonalMind/dcl-dev-exportersdk7-release");
            }
            if (GUILayout.Button(string.Format("By PolygonalMind:\n{0}", "https://www.polygonalmind.com/"), EditorStyles.helpBox, GUILayout.Height(32)))
            {
                Application.OpenURL("https://www.polygonalmind.com/");
            }
            EditorGUILayout.EndHorizontal();
        }

        // DECENTRALAND TITLE
        private void TitleGUI()
        {
            EditorGUILayout.BeginVertical("box");

            GUILayout.Space(SPACE);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Box(Resources.Load<Texture>("Icons/dclLogo"));
            GUILayout.Space(SPACE);
            GUILayout.Label("Decentraland", titleStyle);
            GUILayout.Space(SPACE);
            GUILayout.Label("\nSDK7", blackLabelStyle);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Label(string.Format("Current Version: {0}", version), blackLabelStyle);
            GUILayout.Space(SPACE);

            GUILayout.Label("", GUI.skin.horizontalSlider); //Horizontal line

            GUILayout.Space(20f);
            GUILayout.Label("To make this tool run properly and be able to test it in your local\n" +
                "machine you need to install Node.js and Decentraland dependencies.\n" +
                "These assets will allow you to quick test and preview your scene\n" +
                "code in a local machine and also prepare the project for deployment.", blackLabelStyle);
            GUILayout.Space(20f);
            EditorGUILayout.EndVertical();
        }

        // HANDLE DEPENDENCIES GUI
        private void HandleDependenciesGUI()
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical();

            GUILayout.Space(SPACE);
            GUILayout.Label("Follow the next steps and/or hit Check Dependencies\n" +
                "to start using the tool", labelStyle, GUILayout.Width(defaultWidth));
            GUILayout.Space(SPACE);
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("1", "Button", GUILayout.Height(25), GUILayout.Width(25));
            GUILayout.Space(30f);
            GUILayout.Label("Download and install NodeJS\nif you don't have it already.", blackLabelStyle, GUILayout.Width(200));
            GUILayout.Space(30f);
            if (GUILayout.Button("Download", GUILayout.Height(25), GUILayout.Width(100)))
            {
                if (EditorUtility.DisplayDialog("Confirm to open NodeJS website",
                    "This will open NodeJS website in your browser\nAre you sure?", "Yes", "No"))
                {
                    Application.OpenURL("https://nodejs.org");
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(SPACE);

            GUILayout.BeginHorizontal();
            GUILayout.Label("2", "Button", GUILayout.Height(25), GUILayout.Width(25));
            GUILayout.Space(30f);
            GUILayout.Label("Install the GLTF Pipeline, this\nwill allow you to export the assets.", blackLabelStyle, GUILayout.Width(200));
            GUILayout.Space(30f);
            if (GUILayout.Button("Install", GUILayout.Height(25), GUILayout.Width(76)))
            {
                if (EditorUtility.DisplayDialog("GLTF Pipeline",
                    "This will install the GLTF Pipeline in your machine.\nAre you sure?", "Yes", "No"))
                {
                    RunCommand.GltfPipeline();
                }
            }
            GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
            if (GUILayout.Button("X", GUILayout.Height(25), GUILayout.Width(20)))
            {
                RunCommand.UninstallGltfPipeline();
                gltfVersion = null;
                gltfFound = false;
            }
            GUI.backgroundColor = oriColor;
            GUILayout.EndHorizontal();
            
            GUILayout.Space(SPACE);

            GUILayout.BeginHorizontal();
            GUILayout.Label("3", "Button", GUILayout.Height(25), GUILayout.Width(25));
            GUILayout.Space(30f);
            GUILayout.Label("Install the Decentraland CLI\nto be able to run the projects.", blackLabelStyle, GUILayout.Width(200));
            GUILayout.Space(30f);
            if (GUILayout.Button("Install", GUILayout.Height(25), GUILayout.Width(76)))
            {
                if (EditorUtility.DisplayDialog("Decentraland CLI",
                    "This will install the Decentraland CLI in your machine.\nAre you sure?", "Yes", "No"))
                {
                    RunCommand.DclUpdateCLI(exportPath);
                }
            }
            GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
            if (GUILayout.Button("X", GUILayout.Height(25), GUILayout.Width(20)))
            {
                RunCommand.DclUninstallCLI(exportPath);
                dclCliVersion = null;
                dclCliFound = false;
            }
            GUI.backgroundColor = oriColor;
            GUILayout.EndHorizontal();
            
            GUILayout.Space(SPACE);

            GUILayout.BeginHorizontal();
            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            if (GUILayout.Button("Check Dependencies", GUILayout.Height(35), GUILayout.Width(defaultWidth)))
            {
                RunCommand.DependencieCheck();
                ShowNotification(new GUIContent("Checking for Dependencies"), 0.2f);
            }
            GUI.backgroundColor = oriColor;
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        // DEPENDENCIES INFO GUI
        private void DependenciesGUI()
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();

            GUILayout.Label("Decentraland Toolkit Version Control", labelStyle, GUILayout.Width(defaultWidth));
            GUILayout.Space(SPACE);
            
            GUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            GUILayout.Label("NodeJS", blackLabelStyle, GUILayout.Width(80));
            if (nodeFound) stringColor = "<color=#A8EDA8>{0}</color>";
            else stringColor = "<color=#F0637F>{0}</color>";
            GUILayout.Label(string.Format(stringColor, nodeVersion), blackLabelStyle, GUILayout.Width(80));
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical();
            if (npmFound) stringColor = "<color=#A8EDA8>{0}</color>";
            else stringColor = "<color=#F0637F>{0}</color>";
            GUILayout.Label("Npm", blackLabelStyle, GUILayout.Width(80));
            GUILayout.Label(string.Format(stringColor, npmVersion), blackLabelStyle, GUILayout.Width(80));
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical();
            if (gltfFound) stringColor = "<color=#A8EDA8>{0}</color>";
            else stringColor = "<color=#F0637F>{0}</color>";
            GUILayout.Label("Gltf-Pipeline", blackLabelStyle, GUILayout.Width(80));
            GUILayout.Label(string.Format(stringColor, gltfVersion), blackLabelStyle, GUILayout.Width(80));
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical();
            if (dclCliFound) stringColor = "<color=#A8EDA8>{0}</color>";
            else stringColor = "<color=#F0637F>{0}</color>";
            GUILayout.Label("CLI", blackLabelStyle, GUILayout.Width(80));
            GUILayout.Label(string.Format(stringColor, dclCliVersion), blackLabelStyle, GUILayout.Width(80));
            EditorGUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            if (GUILayout.Button("Check Dependencies", GUILayout.Height(35), GUILayout.Width(300)))
            {
                RunCommand.DependencieCheck();
                ShowNotification(new GUIContent("Checking for Dependencies"), 0.2f);
            }
            GUI.backgroundColor = new Color(1f, 0.6f, 0.6f);
            if (GUILayout.Button("Clear", GUILayout.Height(35), GUILayout.Width(100)))
            {
                clearDeps();
            }
            GUI.backgroundColor = oriColor;
            GUILayout.EndHorizontal();

            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

        }

        // PROJECT SETUP, LOCATE OR CREATE DCL PROJECT
        private void ProjectSetUp()
        {
            string folder = Path.GetFullPath(Path.Combine(Application.dataPath, "../")) + "Decentraland/";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);


            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Project Setup", labelStyle, GUILayout.Width(85));
            GUILayout.Label(new GUIContent("[ ? ]", "This can be handled in the Exporter tab of the tool"), labelStyle);
            GUILayout.EndHorizontal();
            GUILayout.Space(SPACE);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Create a New Project", GUILayout.Height(32), GUILayout.Width(133)))
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
            
            if (GUILayout.Button("Locate Project", GUILayout.Height(32), GUILayout.Width(133)))
            {
                newExportPath = EditorUtility.OpenFolderPanel("Select the Decentraland project folder", folder, "");
                if (string.IsNullOrEmpty(newExportPath)) newExportPath = exportPath;
                autoFindFolders.Clear();
            }
            
            GUI.backgroundColor = new Color(1.0f, 0.8f, 0.5f);
            if (GUILayout.Button("Autofind", GUILayout.Height(32), GUILayout.Width(133)))
            {
                AutoFindExport();
            }
            GUI.backgroundColor = oriColor;
            GUILayout.EndHorizontal();

            GUILayout.Space(SPACE);

            if (dclProjectExist) stringColor = "<color=#A8EDA8>{0}</color>";
            else stringColor = "<color=#F0637F>{0}</color>";
            
            if(GUILayout.Button(string.Format("Path:\n" + stringColor, newExportPath), labelStyle, GUILayout.Width(defaultWidth)))
            {
                Application.OpenURL(newExportPath);
            }
            
            GUILayout.Space(SPACE);

            if (!Directory.Exists(exportPath) || !dclProjectExist)
            {
                GUILayout.Label(string.Format("<color=#F0637F>{0}</color>", "Select a valid Decentraland project root folder to continue"), blackLabelStyle);
                GUILayout.Space(SPACE);
            }
            
            //AUTOFIND SLOTS
            if (autoFindFolders.Count > 0)
            {
                GUI.backgroundColor = new Color(1.0f, 0.8f, 0.5f);
                GUILayout.Space(SPACE);
                for (int i = 0; i < autoFindFolders.Count; i++)
                {
                    if (GUILayout.Button(Path.GetFileName(Path.GetDirectoryName(autoFindFolders[i])) + "\\" + Path.GetFileName(autoFindFolders[i]), GUILayout.Height(25)))
                    {
                        newExportPath = autoFindFolders[i];
                        exportPath = newExportPath;
                        EditorPrefs.SetString("DclExportPath", newExportPath);
                        ShowNotification(new GUIContent("Path: " + Path.GetFileName(Path.GetDirectoryName(autoFindFolders[i])) + "\\" + Path.GetFileName(autoFindFolders[i])), 0.2f);
                        autoFindFolders.Clear();
                    }
                }
                GUILayout.Space(SPACE);
                GUI.backgroundColor = oriColor;
            }

            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            if (newExportPath != exportPath)
            {
                exportPath = newExportPath;
                EditorPrefs.SetString("DclExportPath", newExportPath);
            }
        }

        // DCL PATH GUI
        private void DclPathGUI()
        {
            string folder = Path.GetFullPath(Path.Combine(Application.dataPath, "../")) + "Decentraland/";
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);


            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(GUILayout.Width(defaultWidth));

            GUILayout.BeginHorizontal();
            GUILayout.Label("Current Export Path", labelStyle);
            if (GUILayout.Button("Open Folder", GUILayout.Width(130)))
            {
                Application.OpenURL(exportPath);
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(SPACE);

            stringColor = "<color=#A8EDA8>{0}</color>";
            if (GUILayout.Button(string.Format(stringColor, newExportPath), labelStyle, GUILayout.Width(defaultWidth)))
            {
                Application.OpenURL(newExportPath);
            }
            
            GUILayout.Space(SPACE);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Create a New Project", GUILayout.Height(32), GUILayout.Width(133)))
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

            if (GUILayout.Button("Locate Project", GUILayout.Height(32), GUILayout.Width(133)))
            {
                newExportPath = EditorUtility.OpenFolderPanel("Select the Decentraland project folder", folder, "");
                if (string.IsNullOrEmpty(newExportPath)) newExportPath = exportPath;
                autoFindFolders.Clear();
            }

            GUI.backgroundColor = new Color(1.0f, 0.8f, 0.5f);
            if (GUILayout.Button("Autofind", GUILayout.Height(32), GUILayout.Width(133)))
            {
                AutoFindExport();
            }
            GUI.backgroundColor = oriColor;
            GUILayout.EndHorizontal();

            GUILayout.Space(SPACE);

            if (!Directory.Exists(exportPath) || !dclProjectExist)
            {
                GUILayout.Label(string.Format("<color=#F0637F>{0}</color>", "Select a valid Decentraland project root folder to continue"), blackLabelStyle);
                GUILayout.Space(SPACE);
            }

            //AUTOFIND SLOTS
            if (autoFindFolders.Count > 0)
            {
                GUI.backgroundColor = new Color(1.0f, 0.8f, 0.5f);
                GUILayout.Space(SPACE);
                for (int i = 0; i < autoFindFolders.Count; i++)
                {
                    if (GUILayout.Button(Path.GetFileName(Path.GetDirectoryName(autoFindFolders[i])) + "\\" + Path.GetFileName(autoFindFolders[i]), GUILayout.Height(25)))
                    {
                        newExportPath = autoFindFolders[i];
                        exportPath = newExportPath;
                        EditorPrefs.SetString("DclExportPath", newExportPath);
                        ShowNotification(new GUIContent("Path: " + Path.GetFileName(Path.GetDirectoryName(autoFindFolders[i])) + "\\" + Path.GetFileName(autoFindFolders[i])), 0.2f);
                        autoFindFolders.Clear();
                    }
                }
                GUILayout.Space(SPACE);
                GUI.backgroundColor = oriColor;
            }

            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            if (newExportPath != exportPath)
            {
                exportPath = newExportPath;
                EditorPrefs.SetString("DclExportPath", newExportPath);
            }
        }

        // DCL PROJECT GUI
        private void ProjectCoreGUI()
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(GUILayout.Width(defaultWidth));
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Project Core", labelStyle);
            if (dclCliFound) stringColor = "<color=#A8EDA8>{0}</color>";
            else stringColor = "<color=#F0637F>{0}</color>";
            GUILayout.Label(string.Format("CLI " + stringColor, dclCliVersion), labelStyle);
            GUILayout.EndHorizontal();

            GUILayout.Space(SPACE);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Init Dcl Project", GUILayout.Height(32)))
                toolBarIndex = 0;
            if (GUILayout.Button("Update CLI", GUILayout.Height(32)))
                toolBarIndex = 0;
            if (GUILayout.Button("Update Dcl SDK", GUILayout.Height(32)))
                toolBarIndex = 0;
            GUILayout.EndHorizontal();
            
            GUILayout.Space(SPACE);

            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        // EXPORT & RUN GUI
        void ExportRunDebugGUI()
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(GUILayout.Width(defaultWidth));
            
            GUILayout.Label("Export, Run & Debug", labelStyle);
            GUILayout.Space(SPACE);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Export 3D Entities as", GUILayout.Width(120));
            GUILayout.Label(new GUIContent("[ ? ]", "GLB are way faster and lightweight for the engine.\n\n" +
                "While GLTF + .bin offers more editing flexibility.\n\n" +
                "GLB + Textures is a good fit for modular enviroments that reuse a lot of textures across entities."), labelStyle);
            GUILayout.FlexibleSpace();
            mFormat = (ExportFormat)EditorGUILayout.EnumPopup("", mFormat);
            GUILayout.EndHorizontal();
            
            GUILayout.Space(SPACE);

            // EXPORT BUTTONS //
            GUI.backgroundColor = new Color(1f, 0.8f, 1f);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Export 3D Entities", GUILayout.Height(26), GUILayout.Width(133)))
            {
                if (EditorUtility.DisplayDialog("Export 3D", "Do you want to export the 3D entities?", "Yes", "No"))
                {
                    saveOpenScenes();
                    Export(false, true, false);
                }
            }
            if (GUILayout.Button("Export index.ts", GUILayout.Height(26), GUILayout.Width(133)))
            {
                if (EditorUtility.DisplayDialog("Export index.ts", "Do you want to export the project index.ts script?", "Yes", "No"))
                {
                    saveOpenScenes();
                    Export(true, false, false);
                }
            }
            if (GUILayout.Button("Export scene.js", GUILayout.Height(26), GUILayout.Width(133)))
            {
                if (EditorUtility.DisplayDialog("Export", "Do you want to export the project metadata to the scene.js?", "Yes", "No"))
                {
                    saveOpenScenes();
                    Export(false, false, true);
                }
            }
            EditorGUILayout.EndHorizontal();
            
            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            if (GUILayout.Button("Export", GUILayout.Height(32)))
            {
                if (EditorUtility.DisplayDialog("Full Export", "Do you want to do a full export?", "Yes", "No"))
                {
                    saveOpenScenes();
                    Export();
                }
            }
            GUI.backgroundColor = oriColor;

            GUILayout.Space(SPACE);
            
            GUILayout.Label(string.Format("Project  <color=#A8EDA8>{0}</color>", Path.GetFileName(exportPath)), labelStyle);

            GUILayout.Space(SPACE);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open Folder", GUILayout.Height(32), GUILayout.Width(133)))
            {
                Application.OpenURL(exportPath);
            }
            if (GUILayout.Button("Open /unity_assets", GUILayout.Height(32), GUILayout.Width(133)))
            {
                Application.OpenURL(exportPath + "/unity_assets");
            }
            if (GUILayout.Button("Open /images", GUILayout.Height(32), GUILayout.Width(133)))
            {
                Application.OpenURL(exportPath + "/images");
            }
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Open package.js", GUILayout.Height(32), GUILayout.Width(133)))
            {
                Application.OpenURL(exportPath + "/package.json");
            }
            if (GUILayout.Button("Open index.ts", GUILayout.Height(32), GUILayout.Width(133)))
            {
                Application.OpenURL(exportPath + "/src/index.ts");
            }
            if (GUILayout.Button("Open scene.js", GUILayout.Height(32), GUILayout.Width(133)))
            {
                Application.OpenURL(exportPath + "/scene.json");
            }
            GUILayout.EndHorizontal();
            
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            if (GUILayout.Button("Run Local", GUILayout.Height(32), GUILayout.Width(133)))
            {
                RunMenu("");
            }
            GUI.backgroundColor = oriColor;
            GUILayout.EndHorizontal();
            GUILayout.Space(SPACE);

            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        // AUTOMATIC DIRECTORY CREATION
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

        // STATISTICS GUI
        void StatsGUI()
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(GUILayout.Width(defaultWidth));

            GUILayout.BeginHorizontal();
            GUILayout.Label("Scene Stats", labelStyle, GUILayout.Width(80));
            GUILayout.Label(new GUIContent("[ ? ]", "Scene limitations are based on ECS limitations"), labelStyle);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();
            GUILayout.Space(SPACE);
            showBoundingBoxes = EditorGUILayout.ToggleLeft("Show bounds on selection", showBoundingBoxes);
            //GUILayout.Space(SPACE);
            //showMaxHeight = EditorGUILayout.ToggleLeft("Show max height", showMaxHeight);
            GUILayout.EndVertical();
            
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("ECS Limitations", GUILayout.Width(100)))
            {
                Application.OpenURL("https://docs.decentraland.org/creator/development-guide/scene-limitations/");
            }
            GUILayout.EndHorizontal();
            
            var n = sceneMeta.parcels.Count;
            var sceneStatistics = sceneMeta.sceneStatistics;
            StatisticsLineGUI("Triangles", sceneStatistics.triangleCount, LimitationConfigs.GetMaxTriangles(n));
            StatisticsLineGUI("Bodies", sceneStatistics.bodyCount, LimitationConfigs.GetMaxBodies(n));
            StatisticsLineGUI("Entities", sceneStatistics.entityCount, LimitationConfigs.GetMaxTriangles(n));
            StatisticsLineGUI("Materials", (long)sceneStatistics.materialCount, LimitationConfigs.GetMaxMaterials(n));
            StatisticsLineGUI("Textures", (long)sceneStatistics.textureCount, LimitationConfigs.GetMaxTextures(n));
            StatisticsLineGUI("Height", (long)sceneStatistics.maxHeight, LimitationConfigs.GetMaxHeight(n));
            GUILayout.BeginHorizontal();
            GUILayout.Label("GLTFs");
            GUILayout.FlexibleSpace();
            GUILayout.Label(sceneStatistics.gltfCount.ToString());
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("DCL Primitives");
            GUILayout.FlexibleSpace();
            GUILayout.Label(sceneStatistics.primitiveCount.ToString());
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();
            
            GUILayout.Space(SPACE);

            GUILayout.BeginHorizontal();
            updateTrasverser = EditorGUILayout.ToggleLeft("", updateTrasverser, GUILayout.Width(20));
            GUILayout.Label("Autorefresh every ", GUILayout.Width(105));
            timeUpdateTrasverser = EditorGUILayout.FloatField(timeUpdateTrasverser, GUILayout.Width(40));
            GUILayout.Label("seconds", GUILayout.Width(50));
            GUILayout.FlexibleSpace();
            GUI.backgroundColor = new Color(0.6f, 1.0f, 0.6f);
            if (GUILayout.Button("Refresh", GUILayout.Height(25), GUILayout.Width(100)))
            {
                sceneMeta.RefreshStatistics();
                sceneMeta.getParcelSetVolumes();
            }
            GUI.backgroundColor = oriColor;
            GUILayout.EndHorizontal();

            WarningsGUI();

            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        // SET SKYBOX & ILLUMINATION GUI
        private void SkyboxGUI()
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(GUILayout.Width(defaultWidth));
            
            GUILayout.Label("Illumination and Reflections Preview", labelStyle);
            GUILayout.Space(SPACE);
            GUILayout.BeginHorizontal();
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
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
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
            GUILayout.EndHorizontal();
            
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        // SCENE METADATA GUI
        private void SceneDataGUI()
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            EditorGUILayout.BeginVertical(GUILayout.Width(defaultWidth));
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Scene Metadata", labelStyle);
            GUILayout.Label(new GUIContent("[ ? ]", "Scene Metadata is used during your playtime in Decentraland to show to the user the Scene name, it's preview and creator"), labelStyle, GUILayout.Width(300));
            GUILayout.EndHorizontal();
            GUILayout.Space(SPACE);

            if (GUILayout.Button("Open Parcel Manager", GUILayout.Height(16), GUILayout.Width(defaultWidth)))
            {
                ParcelManager.Init();  
            }
            EditorGUILayout.LabelField("Land General Info", EditorStyles.boldLabel);
            sceneMeta.landTitle = EditorGUILayout.TextField("Land Name", sceneMeta.landTitle); // title of the land
            EditorGUILayout.LabelField("Land Description");
            GUIStyle style = new GUIStyle(EditorStyles.textArea);
            style.wordWrap = true;
            sceneMeta.landInfo = EditorGUILayout.TextArea(sceneMeta.landInfo, style);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Land Thumbnail", GUILayout.Width(100));
            GUILayout.Label(new GUIContent("[ ? ]", "If you let this field empty, the thumbnail image will be the default"), labelStyle);
            thumbnailImage = (Texture2D)EditorGUILayout.ObjectField(thumbnailImage, typeof(Texture2D), true);
            GUILayout.EndHorizontal();
            //sceneMeta.landImg = EditorGUILayout.TextField("Land Thumbnail", sceneMeta.landImg); // url or uri to a jpg containing the thumbnail to be seen from the Atlas

            GUILayout.Space(SPACE);
            
            EditorGUILayout.LabelField("Land Owner Info", EditorStyles.boldLabel);
            sceneMeta.ethAddress = EditorGUILayout.TextField("Address", sceneMeta.ethAddress); //"owner" stands for ETH Address holding this LAND
            sceneMeta.contactName = EditorGUILayout.TextField("Name", sceneMeta.contactName); // Name of the owner
            sceneMeta.email = EditorGUILayout.TextField("Email", sceneMeta.email); // Email or contact form of the owner

            EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        
        // SPAWN POINT/AREA GUI
        private void SpawnPointsGUI()
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(GUILayout.Width(defaultWidth));
            
            GUILayout.Label("Spawn Points", labelStyle);
            GUILayout.Space(SPACE);
            GUILayout.Label("Locate Spawns in the scene and assign them here. Using the property\nsize will turn your Spawn into a Spawn Area");
            
            foreach (Transform transform in sceneMeta.spawnPoints)
            {
                EditorGUILayout.LabelField(transform.name + ": ", EditorStyles.boldLabel);
                GUILayout.BeginHorizontal();
                transform.localPosition = EditorGUILayout.Vector3Field("Position", transform.localPosition, GUILayout.Width(190));
                transform.localScale = EditorGUILayout.Vector3Field("Size", transform.localScale, GUILayout.Width(190));
                GUILayout.EndHorizontal();
            }
            
            GUILayout.Space(SPACE);
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add Spawn", GUILayout.MaxWidth(380)))
            {
                GameObject go = new GameObject();
                go.name = "spawnPoint" + (sceneMeta.spawnPoints.Count > 0 ? sceneMeta.spawnPoints.Count : 0) + "DCL";
                go.transform.parent = sceneMeta.gameObject.transform;
                sceneMeta.spawnPoints.Add(go.transform);
            }
            if (sceneMeta.spawnPoints.Count > 1)
            {
                if (GUILayout.Button("Remove Last", EditorStyles.miniButtonRight, GUILayout.Width(120)))
                {
                    DestroyImmediate(sceneMeta.spawnPoints[sceneMeta.spawnPoints.Count - 1].gameObject);
                    sceneMeta.spawnPoints.RemoveAt(sceneMeta.spawnPoints.Count - 1);
                    sceneMeta.spawnPoints.Capacity -= 1;
                }
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(SPACE);
            
            GUILayout.BeginHorizontal();
            sceneMeta.allowCCT = EditorGUILayout.ToggleLeft("Use Custom Look at direction" ,sceneMeta.allowCCT, GUILayout.Width(190));
            GUILayout.Label(new GUIContent("[ ? ]", "If false, the player will be oriented to the +Z axis"), labelStyle);
            GUILayout.EndHorizontal();
            
            if (sceneMeta.allowCCT)
            {
                sceneMeta.rotX = EditorGUILayout.FloatField("Set X", sceneMeta.rotX);
                sceneMeta.rotY = EditorGUILayout.FloatField("Set Y", sceneMeta.rotY);
                sceneMeta.rotZ = EditorGUILayout.FloatField("Set Z", sceneMeta.rotZ);
            }
            GUILayout.Space(SPACE);
            
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        // SPECIAL PERMISSIONS GUI
        void permissionsGUI()
        {
            GUILayout.BeginHorizontal("box");
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(GUILayout.Width(defaultWidth));

            GUILayout.Label("Special Permissions", labelStyle);
            
            GUILayout.Space(SPACE);
            
            GUILayout.Label("Some gameplay configurations require special permissions\nfrom the scene to the player");

            GUILayout.Space(SPACE);
            
            sceneMeta.voiceChatEnabled = EditorGUILayout.ToggleLeft("Enable voice chat", sceneMeta.voiceChatEnabled);
            sceneMeta.portableExpEnabled = EditorGUILayout.ToggleLeft("Enable portable experiences", sceneMeta.portableExpEnabled);

            GUILayout.Space(SPACE);
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        
        // RUN LOCAL PREVIEW
        private void RunMenu(object str)
        {
            string type = (string)str;
            
            if (EditorUtility.DisplayDialog("Confirm to run Dcl", "Are you sure to run the Dcl scene?", "Yes", "No"))
            {
                RunCommand.DclRunStart(exportPath, type);
                ShowNotification(new GUIContent("Decentraland is Starting"), 0.1f);
            }
        }
        
        // CLEAR DEPENDENCIES
        public static void clearDeps()
        {
            nodeVersion = "not found";
            npmVersion = "not found";
            gltfVersion = "not found";
            dclCliVersion = "not found";

            nodeFound = false;
            npmFound = false;
            gltfFound = false;
            dclCliFound = false;
        }

        // FULL EXPORT
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

        // EXPORT 3D MODELS
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
                    case ExportFormat.GLTFBin:
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
                    path = path.Remove(path.Length - 6, 6) + relPath; //Remove /Assets
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

        // EXPORT METADATA
        private void ExportSceneJson()
        {
            //Copy image to exportPath/images/
            if (thumbnailImage != null)
            {
                var relPath = AssetDatabase.GetAssetPath(thumbnailImage);
                var path = Application.dataPath; //<path to project folder>/Assets
                path = path.Remove(path.Length - 6, 6) + relPath;
                var toPath = exportPath + "\\images\\" + thumbnailImage.name + ".png";
                toPath = toPath.Replace(" ", string.Empty);
                var directoryPath = Path.GetDirectoryName(toPath);
                if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
                File.Copy(path, toPath, true);
            }
            //Write scene.json
            {
                var fileTxt = GetSceneJsonFileTemplate();
                fileTxt = fileTxt.Replace("{LAND_TITLE}", sceneMeta.landTitle);
                fileTxt = fileTxt.Replace("{LAND_INFO}", sceneMeta.landInfo);
                if (thumbnailImage != null)
                    fileTxt = fileTxt.Replace("{LAND_IMG}", "images/" + thumbnailImage.name + ".png");
                else
                    fileTxt = fileTxt.Replace("{LAND_IMG}", "images/scene-thumbnail.png");

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
                    fileTxt = fileTxt.Replace("{VOICECHAT}", "\"enabled\"");
                else
                    fileTxt = fileTxt.Replace("{VOICECHAT}", "\"disabled\"");
                
                if (sceneMeta.portableExpEnabled == true)
                    fileTxt = fileTxt.Replace("{PORTABLEEXP}", "\"enabled\"");
                else
                    fileTxt = fileTxt.Replace("{PORTABLEEXP}", "\"disabled\"");

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
        
        // GET SCENE.JS TEMPLATE
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

        //GET PARCELS
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

        // GET DCL SCENE META OBJECT
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

        // SCENE LIMITATIONS GUI
        void StatisticsLineGUI(string indexName, long leftValue, long rightValue)
        {
            var oriColor = GUI.contentColor;
            GUILayout.BeginHorizontal();
            if (leftValue > rightValue)
            {
                GUILayout.Label(DclEditorSkin.WarningIconSmall, GUILayout.Width(20));
                GUI.contentColor = Color.yellow;
            }
            GUILayout.Label(indexName);
            GUILayout.FlexibleSpace();
            GUILayout.Label(string.Format("{0} / {1}", leftValue, rightValue));
            GUILayout.EndHorizontal();
            GUI.contentColor = oriColor;
        }

        // AUTOFIND DCL PROJECS
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

        // SET SKYBOX TIME
        private void SetSkyboxTime(int time, float intensity, bool softShadow)
        {
            Light light = FindFirstObjectByType<Light>();
            light.transform.rotation = lightRotation;
            light.color = lightColor;
            light.intensity = intensity;
            light.shadows = softShadow ? LightShadows.Soft : LightShadows.None;

            RenderSettings.skybox = Resources.Load<Material>("Skybox/Materials/SkyboxMat");
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

        //WARNNG GUI FOR STATISTICS
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
                if (hintMessage != null) ShowNotification(new GUIContent(hintMessage), 0.1f);
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
                if (hintMessage != null) ShowNotification(new GUIContent(hintMessage), 0.1f);
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