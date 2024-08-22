using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BagFight
{
    public class ItemGrid : MonoBehaviour
    {
        public const float tileSizeWidth = 32;
        public const float tileSizeHeight = 32;

        private InventoryItem[,] inventorySlot;

        private RectTransform rectTransform;

        private Vector2 posOnTheGrid = new Vector2();
        private Vector2Int tileGridPos = new Vector2Int();

        [SerializeField] private int gridSizeWidth;
        [SerializeField] private int gridSizeHeight;

        [SerializeField] private InventoryItem invenItemPref;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            Init(gridSizeWidth, gridSizeHeight);

            InventoryItem inventoryItem = Instantiate(invenItemPref, this.transform);
            PlaceItem(inventoryItem, 4, 2);
        }

        internal InventoryItem PickUpItem(int x, int y)
        {
            InventoryItem toReturn = inventorySlot[4, 2];
            // inventorySlot[x, y] = null;
            return toReturn;
        }

        [Button("Set")]
        void Init(int width, int height)
        {
            inventorySlot = new InventoryItem[width, height];
            Vector2 size = new Vector2(width * tileSizeWidth, height * tileSizeHeight);
            rectTransform.sizeDelta = size;
        }

        public Vector2Int GetTileGridPosition(Vector2 mousePos)
        {
            posOnTheGrid.x = mousePos.x - rectTransform.position.x;
            posOnTheGrid.y = rectTransform.position.y - mousePos.y;

            tileGridPos.x = (int)(posOnTheGrid.x / tileSizeWidth);
            tileGridPos.y = (int)(posOnTheGrid.y / tileSizeHeight);

            return tileGridPos;
        }

        [Button("place item")]
        public void PlaceItem(InventoryItem invenItem, int posX, int posY)
        {
            RectTransform rectTransform = invenItem.GetComponent<RectTransform>();
            rectTransform.SetParent(rectTransform);
            inventorySlot[posX, posY] = invenItem;

            Vector2 pos = new Vector2();
            pos.x = posX * tileSizeWidth + tileSizeWidth * invenItem.itemData.width / 2;
            pos.y = -(posY * tileSizeHeight + tileSizeHeight  * invenItem.itemData.height / 2);

            rectTransform.localPosition = pos;
        }
    }
}