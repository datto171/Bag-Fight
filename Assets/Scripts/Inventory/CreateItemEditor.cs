using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BagFight
{
    [CustomEditor(typeof(ItemData))]
    public class CreateItemEditor : Editor
    {
        private Vector2 scrollValue;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update Size"))
            {
                CreateGrid();
            }

            DrawGridButtons();

            // Hàm nay de cho phep user click thay doi so trong grid vector2
            serializedObject.ApplyModifiedProperties();
        }

        private void CreateGrid()
        {
            var gridSizeProperty = serializedObject.FindProperty("gridSize");
            var gridSize = gridSizeProperty.vector2IntValue;
            var cellData = new int[gridSize.x * gridSize.y];
            serializedObject.FindProperty("cellData").arraySize = cellData.Length;
        }

        private void DrawGridButtons()
        {
            var gridSizeProperty = serializedObject.FindProperty("gridSize");
            var gridSize = gridSizeProperty.vector2IntValue;
            var cellDataProperty = serializedObject.FindProperty("cellData");

            if (cellDataProperty.arraySize != gridSize.x * gridSize.y)
            {
                GUILayout.Label("Grid size does not match cell data size");
                return;
            }

            GUILayout.Label("0 = None, 1 = Wall");

            var originColor = GUI.backgroundColor;
            for (int y = 0; y < gridSize.y; y++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < gridSize.x; x++)
                {
                    var index = y * gridSize.x + x;
                    var cellProperty = cellDataProperty.GetArrayElementAtIndex(index);
                    var buttonLabel = cellProperty.intValue.ToString();

                    var btnColor = Color.white;
                    if (cellProperty.intValue == 1)
                    {
                        btnColor = Color.red;
                    }

                    // Add Color to button
                    GUI.backgroundColor = btnColor;
                    if (GUILayout.Button(buttonLabel, GUILayout.Width(30), GUILayout.Height(30)))
                    {
                        var value = cellProperty.intValue + 1;
                        value %= 2;
                        cellProperty.intValue = value;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }

            GUI.backgroundColor = originColor;

            if (GUILayout.Button("Clear Grid"))
            {
                cellDataProperty.ClearArray();
                CreateGrid();
            }
        }
    }
}