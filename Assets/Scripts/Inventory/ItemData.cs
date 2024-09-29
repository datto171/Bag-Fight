using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace BagFight
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
    public class ItemData : ScriptableObject
    {
        public Sprite itemIcon;

        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private bool[] bagSlots;
        [SerializeField] List<Vector2Int> listSlotsCheck;

        public Vector2Int GridSize => gridSize;
        public bool[] BagSlots => bagSlots;
        public List<Vector2Int> ListSlotsCheck => listSlotsCheck;

        public void GetSlotCheck()
        {
            listSlotsCheck = new List<Vector2Int>();
            for (int i = 0; i < gridSize.x * gridSize.y; i++)
            {
                int posX = i % gridSize.x;
                int posY = i / gridSize.x;
                // Debug.Log("slot: " + "x_y" + posX + "_" + posY + " value: " + cellData[i]);
                if (bagSlots[i])
                {
                    listSlotsCheck.Add(new Vector2Int(posX, posY));
                }
            }
        }
    }
}