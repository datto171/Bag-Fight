using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BagFight
{
    // [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
    public class ItemData : ScriptableObject
    {
        public int width = 1;
        public int height = 1;

        public Sprite itemIcon;
    }
}