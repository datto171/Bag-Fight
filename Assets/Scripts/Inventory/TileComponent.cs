using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace BagFight
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

        public List<TileComponent> listTilesAround;
        public TileComponent mainTileLeft;

        public static Action<TileComponent, Item> onSelectTile;
        public static Action<TileComponent, Item> onHoverItem;
        // public static Action<TileComponent> onClearHighlight;

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
            // onClearHighlight?.Invoke(this);
            invenCreate.ClearHighlightInventory();
        }

        public void SetupTilesAround(TileComponent tileAround)
        {
            listTilesAround.Add(tileAround);
        }

        [Button("Set null")]
        public void SetNotNull()
        {
            itemContain = null;
        }

        public void RemoveItem()
        {
            itemContain = null;
            mainTileLeft = null;
        }

        public void ActiveHighlight()
        {
            objHighlight.SetActive(true);
        }

        public void ActiveErrorTile()
        {
            objErrorNotPlace.SetActive(true);
        }

        public void OffHighlight()
        {
            objHighlight.SetActive(false);
        }

        public void OffErrorTile()
        {
            objErrorNotPlace.SetActive(false);
        }
    }
}