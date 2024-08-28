using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class TileComponent : MonoBehaviour
    {
        public Inventory invenCreate;

        public int x;
        public int y;

        public SpriteRenderer img;
        public GameObject objHighlight; // highlight when not bring any item
        public GameObject objErrorNotPlace;
        public Item itemContain;

        public TileComponent mainTileLeft;

        public static Action<TileComponent, Item> onSelectTile;
        public static Action<TileComponent, Item> onHoverItem;
        public static Action<TileComponent> onClearHighlight;

        public void OnMouseDown()
        {
            onSelectTile?.Invoke(this, itemContain);
        }

        public void OnMouseEnter()
        {
            onHoverItem?.Invoke(this, itemContain);
        }

        public void OnMouseExit()
        {
            onClearHighlight?.Invoke(this);
        }
    }
}