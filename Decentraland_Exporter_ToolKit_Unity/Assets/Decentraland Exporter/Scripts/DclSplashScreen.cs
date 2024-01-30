using DCLExport;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class DclSplashScreen : EditorWindow
{
    internal static Color oriColor;
    private int SPACE = 20;
    internal static GUIStyle titleStyle = new GUIStyle();
    private static GUIStyle labelStyle = new GUIStyle();
    private static GUIStyle blackLabelStyle = new GUIStyle();
    private bool showAtStartup;

    static DclSplashScreen()
    {
        EditorApplication.update += RunOnce;
    }

    static void RunOnce()
    {
        if (EditorPrefs.GetBool("showAtStartup"))
            Init();
        
        EditorApplication.update -= RunOnce;
    }

    [MenuItem("Dcl Exporter ToolKit/SplashScreen")]
    static void Init()
    {
        var window = GetWindow<DclSplashScreen>();
        window.minSize = new Vector2(500, 600);
        window.maxSize = new Vector2(500, 600);
        window.titleContent = new GUIContent("Welcome");
        window.Show();
    }
    private void SetUp()
    {
        oriColor = GUI.backgroundColor;

        titleStyle.normal.textColor = Color.white;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.fontSize = 32;
        titleStyle.margin = new RectOffset(0, 0, 0, 0);

        labelStyle.normal.textColor = Color.white;
        labelStyle.alignment = TextAnchor.MiddleLeft;

        blackLabelStyle.normal.textColor = Color.white;
        blackLabelStyle.alignment = TextAnchor.MiddleCenter;
        
        showAtStartup = EditorPrefs.GetBool("showAtStartup");
    }
    private void OnGUI()
    {
        SetUp();
        
        GUILayout.BeginVertical("box");
        GUILayout.Space(SPACE);
        
        TitleGUI();
        
        GUILayout.Label("", GUI.skin.horizontalSlider); //Horizontal line
        GUILayout.Space(SPACE*2);

        ButtonsGUI();

        GUILayout.Label("", GUI.skin.horizontalSlider); //Horizontal line
        GUILayout.Space(SPACE*2);

        ContributionsGUI();

        ControlPanelButton();
        
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        showAtStartup = EditorGUILayout.ToggleLeft("Show at Startup", showAtStartup, GUILayout.Width(120));
        EditorPrefs.SetBool("showAtStartup", showAtStartup);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
    private void TitleGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Box(Resources.Load<Texture>("Icons/dclLogo"));
        GUILayout.Space(SPACE);
        GUILayout.Label("Decentraland", titleStyle);
        GUILayout.Space(SPACE);
        GUILayout.Label("\nSDK7", blackLabelStyle);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(SPACE);

        GUILayout.Label("", GUI.skin.horizontalSlider); //Horizontal line

        GUILayout.Space(SPACE*2);

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Welcome to the Decentraland SDK7 Toolkit for Unity.\n" +
            "This set of tools aims to ease and transform the way creators\n" +
            "develop content for the platform.\n", labelStyle);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
    private void ButtonsGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        
        GUILayout.BeginVertical();
        GUILayout.Label("Get Started", blackLabelStyle);
        if (GUILayout.Button("Docs", GUILayout.Height(32), GUILayout.Width(120)))
        {
            Application.OpenURL("https://decentraland.org/");
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.Label("Stay Updated", blackLabelStyle);
        if (GUILayout.Button("CheckUpdates", GUILayout.Height(32), GUILayout.Width(120)))
        {
            Application.OpenURL("https://github.com/PolygonalMind/dcl-dev-exportersdk7-release/tags");
        }
        GUILayout.EndVertical();
        GUILayout.BeginVertical();
        GUILayout.Label("Contribute", blackLabelStyle);
        if (GUILayout.Button("Feedback", GUILayout.Height(32), GUILayout.Width(120)))
        {
            Application.OpenURL("https://decentraland.org/");
        }
        GUILayout.EndVertical();
        
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.Space(SPACE);
        GUILayout.Label(string.Format("Current Version: {0}", DclExporter.version), blackLabelStyle);
    }
    private void ContributionsGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();

        GUILayout.Label("This project has been developed by PolygonalMind with the help of the\n" +
            "Decentraland DAO. Check both entities contributions here:", blackLabelStyle);

        GUILayout.Space(SPACE);
        
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Polygonal Mind\nWebsite", GUILayout.Height(40), GUILayout.Width(133)))
        {
            Application.OpenURL("https://www.polygonalmind.com/");
        }
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Decentraland Dao\nGrant", GUILayout.Height(40), GUILayout.Width(133)))
        {
            Application.OpenURL("https://decentraland.org/governance/proposal/?id=122c02b0-4b38-11ee-8dc1-47e81c0c49b1");
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
    private void ControlPanelButton()
    {
        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        
        GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
        if (GUILayout.Button("Open Control Panel", GUILayout.Height(40), GUILayout.Width(250)))
        {
            DclExporter.Init();
            GetWindow<DclSplashScreen>().Close();
        }
        GUI.backgroundColor = oriColor;

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
    }
}
