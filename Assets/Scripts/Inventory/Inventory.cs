using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BagFight
{
    public enum StateTilesItem
    {
        HoverHighlight,
        HoverError,
        RemoveOldPosItem
    }

    public class Inventory : MonoBehaviour
    {
        [SerializeField] int width = 5;
        [SerializeField] int height = 5;
        [SerializeField] TileComponent tilePrefab;
        [SerializeField] Transform posContain;

        [SerializeField] float startPosX;
        [SerializeField] float startPosY; // pos y lam chuan o device 1920x1080p
        [SerializeField] float distanceX;
        [SerializeField] float distanceY;

        private List<TileComponent> listTiles;

        private void Awake()
        {
            listTiles = new List<TileComponent>();
            CreateMap();
        }

        public void CreateMap()
        {
            for (int x = 0; x < width * height; x++)
            {
                TileComponent tileCreate = Instantiate(tilePrefab, posContain);
                int tilePosX = x % width;
                int tilePosY = x / width;

                tileCreate.name = (tilePosX + "_" + tilePosY);
                float posX = tilePosX * distanceX + startPosX;
                float posY = tilePosY * distanceY * (-1) + startPosY;
                tileCreate.transform.localPosition = new Vector3(posX, posY, 0);

                tileCreate.x = tilePosX;
                tileCreate.y = tilePosY;
                tileCreate.invenCreate = this;

                listTiles.Add(tileCreate);
            }
        }

        public void SetupDataTile(TileComponent tile, int i)
        {
        }

        bool PositionCheck(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
            {
                return false;
            }

            return true;
        }

        // Chỗ này viết vẫn thiếu phải bổ sung chính xác vị trí item này được đặt xuông với vị trí nào
        public List<TileComponent> BoundaryCheck(int x, int y, int widthItem, int heightItem, out Vector2 posItem)
        {
            posItem = new Vector2();
            if (PositionCheck(x, y) == false) return null;

            // Pos x, pos y of item if place
            var newPosX = x + widthItem - 1;
            var newPosY = y + heightItem - 1;

            if (PositionCheck(newPosX, newPosY) == false) return null;

            // Return position item visual
            TileComponent tileStart = GetTile(x, y);
            TileComponent tileEnd = GetTile(newPosX, newPosY);
            posItem = (Vector2)(tileEnd.transform.position + tileStart.transform.position) / 2;

            // Return all tiles contain pos of item
            List<TileComponent> tiles = new List<TileComponent>();
            for (int i = x; i <= newPosX; i++)
            {
                for (int j = y; j <= newPosY; j++)
                {
                    TileComponent tile = GetTile(i, j);
                    tiles.Add(tile);
                }
            }

            return tiles;
        }

        public Vector2 GetPosItem(int x, int y, Item item)
        {
            var newPosX = x + item.itemData.width - 1;
            var newPosY = y + item.itemData.height - 1;

            // Return position item visual
            TileComponent tileStart = GetTile(x, y);
            TileComponent tileEnd = GetTile(newPosX, newPosY);
            var posItem = (Vector2)(tileEnd.transform.position + tileStart.transform.position) / 2;
            return posItem;
        }

        [Button("Check boundary slot")]
        public List<TileComponent> BoundaryCheck2(int x, int y, Item item)
        {
            if (PositionCheck(x, y) == false) return null;

            item.itemData.GetSlotCheck();

            // Pos x, pos y of item if place
            var newPosX = x + item.itemData.width - 1;
            var newPosY = y + item.itemData.height - 1;

            if (PositionCheck(newPosX, newPosY) == false) return null;

            // Return all tiles contain pos of item
            List<TileComponent> tiles = new List<TileComponent>();
            foreach (var slot in item.itemData.listTilesCheck)
            {
                TileComponent tileCheck = GetTile(x + slot.x, y + slot.y);
                if (tileCheck != null)
                {
                    Debug.Log("tile Check: " + tileCheck.x + "_" + tileCheck.y);
                    tiles.Add(tileCheck);
                }
            }

            // for (int i = x; i <= newPosX; i++)
            // {
            //     for (int j = y; j <= newPosY; j++)
            //     {
            //         var valueAtSlotItem = item.itemData.listTilesCheck[z];
            //         // if (valueAtSlotItem == 1)
            //         // {
            //         //     TileComponent tile = GetTile(i, j);
            //         //     tiles.Add(tile);
            //         // }
            //     }
            // }

            return tiles;
        }

        public Vector2 GetPosItem(Vector2Int vTileStart, Vector2Int vectorTileEnd)
        {
            TileComponent tileStart = GetTile(vTileStart.x, vTileStart.y);
            TileComponent tileEnd = GetTile(vectorTileEnd.x, vectorTileEnd.y);
            Vector2 posItem = (Vector2)(tileEnd.transform.position + tileStart.transform.position) / 2;
            return posItem;
        }

        // Check bên dưới có bị trùng vị trí item không
        public List<TileComponent> CheckOverlapItem(int x, int y, Item item)
        {
            item.itemData.GetSlotCheck();
            List<TileComponent> tilesOverlap = new List<TileComponent>();
            foreach (var slot in item.itemData.listTilesCheck)
            {
                TileComponent tileCheck = GetTile(x + slot.x, y + slot.y);
                if (tileCheck != null)
                {
                    Debug.Log("tile Check: " + tileCheck.x + "_" + tileCheck.y);
                    if (tileCheck.itemContain != null && tileCheck.itemContain != item)
                    {
                        tilesOverlap.Add(tileCheck);
                    }
                }
            }

            return tilesOverlap;

            // List<TileComponent> tilesOverlap = new List<TileComponent>();
            // int widthItem = item.width;
            // int heightItem = item.height;
            //
            // for (int i = 0; i < widthItem; i++)
            // {
            //     for (int j = 0; j < heightItem; j++)
            //     {
            //         TileComponent tileCheck = GetTile(x + i, y + j);
            //         if (tileCheck != null)
            //         {
            //             if (tileCheck.itemContain != null && tileCheck.itemContain != item)
            //             {
            //                 tilesOverlap.Add(tileCheck);
            //             }
            //         }
            //     }
            // }
            //
            // return tilesOverlap;
        }

        public void CheckPosItem(int x, int y, Item item, StateTilesItem state)
        {
            var newPosX = x + item.width - 1;
            var newPosY = y + item.height - 1;

            item.itemData.GetSlotCheck();

            foreach (var slot in item.itemData.listTilesCheck)
            {
                TileComponent tileCheck = GetTile(x + slot.x, y + slot.y);
                if (tileCheck != null)
                {
                    HandleState(tileCheck);
                }
            }

            // for (int i = x; i <= newPosX; i++)
            // {
            //     for (int j = y; j <= newPosY; j++)
            //     {
            //         TileComponent tile = GetTile(i, j);
            //         if (tile != null)
            //         {
            //             HandleState(tile);
            //         }
            //     }
            // }

            void HandleState(TileComponent tile)
            {
                switch (state)
                {
                    case StateTilesItem.RemoveOldPosItem:
                        tile.RemoveItem();
                        break;
                    case StateTilesItem.HoverError:
                        tile.ActiveErrorTile();
                        break;
                    case StateTilesItem.HoverHighlight:
                        tile.ActiveHighlight();
                        break;
                }
            }
        }

        public void ClearHighlightInventory()
        {
            foreach (var tile in listTiles)
            {
                tile.OffHighlight();
                tile.OffErrorTile();
            }
        }

        public TileComponent GetTile(int x, int y)
        {
            if (x < 0 || y < 0 || x > (width - 1) || y > (height - 1))
            {
                return null;
            }
            else
            {
                return listTiles[y * width + x];
            }
        }
    }
}