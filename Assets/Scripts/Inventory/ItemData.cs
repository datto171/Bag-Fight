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

        public int[][] matrix;

        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private bool[] bagSlots;
        [SerializeField] List<Vector2Int> listSlotsCheck;

        public Vector2Int GridSize => gridSize;
        public bool[] BagSlots => bagSlots;
        public List<Vector2Int> ListSlotsCheck => listSlotsCheck;

        public RotateItem stateRotate;

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

        void Test()
        {
            // l,r = 0, len(matrix) -1;
            // while l < r:
            // for i in range (l-r):
                // top, bottom = l,r;
                // === save the top left
                // topLeft = matrix[top][l+i]
                // ===  move bottom left into top left
                // matrix[top][l+i] = matrix[bottom -i][l]
                // ===  move bottom right in to bottom left
                // matrix[bottom -i][l] = matrix[bottom][r-i]
                // ===  move top right into bottom right
                // matrix[bottom][r-i] = matrix[top + i][r]
                // ===  move top left into top right
                // matrix[bottom -i][l] = topleft
            // r -= 1
            // l += 1
        }

        public void RotateSlotCheckItem()
        {
            int l = 0;
            int r = GridSize.x;
            while (l < r)
            {
                for (int i = l; i < (r - l); i++)
                {
                    var top = l;
                    var bot = r;

                    int posX = i % gridSize.x;
                    int posY = i / gridSize.x;

                    var valueSlot = GetValueSlot(posX, posY);
                    
                }
            }
        }

        public int GetValueSlot(int x, int y)
        {
            if (x < 0 || y < 0 || x > (gridSize.x - 1) || y > (gridSize.y - 1))
            {
                return -1;
            }
            else
            {
                var value = bagSlots[y * gridSize.x + x];
                if (value)
                {
                    return 1; // true
                }
                else
                {
                    return 2; // false
                }
            }
        }

        public void ChangeStateRotate()
        {
            if (stateRotate == RotateItem.Down)
            {
                stateRotate = RotateItem.Left;
            }
            else if (stateRotate == RotateItem.Left)
            {
                stateRotate = RotateItem.Up;
            }
            else if (stateRotate == RotateItem.Up)
            {
                stateRotate = RotateItem.Right;
            }
            else if (stateRotate == RotateItem.Right)
            {
                stateRotate = RotateItem.Down;
            }
        }
    }
}