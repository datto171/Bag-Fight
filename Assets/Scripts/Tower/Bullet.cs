using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AdvancedModule.ObserverPattern;

namespace TowerDefense
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public float damage;
        public float slowEffect;
        public float AOE;
        private CircleCollider2D bulletAOE;

        public Transform target;
        private Vector3 lastTargetPosition;
        private float distanceToTarget;
        [SerializeField] private GameObject bulletAOEEffect;
        // private CircleCollider2D bulletAOEEffectCollider;

        private void Start()
        {
            bulletAOE = GetComponent<CircleCollider2D>();
            // bulletAOEEffectCollider = bulletAOEEffect.GetComponent<CircleCollider2D>();

            // Handle AOE Damage
            if (AOE > 0.5f)
            { // If the AOE bigger than standard normal bullet AOE => deal AOE Damage, not initial Damage
                bulletAOE.enabled = false;
                // bulletAOEEffectCollider.radius = AOE;
            }
            else
            {
                bulletAOE.enabled = true;
            }

        }

        private void Update()
        {
            distanceToTarget = Vector2.Distance(lastTargetPosition, transform.position);

            if (target != null)
            {
                lastTargetPosition = target.position;
                transform.position = Vector3.MoveTowards(transform.position, target.position, speed * speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, lastTargetPosition, speed * speed * Time.deltaTime);
            }

            if (distanceToTarget <= 0.1f)
            {
                HandleEndOfTravelEvent();
            }
        }

        void HandleEndOfTravelEvent()
        {
            if (AOE > 0.5f)
            {
                var bulletAOEInstance = Instantiate(bulletAOEEffect, transform.position, Quaternion.identity, this.gameObject.transform.parent.gameObject.transform);
                bulletAOEInstance.GetComponent<BulletAOEEffect>().AOEdamage = damage;
                bulletAOEInstance.GetComponent<BulletAOEEffect>().AOE = AOE;
            }
            Destroy(gameObject);
        }
    }
}