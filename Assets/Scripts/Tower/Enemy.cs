using System;
using System.Collections;
using System.Collections.Generic;
using AdvancedModule.ObserverPattern;
using BagFight;
using UnityEngine;

namespace TowerDefense
{
    public class Enemy : MonoBehaviour
    {
        // [SerializeField] private Transform target;
        [SerializeField] private EnemyConfig enemyConfig;
        [SerializeField] private float currentHP;
        public float currentMS;
        private bool isAlive = true;

        private void OnEnable()
        {
            currentHP = enemyConfig.HP;
            currentMS = enemyConfig.MoveSpeed;
        }

        // Start is called before the first frame update
        void Start()
        {
            // target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isAlive)
            {
                transform.position = transform.position;
            }
            else
            {
                transform.position += Vector3.right * currentMS * Time.deltaTime;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Bullet>())
            {
                var bullet = other.gameObject.GetComponent<Bullet>();
                if (bullet.target == this.transform)
                {
                    Destroy(bullet.gameObject);

                    ApplyEffect(bullet.slowEffect);
                    CalculateHP(bullet.damage);
                }
                // Destroy(gameObject);
            }
            
            if (other.gameObject.GetComponent<BulletAOEEffect>()){
                var bulletAOEEffect = other.gameObject.GetComponent<BulletAOEEffect>();
                CalculateHP(bulletAOEEffect.AOEdamage);
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            TowerConfig towerConfig = other.transform.parent.GetComponent<TowerConfig>();

            // Destroy(other.gameObject);   
        }
        private void OnParticleTrigger()
        {
            Debug.Log("Triggered");
        }

        private void ApplyEffect(float slowEffect)
        {
            // enemyConfig.MoveSpeed -= (slowEffect*100)/100;
            currentMS = enemyConfig.MoveSpeed - (slowEffect * 100) * enemyConfig.MoveSpeed / 100;
        }

        private void CalculateHP(float damage)
        {
            currentHP -= damage;
            if (currentHP <= 0f)
            {
                isAlive = false;
                EventDispatcher.SceneInstance.TriggerEvent(new KillEnemyEvent
                {
                    enemy = this.gameObject
                });
                // StartCoroutine(InitiateCleanupSequence());
                Destroy(gameObject);
                Debug.Log("Nooooo! Datto whyyyy?");
            }
        }

        private IEnumerator InitiateCleanupSequence()
        {
            // Wait for Death / Cleanup time animation, should be set explicitly
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
}