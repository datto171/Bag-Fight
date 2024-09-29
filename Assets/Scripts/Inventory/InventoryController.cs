using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BagFight
{
    public class InventoryController : MonoBehaviour
    {
        public static InventoryController Instance;

        private Vector3 posItemSelected;
        private TileComponent mainTileHasItem;
        private Item itemSelected;

        // public Item itemTest;

        private void Awake()
        {
            Instance = this;
            TileComponent.onSelectTile = ClickTile;
            TileComponent.onHoverItem = CheckHighLightItem;
            TileComponent.onClearHighlight = RemoveHighlight;
            Item.onPickedUpItem = PickUpItemInBackPack;
        }

        private void RemoveHighlight(TileComponent tile)
        {
            tile.invenCreate.ClearHighlightInventory();
        }

        private void CheckHighLightItem(TileComponent tile, Item item)
        {
            var invenCheck = tile.invenCreate;
            tile.invenCreate.ClearHighlightInventory();

            // user hover item when not selected any item
            if (itemSelected == null)
            {
                if (tile.itemContain != null)
                {
                    var tileHover = tile.mainTileLeft;
                    item.invenContain.CheckPosItem(tileHover.x, tileHover.y, item, StateTilesItem.HoverHighlight);
                }
            }
            else
            {
                int widthItem = itemSelected.width;
                int heightItem = itemSelected.height;

                Vector2 posItem;
                List<TileComponent> tilesPlace = invenCheck.BoundaryCheck2(tile.x, tile.y, itemSelected);
                // Cannot place => notice error tiles 
                if (tilesPlace == null)
                {
                    invenCheck.CheckPosItem(tile.x, tile.y, itemSelected, StateTilesItem.HoverError);
                }
                else // show tiles object highlight
                {
                    invenCheck.CheckPosItem(tile.x, tile.y, itemSelected, StateTilesItem.HoverHighlight);
                }

                // Check hover tiles can place ? 
                List<TileComponent> tilesOverlap = invenCheck.CheckOverlapItem(tile.x, tile.y, itemSelected);
                if (tilesOverlap.Count > 0)
                {
                    foreach (var tileCheck in tilesOverlap)
                    {
                        tileCheck.ActiveErrorTile();
                    }
                }
            }
        }

        private void Update()
        {
            if (itemSelected == null) return;

            posItemSelected = Input.mousePosition;
            posItemSelected.z = 1;
            itemSelected.transform.position = Camera.main.ScreenToWorldPoint(posItemSelected);

            if (Input.GetKeyDown(KeyCode.R))
            {
                itemSelected.RotateItem();
            }
        }

        private void PickUpItemInBackPack(Item item)
        {
            itemSelected = item;
        }

        private void ClickTile(TileComponent tile, Item item)
        {
            // ko cam item
            if (itemSelected == null)
            {
                if (item == null) return;

                mainTileHasItem = tile.mainTileLeft;
                itemSelected = item;
                // PickUpItem(item);
            }
            else // place item 
            {
                Inventory invenCheck = tile.invenCreate;

                int widthItem = itemSelected.width;
                int heightItem = itemSelected.height;

                // Check Overlap item
                var tilesOverlap = invenCheck.CheckOverlapItem(tile.x, tile.y, itemSelected);
                if (tilesOverlap.Count > 0)
                {
                    return;
                }

                // Vector2 posItem;
                // Chỗ này xử lý lại chi tiết hơn ở bên file này
                // Hàm BoundaryCheck chỉ return là ô đấy có được phép đặt item không
                // List<TileComponent> tilesPlace = invenCheck.BoundaryCheck(tile.x, tile.y, widthItem, heightItem, out posItem);
                
                List<TileComponent> tilesPlace = invenCheck.BoundaryCheck2(tile.x, tile.y, itemSelected);
                Vector2 posItem = invenCheck.GetPosItem(tile.x, tile.y, itemSelected);

                if (tilesPlace != null)
                {
                    // Clear old position contain item
                    if (mainTileHasItem != null)
                    {
                        itemSelected.invenContain.CheckPosItem(mainTileHasItem.x, mainTileHasItem.y, itemSelected,
                            StateTilesItem.RemoveOldPosItem);
                        mainTileHasItem = null;
                    }

                    // Set all tile contain item at new place 
                    foreach (var tileSet in tilesPlace)
                    {
                        tileSet.itemContain = itemSelected;
                        tileSet.mainTileLeft = tile;
                    }

                    // Set new pos for item
                    itemSelected.invenContain = invenCheck;
                    itemSelected.transform.position = posItem;
                    itemSelected = null;
                }
            }
        }


        // public void PlaceItem(TileComponent tile)
        // {
        //     mainTileHasItem.itemContain = null;
        //     itemSelected.transform.position = tile.transform.position;
        //     itemSelected = null;
        // }
        //
        // [Button("Place Item")]
        // public void SetPlaceItem(Item item, TileComponent tile)
        // {
        //     tile.itemContain = item;
        //     item.transform.position = tile.transform.position;
        // }
        //
        // public void PickUpItem(Item item)
        // {
        //     itemSelected = item;
        //     Debug.Log("pick up success");
        // }
    }
}