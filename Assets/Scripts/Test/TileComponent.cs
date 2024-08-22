using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TileComponent : MonoBehaviour
    {
        public int x;
        public int y;

        public SpriteRenderer img;
        public Item itemContain;

        public TileComponent mainTileLeft;

        public static Action<TileComponent, Item> onSelectTile;
        public static Action<Item> onHoverItem;

        public void OnMouseDown()
        {
            onSelectTile?.Invoke(this, itemContain);
        }

        public void OnMouseEnter()
        {
            if (itemContain != null)
            {
                onHoverItem?.Invoke(itemContain);
            }
            else
            {
                
            }
        }
    }
}