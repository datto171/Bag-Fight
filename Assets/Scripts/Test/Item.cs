using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Item : MonoBehaviour
    {
        public ItemData itemData;
        public bool inBackPack = true;
        public BoxCollider2D collider2D;
        public Inventory inventoryContain; // save inventory contain this item
        
        public static Action<Item> onPickedUpItem;

        public void OnMouseDown()
        {
            if (!inBackPack) return;

            onPickedUpItem?.Invoke(this);
            collider2D.enabled = false;
        }

    }
}