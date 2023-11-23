using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using TMPro;
using System.Reflection;

namespace DCLExport
{
    public class ParcelManager : EditorWindow
    {
        private static Vector2 scrollPosition;

        //TEST
        static string startingParcel;
        private static string editParcelsText;

        public static float gridCount = 3;
        public static int gridColumn = 3;
        public static int gridRow = 3;

        public static List<string> stringList = new List<string>();

        //Base 

        private static GameObject go;
        private static GUIStyle style = new GUIStyle();
        private static GUIStyle style2 = new GUIStyle();
        private static SerializedObject so;
        SerializedProperty sp;

        private static DclSceneMeta sceneMeta;
        private static bool editParcelsMode;
        private static string parcelsText;



        // Add menu named "My Window" to the Window menu
        [MenuItem("Dcl Exporter ToolKit/Parcel Manager")]
        static void Init()
        {

            // Get existing open window or if none, make a new one:
            ParcelManager window = (ParcelManager)EditorWindow.GetWindow(typeof(ParcelManager));
            window.titleContent = new GUIContent("ParcelManager");
            window.minSize = new Vector2(325, 400);
            window.maxSize = new Vector2(325, 800);
            window.Show();

            // window.index = 0;

            startingParcel = ParcelToStringBuilder(sceneMeta.parcels[0]).ToString();
        }

