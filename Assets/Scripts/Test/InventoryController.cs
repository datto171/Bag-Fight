using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Test
{
    public class InventoryController : MonoBehaviour
    {
        public static InventoryController Instance;
        public Inventory inventory;

        private Vector3 posItemSelected;
        private TileComponent mainTileHasItem;
        private Item itemSelected;

        public Item itemTest;

        private void Awake()
        {
            Instance = this;
            TileComponent.onSelectTile = ClickTile;
            TileComponent.onHoverItem = CheckHighLightItem;
            Item.onPickedUpItem = PickUpItemInBackPack;
        }

        private void CheckHighLightItem(Item item)
        {
            
        }


        private void Start()
        {
            TileComponent tileTest = inventory.GetTile(1, 1);
            // SetPlaceItem(itemTest, tileTest);
        }

        private void Update()
        {
            
            if (itemSelected == null) return;
            
            posItemSelected = Input.mousePosition;
            posItemSelected.z = 1;
            itemSelected.transform.position = Camera.main.ScreenToWorldPoint(posItemSelected);
        }

        private void PickUpItemInBackPack(Item item)
        {
            itemSelected = item;
        }

        private void ClickTile(TileComponent tile, Item item)
        {
            if (itemSelected == null)
            {
                if (item == null) return;

                mainTileHasItem = tile.mainTileLeft;
                itemSelected = item;
                // PickUpItem(item);
            }
            else
            {
                int widthItem = itemSelected.itemData.width;
                int heightItem = itemSelected.itemData.height;

                // Check Overlap item
                bool isOverlapItem = inventory.CheckOverlapItem(tile.x, tile.y, widthItem, heightItem, itemSelected);
                if (isOverlapItem)
                {
                    return;
                }

                Vector2 posItem;
                // Chỗ này xử lý lại chi tiết hơn ở bên file này
                // Hàm BoundaryCheck chỉ return là ô đấy có được phép đặt item không
                List<TileComponent> tilesPlace =
                    inventory.BoundaryCheck(tile.x, tile.y, widthItem, heightItem, out posItem);

                if (tilesPlace != null)
                {
                    // Clear old position contain item
                    if (mainTileHasItem != null)
                    {
                        inventory.ClearOldPosItem(mainTileHasItem.x, mainTileHasItem.y, widthItem, heightItem);
                        mainTileHasItem = null;
                    }
                    
                    // Set all tile contain item at new place 
                    foreach (var tileSet in tilesPlace)
                    {
                        tileSet.itemContain = itemSelected;
                        tileSet.mainTileLeft = tile;
                    }

                    // Set new pos for item
                    itemSelected.transform.position = posItem;
                    itemSelected = null;
                }
            }
        }


        public void PlaceItem(TileComponent tile)
        {
            mainTileHasItem.itemContain = null;
            itemSelected.transform.position = tile.transform.position;
            itemSelected = null;
        }

        [Button("Place Item")]
        public void SetPlaceItem(Item item, TileComponent tile)
        {
            tile.itemContain = item;
            item.transform.position = tile.transform.position;
        }

        public void PickUpItem(Item item)
        {
            itemSelected = item;
            Debug.Log("pick up success");
        }
    }
}