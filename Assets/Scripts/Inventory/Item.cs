using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace BagFight
{
    public class Item : MonoBehaviour
    {
        public int width;
        public int height;
        private float valueRotation;
        public bool isRotate = false;

        [InlineEditor] public ItemData itemData;
        public bool inBackPack = true;
        public BoxCollider2D collider2D;
        public Inventory invenContain; // save inventory contain this item

        public static Action<Item> onPickedUpItem;

        private void Start()
        {
            valueRotation = 0;
            width = itemData.GridSize.x;
            height = itemData.GridSize.y;
        }

        public void OnMouseDown()
        {
            if (!inBackPack) return;
            onPickedUpItem?.Invoke(this);
            collider2D.enabled = false;
        }

        [Button("check")]
        public void Test()
        {
            itemData.GetSlotCheck();
        }

        public void RotateItem()
        {
            isRotate = !isRotate;
            if (isRotate)
            {
                width = itemData.GridSize.y;
                height = itemData.GridSize.x;
            }
            else
            {
                width = itemData.GridSize.x;
                height = itemData.GridSize.y;
            }

            itemData.ChangeStateRotate();
            valueRotation -= 90;
            transform.rotation = Quaternion.Euler(0, 0, valueRotation);
        }
    }
}