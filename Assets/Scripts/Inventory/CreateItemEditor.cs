using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace BagFight
{
    [CustomEditor(typeof(ItemData))]
    public class CreateItemEditor : Editor
    {
        private SerializedProperty gridSize;
        private SerializedProperty slots;
        // private SerializedProperty listTilesCheck;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            gridSize = serializedObject.FindProperty("gridSize");
            slots = serializedObject.FindProperty("bagSlots");
            // listTilesCheck = serializedObject.FindProperty("listTilesCheck");

            DrawGridButtons(gridSize.vector2IntValue, slots);

            // Hàm nay de cho phep user click thay doi so trong grid vector2
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawGridButtons(Vector2Int vector2Int, SerializedProperty slots)
        {
            slots.arraySize = vector2Int.x * vector2Int.y;

            for (int y = 0; y < vector2Int.y; y++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < vector2Int.x; x++)
                {
                    var index = y * vector2Int.x + x;
                    var slot = slots.GetArrayElementAtIndex(index);
                    var buttonLabel = slot.boolValue ? "x" : "";
                    
                    if (GUILayout.Button(buttonLabel, GUILayout.Width(30), GUILayout.Height(30)))
                    {
                        slot.boolValue = !slot.boolValue;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}