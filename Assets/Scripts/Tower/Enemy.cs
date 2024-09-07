using System;
using System.Collections;
using System.Collections.Generic;
using AdvancedModule.ObserverPattern;
using UnityEngine;

namespace TowerDefense
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed;

        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Bullet>())
            {
                var bullet = other.gameObject.GetComponent<Bullet>();
                Destroy(bullet.gameObject);
                // Destroy(gameObject);
                EventDispatcher.SceneInstance.TriggerEvent(new KillEnemyEvent
                {
                    enemy = this
                });
            }
        }
    }
}