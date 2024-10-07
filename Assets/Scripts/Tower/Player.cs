using System;
using System.Collections;
using System.Collections.Generic;
using AdvancedModule.ObserverPattern;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefense
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private List<Enemy> enemies;
        [SerializeField] private Bullet prefBullet;
        [SerializeField] private Transform posContain;

        private float currentTime;
        [SerializeField] private float timeDelay = 0.5f;

        void Start()
        {
            enemies = new List<Enemy>();
            EventDispatcher.SceneInstance.AddListener<KillEnemyEvent>(KillEnemy);
        }

        private void KillEnemy(KillEnemyEvent e)
        {
            var enemyKilled = e.enemy;
            if (enemies.Contains(enemyKilled.GetComponent<Enemy>()))
            {
                enemies.Remove(enemyKilled.GetComponent<Enemy>());
                Destroy(enemyKilled.gameObject);
            }
        }

        private void FixedUpdate()
        {
            if (enemies.Count > 0)
            {
                currentTime += Time.fixedDeltaTime;
                if (currentTime >= timeDelay)
                {
                    int random = Random.Range(0, enemies.Count);
                    var bulletX = Instantiate(prefBullet, posContain);
                    bulletX.transform.position = this.transform.position;
                    if (enemies[random] != null)
                    {
                        var posEnemy = enemies[random].transform.position;
                        // bulletX.target = enemies[random].gameObject.transform;
                        bulletX.transform.DOMove(posEnemy, 0.2f);
                    }

                    currentTime = 0;
                }
            }
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