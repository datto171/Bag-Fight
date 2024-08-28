using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Test
{
    public class Item : MonoBehaviour
    {
        public int width;
        public int height;
        public bool isRotate = false;

        [SerializeField] ItemData itemData;
        public bool inBackPack = true;
        public BoxCollider2D collider2D;
        public Inventory invenContain; // save inventory contain this item

        public static Action<Item> onPickedUpItem;

        private void Start()
        {
            width = itemData.width;
            height = itemData.height;
        }

        public void OnMouseDown()
        {
            if (!inBackPack) return;

            onPickedUpItem?.Invoke(this);
            collider2D.enabled = false;
        }

        public void RotateItem()
        {
            isRotate = !isRotate;
            if (isRotate)
            {
                width = itemData.height;
                height = itemData.width;
            }
            else
            {
                width = itemData.width;
                height = itemData.height;
            }

            transform.rotation = Quaternion.Euler(0, 0, isRotate == true ? 90f : 0f);
        }
    }
}