        private void SetUp()
        {
            if (!sceneMeta)
            {
                CheckAndGetDclSceneMetaObject();
            }
            if (string.IsNullOrEmpty(startingParcel))
            {
                startingParcel = ParcelToStringBuilder(sceneMeta.parcels[0]).ToString();
            }


            so = new SerializedObject(FindObjectOfType<DclSceneMeta>());

            style2.normal.textColor = Color.white;
            style2.alignment = TextAnchor.MiddleLeft;

            var sb = new StringBuilder();
            if (sceneMeta.parcels.Count > 0)
            {
                sb.Append(ParcelToStringBuilder(sceneMeta.parcels[0]));
                for (int i = 1; i < sceneMeta.parcels.Count; i++)
                {
                    sb.Append('\n').Append(ParcelToStringBuilder(sceneMeta.parcels[i]));
                }
            }

            parcelsText = sb.ToString();

            gridRow = (int)gridCount;
            gridColumn = (int)gridCount;

            if (gridColumn > 12)
            {
                gridColumn = 12;
            }
            else if (gridColumn < 0)
            {
                gridColumn = 0;
            }
            if (gridRow > 12)
            {
                gridRow = 12;
            }
            else if (gridRow < 0)
            {
                gridRow = 0;
            }
        }
        public void OnGUI()
        {

            SetUp();

            EditorGUI.BeginChangeCheck();

            parcelsDataGUI();

            GUILayout.Space(10);

            fixedSizeParcelsGUI();

            GUILayout.Space(10);

            GUILayout.BeginVertical();
            
            buttonGrid();
            
            GUILayout.EndVertical();
            
            GUILayout.Space(10);

            createButtonGUI();
            
            parcelGUI();

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(sceneMeta);
                EditorSceneManager.MarkSceneDirty(sceneMeta.gameObject.scene);
            }
        }
        void CheckAndGetDclSceneMetaObject()
        {
            var rootGameObjects = new List<GameObject>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var roots = SceneManager.GetSceneAt(i).GetRootGameObjects();
                rootGameObjects.AddRange(roots);
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

            //Did not find .dclManager object. Create one.
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

        void createButton(int index)
        {
            var boton = GUILayout.Button(stringList[index], GUILayout.Width(20), GUILayout.Height(20));
            if (boton)
            {
                if (stringList[index] == "")
                {
                    stringList[index] = "x";
                }
                else
                {
                    stringList[index] = "";
                }
                //Debug.Log("Clicked index: " + index);
                //GUILayout.Button(stringList[u + i].ToString(), GUILayout.Width(20), GUILayout.Height(20));
            }
        }
        public void buttonGrid()
        {
            GUILayout.Label("Create a custom parcel in the grid: ");
            
            stringList.Add("");
            int index = 1;
            for (int i = 1; i <= gridRow; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(15);
                for (int u = 0; u < gridColumn; u++)
                {
                    if (index <= ((gridColumn * gridRow)) && index >= 0)
                    {
                        stringList.Add("");
                        
                    }
                    else if (index > (gridColumn * gridRow))
                    {
                        stringList.Clear();
                        index = 0;
                    }
                   
                    //Si no es la casilla 0 y tiene un 0, este se borra

                    if (stringList[index] == "0" && index != (gridColumn * gridRow) - gridColumn + 1)
                    {
                        stringList[index] = "";
                    }
                    
                    if (index == (gridColumn * gridRow) - gridColumn + 1)
                    {
                        stringList[index] = "0";
                    }
                    if(index >=0 && index <= stringList.Count)
                    {
                        createButton(index);
                    }
                    index++;
                }
                //Debug.Log("Creating buttons line number: " + i);
                GUILayout.EndHorizontal();
            }
        }

        void createParcel(string startingParcel)
        {
            CheckAndGetDclSceneMetaObject();
            int i = 0;
            int deepCount = 1;
            int relativeIndex = 0;

            int x = 0;
            int y = 0;
            int startingParcelX = 0;
            int startingParcelY = 0;

            var parcels = sceneMeta.parcels;

            var newParcels = new List<ParcelCoordinates>();


            createBase(startingParcel, newParcels, x, y, startingParcelX, startingParcelY);

            foreach (string celda in stringList)
            {
                /* if(celda == "0")
                {
                    relativeIndex = i;
                    restColumn(relativeIndex, deepCount, x, y, newParcels);
                }*/
                if (celda == "x" /*&& celda != "0"*/)
                {
                    relativeIndex = i;
                    
                    restColumn(relativeIndex, deepCount, x, y, newParcels, startingParcelX, startingParcelY);
                }
                i++;
            }

            parcels = newParcels;
            sceneMeta.parcels = parcels;
            EditorUtility.SetDirty(sceneMeta);
            EditorSceneManager.MarkSceneDirty(sceneMeta.gameObject.scene);
        }
        void createBase(string startingParcel, List<ParcelCoordinates> newParcels,int x,int y, int startingX, int startingY)
        {
            ParseTextToCoordinates(startingParcel, newParcels, x, y, startingX, startingY);

            //newParcels.Add(new ParcelCoordinates(x, y));

        }
        void restColumn(int relativeIndex, int deepCount, int x, int y, List<ParcelCoordinates> newParcels, int startingParcelX, int startingParcelY)
        {
            var elements = startingParcel.Trim().Split(',');
            if (elements.Length != 2)
            {
                throw new Exception("A line does not have exactly 2 elements!");
            }
            startingParcelX = int.Parse(elements[0]);
            startingParcelY = int.Parse(elements[1]);

            if (relativeIndex <= gridColumn && relativeIndex > 0)
            {
                y = (gridColumn - deepCount) + startingParcelY;
                x = (relativeIndex - 1) + startingParcelX;
                newParcels.Add(new ParcelCoordinates(x, y));
                //restColumn(relativeIndex, deepCount, x, y, newParcels);
            }
            else
            {
                relativeIndex = relativeIndex - gridColumn;
                deepCount++;
                restColumn(relativeIndex, deepCount, x, y, newParcels, startingParcelX, startingParcelY);
            }
        }

        public static StringBuilder ParcelToStringBuilder(ParcelCoordinates parcel)
        {
            return new StringBuilder().Append(parcel.x).Append(',').Append(parcel.y);
        }

        public void createButtonGUI()
        {
            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            var reload = GUILayout.Button("Create Parcel", GUILayout.Height(25));
            if (reload)
            {
                createParcel(startingParcel);
            }
            GUI.backgroundColor = Color.white;
            
            GUILayout.Space(10);
        }

        public void fixedSizeParcelsGUI()
        {
            GUILayout.Label("Create fixed size parcel: ");

            GUILayout.BeginHorizontal();

            GUI.backgroundColor = new Color(0.6f, 1f, 0.6f);
            var button2_2 = GUILayout.Button("2x2", GUILayout.Height(25));
            if (button2_2)
            {
                try
                {
                    stringList.Clear();
                    var newParcels = new List<ParcelCoordinates>();
                    create2x2(startingParcel, newParcels);
                    sceneMeta.parcels = newParcels;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                    EditorUtility.DisplayDialog("Coordinates format is not correct. Should be like below",
                        @"57,-11
                        57,-12
                        57,-13", "OK");
                }
                gridCount = 2;
            }
            var button3_3 = GUILayout.Button("3x3", GUILayout.Height(25));
            if (button3_3)
            {
                try
                {
                    stringList.Clear();
                    var newParcels = new List<ParcelCoordinates>();
                    create3x3(startingParcel, newParcels);
                    sceneMeta.parcels = newParcels;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                    EditorUtility.DisplayDialog("Coordinates format is not correct. Should be like below",
                        @"57,-11
                        57,-12
                        57,-13", "OK");
                }
                gridCount = 3;
            }
            var button4_4 = GUILayout.Button("4x4", GUILayout.Height(25));
            if (button4_4)
            {
                try
                {
                    stringList.Clear();
                    var newParcels = new List<ParcelCoordinates>();
                    create4x4(startingParcel, newParcels);
                    sceneMeta.parcels = newParcels;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                    EditorUtility.DisplayDialog("Coordinates format is not correct. Should be like below",
                        @"57,-11
                        57,-12
                        57,-13", "OK");
                }
                gridCount = 4;
            }
            var button5_5 = GUILayout.Button("5x5", GUILayout.Height(25));
            if (button5_5)
            {
                try
                {
                    stringList.Clear();
                    var newParcels = new List<ParcelCoordinates>();
                    create5x5(startingParcel, newParcels);
                    sceneMeta.parcels = newParcels;
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                    EditorUtility.DisplayDialog("Coordinates format is not correct. Should be like below",
                        @"57,-11
                        57,-12
                        57,-13", "OK");
                }
                gridCount = 5;
            }
            GUI.backgroundColor = Color.white;

            GUILayout.EndHorizontal();
        }
        
        public void parcelsDataGUI()
        {
            GUILayout.Label("Parcels", EditorStyles.boldLabel);

            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            startingParcel = EditorGUILayout.TextField("Starting Parcel", startingParcel, GUILayout.Width(260));
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Grid Size: ");
            GUILayout.Label("1"); 
            float width = GUILayout.HorizontalSlider(gridCount, 1, 10, GUILayout.Width(150));
            if (width != gridCount)
            {
                stringList.Clear();
                gridCount = width;
            }
            GUILayout.Space(10);
            GUILayout.Label("10");
            GUILayout.EndHorizontal();
        }
        public List<ParcelCoordinates> create2x2(string text, List<ParcelCoordinates> coordinates)
        {
            int x = 0;
            int y = 0;

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


                x = int.Parse(elements[0]);
                y = int.Parse(elements[1]);
                coordinates.Add(new ParcelCoordinates(x, y));
            }

            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            y++;
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));

            return coordinates;
        }
        public List<ParcelCoordinates> create3x3(string text, List<ParcelCoordinates> coordinates)
        {

            int x = 0;
            int y = 0;

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


                x = int.Parse(elements[0]);
                y = int.Parse(elements[1]);
                coordinates.Add(new ParcelCoordinates(x, y));
            }

            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            y++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            y++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));

