using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Test
{
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

        public bool CheckOverlapItem(int x, int y, int widthItem, int heightItem, Item itemSelected)
        {
            bool isOverLap = false;

            for (int i = 0; i < widthItem; i++)
            {
                for (int j = 0; j < heightItem; j++)
                {
                    TileComponent tileCheck = GetTile(x + i, y + j);
                    if (tileCheck != null)
                    {
                        if (tileCheck.itemContain != null)
                        {
                            if (tileCheck.itemContain != itemSelected)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public void ClearOldPosItem(int x, int y, int widthItem, int heightItem)
        {
            var newPosX = x + widthItem - 1;
            var newPosY = y + heightItem - 1;

            for (int i = x; i <= newPosX; i++)
            {
                for (int j = y; j <= newPosY; j++)
                {
                    TileComponent tile = GetTile(i, j);
                    tile.itemContain = null;
                    tile.mainTileLeft = null;
                }
            }
        }

        public void HoverHighLightItem(int x, int y, int widthItem, int heightItem)
        {
            var newPosX = x + widthItem - 1;
            var newPosY = y + heightItem - 1;

            for (int i = x; i <= newPosX; i++)
            {
                for (int j = y; j <= newPosY; j++)
                {
                    TileComponent tile = GetTile(i, j);
                    tile.objHighlight.gameObject.SetActive(true);
                    // tile.itemContain = null;
                    // tile.mainTileLeft = null;
                }
            }
        }

        public void HoverErrorItem(int x, int y, int widthItem, int heightItem)
        {
            var newPosX = x + widthItem - 1;
            var newPosY = y + heightItem - 1;

            for (int i = x; i <= newPosX; i++)
            {
                for (int j = y; j <= newPosY; j++)
                {
                    TileComponent tile = GetTile(i, j);
                    if (tile != null)
                    {
                        tile.objErrorNotPlace.gameObject.SetActive(true);
                    }
                    // tile.itemContain = null;
                    // tile.mainTileLeft = null;
                }
            }
        }

        public void OffHighlightInventory()
        {
            foreach (var tile in listTiles)
            {
                tile.objHighlight.gameObject.SetActive(false);
                tile.objErrorNotPlace.gameObject.SetActive(false);
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