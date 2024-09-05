using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefense
{
    public class Player : MonoBehaviour
    {
        List<Enemy> enemies;

        // Start is called before the first frame update
        void Start()
        {
            enemies = new List<Enemy>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Enemy>())
            {
                Debug.Log(enemies.Count);
                
                var enemy = other.gameObject.GetComponent<Enemy>();
                enemies.Add(enemy);
            }
        }
    }
}