using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BagFight
{
    public class GridInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private InventoryController invenController;
        public ItemGrid itemGrid;

        private void Awake()
        {
            invenController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
            itemGrid = GetComponent<ItemGrid>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Debug.Log("pointer enter");
            invenController.selectedItemGrid = itemGrid;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Debug.Log("pointer exit");
            invenController.selectedItemGrid = null;
        }
    }
}