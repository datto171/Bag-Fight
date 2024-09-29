using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BagFight
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
    public class ItemData : ScriptableObject
    {
        public int width = 1;
        public int height = 1;
        public Sprite itemIcon;
        // public ItemConfig itemConfig;
        
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private int[] cellData;
        public Vector2Int GridSize => gridSize;
        public int[] CellData => cellData;

        public List<Vector2Int> listTilesCheck;

        public void GetSlotCheck()
        {
            listTilesCheck = new List<Vector2Int>();
            for (int i = 0; i < gridSize.x * gridSize.y; i++)
            {
                int posX = i % gridSize.x;
                int posY = i / gridSize.x;
                // Debug.Log("slot: " + "x_y" + posX + "_" + posY + " value: " + cellData[i]);
                if (cellData[i] == 1)
                {
                    listTilesCheck.Add(new Vector2Int(posX, posY));
                }
            }
        }
    }
}