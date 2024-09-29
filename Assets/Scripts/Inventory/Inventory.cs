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

    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
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
        private List<Vector2Int> listDirections;

        private void Awake()
        {
            listTiles = new List<TileComponent>();
            SetupDirections();
            CreateMap();
        }

        [Button("Test Move")]
        Queue<TileComponent> FloodFill(TileComponent tileStart, TileComponent target)
        {
            Dictionary<TileComponent, TileComponent> dictTileToTile = new Dictionary<TileComponent, TileComponent>();
            Queue<TileComponent> queueWillCheck = new Queue<TileComponent>();
            List<TileComponent> listChecked = new List<TileComponent>();

            queueWillCheck.Enqueue(target);

            while (queueWillCheck.Count > 0)
            {
                var curTile = queueWillCheck.Dequeue();

                foreach (var tileNeighbor in curTile.listTilesAround)
                {
                    if (listChecked.Contains(tileNeighbor) == false && queueWillCheck.Contains(tileNeighbor) == false)
                    {
                        if (tileNeighbor.itemContain == null)
                        {
                            queueWillCheck.Enqueue(tileNeighbor);
                            dictTileToTile[tileNeighbor] = curTile;
                        }
                    }
                }

                listChecked.Add(curTile);
            }

            if (listChecked.Contains(tileStart) == false)
            {
                Debug.Log("No path can move");
                return null;
            }

            Queue<TileComponent> path = new Queue<TileComponent>();
            TileComponent curPathTile = tileStart;
            while (curPathTile != target)
            {
                curPathTile = dictTileToTile[curPathTile];
                path.Enqueue(curPathTile);
                Debug.Log("tile move: " + curPathTile);
            }

            return path;
        }

        void SetupDirections()
        {
            listDirections = new List<Vector2Int>();
            listDirections.Add(new Vector2Int(0, -1));
            listDirections.Add(new Vector2Int(0, 1));
            listDirections.Add(new Vector2Int(1, 0));
            listDirections.Add(new Vector2Int(-1, 0));
        }

        public void CreateMap()
        {
            for (int x = 0; x < width * height; x++)
            {
                TileComponent tileCreate = Instantiate(tilePrefab, posContain);
                tileCreate.x = x % width;
                tileCreate.y = x / width;
                tileCreate.name = (tileCreate.x + "_" + tileCreate.y);

                // set position tile 
                float posX = tileCreate.x * distanceX + startPosX;
                float posY = tileCreate.y * distanceY * (-1) + startPosY;
                tileCreate.transform.localPosition = new Vector3(posX, posY, 0);

                tileCreate.invenCreate = this;
                listTiles.Add(tileCreate);
            }

            foreach (var tile in listTiles)
            {
                foreach (var dir in listDirections)
                {
                    TileComponent tileAround = GetTile(tile.x + dir.x, tile.y + dir.y);
                    if (tileAround != null)
                    {
                        tile.SetupTilesAround(tileAround);
                    }
                }
            }
        }

        // Chỗ này viết vẫn thiếu phải bổ sung chính xác vị trí item này được đặt xuông với vị trí nào
        // public List<TileComponent> BoundaryCheck(int x, int y, int widthItem, int heightItem, out Vector2 posItem)
        // {
        //     posItem = new Vector2();
        //     if (PositionCheck(x, y) == false) return null;
        //
        //     // Pos x, pos y of item if place
        //     var newPosX = x + widthItem - 1;
        //     var newPosY = y + heightItem - 1;
        //
        //     if (PositionCheck(newPosX, newPosY) == false) return null;
        //
        //     // Return position item visual
        //     TileComponent tileStart = GetTile(x, y);
        //     TileComponent tileEnd = GetTile(newPosX, newPosY);
        //     posItem = (Vector2)(tileEnd.transform.position + tileStart.transform.position) / 2;
        //
        //     // Return all tiles contain pos of item
        //     List<TileComponent> tiles = new List<TileComponent>();
        //     for (int i = x; i <= newPosX; i++)
        //     {
        //         for (int j = y; j <= newPosY; j++)
        //         {
        //             TileComponent tile = GetTile(i, j);
        //             tiles.Add(tile);
        //         }
        //     }
        //
        //     return tiles;
        // }

        public Vector2 GetPosItem(int x, int y, Item item)
        {
            var newPosX = x + item.itemData.GridSize.x - 1;
            var newPosY = y + item.itemData.GridSize.y - 1;

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
            var newPosX = x + item.itemData.GridSize.x - 1;
            var newPosY = y + item.itemData.GridSize.y - 1;

            if (PositionCheck(newPosX, newPosY) == false) return null;

            // Return all tiles contain pos of item
            List<TileComponent> tiles = new List<TileComponent>();
            foreach (var slot in item.itemData.ListSlotsCheck)
            {
                TileComponent tileCheck = GetTile(x + slot.x, y + slot.y);
                if (tileCheck != null)
                {
                    Debug.Log("tile Check: " + tileCheck.x + "_" + tileCheck.y);
                    tiles.Add(tileCheck);
                }
            }

            return tiles;
        }

        // Check bên dưới có bị trùng vị trí item không
        public List<TileComponent> CheckOverlapItem(int x, int y, Item item)
        {
            item.itemData.GetSlotCheck();
            List<TileComponent> tilesOverlap = new List<TileComponent>();
            foreach (var slot in item.itemData.ListSlotsCheck)
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
        }

        public void CheckPosItem(int x, int y, Item item, StateTilesItem state)
        {
            item.itemData.GetSlotCheck();

            foreach (var slot in item.itemData.ListSlotsCheck)
            {
                TileComponent tileCheck = GetTile(x + slot.x, y + slot.y);
                if (tileCheck != null)
                {
                    HandleState(tileCheck);
                }
            }

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

        bool PositionCheck(int x, int y)
        {
            if (x < 0 || y < 0 || x >= width || y >= height)
            {
                return false;
            }

            return true;
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