using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace BagFight
{
    public class Item : MonoBehaviour
    {
        public int width;
        public int height;
        public bool isRotate = false;
        private Rotate stateRotate;

        public ItemData itemData;
        public bool inBackPack = true;
        public BoxCollider2D collider2D;
        public Inventory invenContain; // save inventory contain this item

        public static Action<Item> onPickedUpItem;

        private void Start()
        {
            width = itemData.GridSize.x;
            height = itemData.GridSize.y;
            valueRotation = z1;
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

        private float valueRotation;
        private float z1 = 0;
        private float z2 = 90;
        private float z3 = 180;
        private float z4 = 270;

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

            if (valueRotation == z1)
            {
                valueRotation = z2;
            }
            else if (valueRotation == z2)
            {
                valueRotation = z3;
            }
            else if (valueRotation == z3)
            {
                valueRotation = z4;
            }
            else if (valueRotation == z4)
            {
                valueRotation = z1;
            }

            // valueRotation = transform.rotation.z;
            // valueRotation += 90f;

            transform.rotation = Quaternion.Euler(0, 0, valueRotation);
            // transform.rotation = Quaternion.Euler(0, 0, isRotate == true ? +90f : +90f);
        }
    }
}