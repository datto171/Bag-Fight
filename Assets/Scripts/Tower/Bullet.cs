using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Bullet : MonoBehaviour
    {
        public GameObject target;

        private void FixedUpdate()
        {
            if (target == null)
            {
                Destroy(gameObject);
            }
        }
    }
}