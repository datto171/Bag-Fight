using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BagFight
{
    public class InventoryController : MonoBehaviour
    {
        public ItemGrid selectedItemGrid;

        private InventoryItem selectedItem;
        private InventoryItem overlapItem;
        private RectTransform rectTransform;

        [SerializeField] private List<ItemData> items;
        [SerializeField] private InventoryItem itemPref;
        [SerializeField] private Transform canvasTransform;

        private void Update()
        {
            ItemIconDrag();

            if (selectedItemGrid == null) return;

            if (Input.GetKeyDown(KeyCode.Q))
            {
                CreateRandomItem();
            }

            if (Input.GetMouseButtonDown(0))
            {
                LeftMouseBtnPress();
            }
        }

        private void CreateRandomItem()
        {
            InventoryItem inventoryItem = Instantiate(itemPref);
            selectedItem = inventoryItem;
            
            rectTransform = inventoryItem.GetComponent<RectTransform>();
            rectTransform.SetParent(canvasTransform);

            int selectedItemID = Random.Range(0, items.Count);
            inventoryItem.Set(items[selectedItemID]);
        }

        private void ItemIconDrag()
        {
            if (selectedItem != null)
            {
                // selectedItem.transform.position = Input.mousePosition;
            }
        }

        void LeftMouseBtnPress()
        {
            Vector2Int tileGridPosition = selectedItemGrid.GetTileGridPosition(Input.mousePosition);

            if (selectedItem == null)
            {
                PickUpItem(tileGridPosition);
            }
            else
            {
                PlaceItem(tileGridPosition);
            }
        }

        void PickUpItem(Vector2Int tileGridPosition)
        {
            selectedItem = selectedItemGrid.PickUpItem(tileGridPosition.x, tileGridPosition.y);
            if (selectedItem != null)
            {
                // rectTransform = selectedItem.GetComponent<RectTransform>();
            }
        }

        void PlaceItem(Vector2Int tileGridPosition)
        {
            selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y);
            selectedItem = null;
        }
    }
}