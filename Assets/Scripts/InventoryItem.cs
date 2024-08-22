using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BagFight
{
    public class InventoryItem : MonoBehaviour
    {
        public Image img;
        public ItemData itemData;

        public void Set(ItemData item)
        {
            this.itemData = item;
            img.sprite = item.itemIcon;

            Vector2 size = new Vector2();
            size.x = itemData.width * ItemGrid.tileSizeWidth;
            size.y = itemData.height * ItemGrid.tileSizeHeight;
            GetComponent<RectTransform>().sizeDelta = size;
        }
    }
}