            return coordinates;
        }
        public List<ParcelCoordinates> create4x4(string text, List<ParcelCoordinates> coordinates)
        {

            int x = 0;
            int y = 0;

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


                x = int.Parse(elements[0]);
                y = int.Parse(elements[1]);
                coordinates.Add(new ParcelCoordinates(x, y));
            }

            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            y++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            y++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            y++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));

            return coordinates;
        }
        public List<ParcelCoordinates> create5x5(string text, List<ParcelCoordinates> coordinates)
        {

            int x = 0;
            int y = 0;

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


                x = int.Parse(elements[0]);
                y = int.Parse(elements[1]);
                coordinates.Add(new ParcelCoordinates(x, y));
            }

            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            y++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            y++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            y++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            x--;
            coordinates.Add(new ParcelCoordinates(x, y));
            y++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));
            x++;
            coordinates.Add(new ParcelCoordinates(x, y));

            return coordinates;
        }
        public void parcelGUI()
        {
            EditorGUILayout.BeginVertical("box");
            var parcels = sceneMeta.parcels;
            EditorGUILayout.BeginHorizontal();
            
            GUILayout.Label(string.Format("Parcels ({0})", parcels.Count));
            if (editParcelsMode)
            {
                if (GUILayout.Button("Save"))
                {
                    CheckAndGetDclSceneMetaObject();
                    try
                    {
                        var newParcels = new List<ParcelCoordinates>();
                        ParseText(editParcelsText, newParcels);
                        parcels = newParcels;
                        sceneMeta.parcels = parcels;
                        editParcelsMode = false;
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e.Message);
                        EditorUtility.DisplayDialog("Coordinates format is not correct. Should be like below",
                            @"57,-11
                            57,-12
                            57,-13", "OK");
                    }

                    EditorUtility.SetDirty(sceneMeta);
                    EditorSceneManager.MarkSceneDirty(sceneMeta.gameObject.scene);

                }

                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    editParcelsMode = false;
                    CheckAndGetDclSceneMetaObject();
                }
            }
            else
            {
                if (GUILayout.Button("Edit"))
                {
                    var sb = new StringBuilder();
                    if (parcels.Count > 0)
                    {
                        sb.Append(ParcelToStringBuilder(parcels[0]));
                        for (int i = 1; i < parcels.Count; i++)
                        {
                            sb.Append('\n').Append(ParcelToStringBuilder(parcels[i]));
                        }
                    }

                    editParcelsText = sb.ToString();
                    editParcelsMode = true;
                    CheckAndGetDclSceneMetaObject();
                }
            }

            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel = 1;
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            if (editParcelsMode)
            {
                editParcelsText = EditorGUILayout.TextArea(editParcelsText);
            }
            else
            {
                var sb = new StringBuilder();
                if (parcels.Count > 0)
                {
                    sb.Append(ParcelToStringBuilder(parcels[0])).Append("(base)");
                    for (int i = 1; i < parcels.Count; i++)
                    {
                        sb.Append('\n').Append(ParcelToStringBuilder(parcels[i]));
                    }
                }

                GUILayout.Label(sb.ToString(),GUILayout.ExpandHeight(true));
            }
            EditorGUILayout.EndScrollView();

            EditorGUI.indentLevel = 0;
            EditorGUILayout.EndVertical();
        }

        public static void ParseTextToCoordinates(string text, List<ParcelCoordinates> coordinates, int x, int y, int startingX, int startingY)
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


                x = int.Parse(elements[0]);
                y = int.Parse(elements[1]);
                startingX = x;
                startingY = y;
                coordinates.Add(new ParcelCoordinates(x, y));
            }

        }
        public static void ParseText(string text, List<ParcelCoordinates> coordinates)
        {
            coordinates.Clear();
            int x = 0;
            int y = 0;
            var lines = text.Replace("\r", "").Split('\n');
            foreach (var line in lines)
            {
                var elements = line.Trim().Split(',');
                if (elements.Length == 0) continue;
                if (elements.Length != 2)
                {
                    throw new Exception("A line does not have exactly 2 elements!");
                }


                x = int.Parse(elements[0]);
                y = int.Parse(elements[1]);
                coordinates.Add(new ParcelCoordinates(x, y));
            }

        }

    }
}
