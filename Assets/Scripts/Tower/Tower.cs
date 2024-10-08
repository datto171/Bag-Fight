using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;

namespace BagFight
{
    public class Tower : MonoBehaviour
    {
        // [SerializeField] Transform weapon;
        [SerializeField] private TowerConfig towerConfig;
        [SerializeField] private GameObject damageSubstance;
        [SerializeField] private Transform target;
        [SerializeField] private GameObject shootingPoint;
        [SerializeField] private GameObject spriteRenderer;
        private Item towerItemData;
        // private bool isActive;

        private float currentCooldown;

        private void Awake()
        {
            towerItemData = GetComponent<Item>();
        }

        private void Start()
        {

        }

        void Update()
        {
            if (towerItemData.invenContain != null)
            {
                FindClosestTarget();
            }
        }

        void FindClosestTarget()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            Transform closestTarget = null;
            float maxDistance = Mathf.Infinity;

            foreach (Enemy enemy in enemies)
            {
                float targetDistance = Vector2.Distance(transform.position, enemy.transform.position);

                if (targetDistance < maxDistance)
                {
                    // WIP - Change target from currentTarget to newTarget to clear / preserved the bullet that is still fired at the old target 
                    // ChangeTarget(target, closestTarget);
                    closestTarget = enemy.transform;
                    maxDistance = targetDistance;
                }
            }

            target = closestTarget;
            AimWeapon(target);
        }

        void ChangeTarget(Transform target, Transform newTarget)
        {
            // Need implementation
        }

        void AimWeapon(Transform currentTarget)
        {
            if (currentTarget == null)
            {
                return;
            }
            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(currentTarget.position.y - transform.position.y, currentTarget.position.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            spriteRenderer.transform.localRotation = Quaternion.Lerp(spriteRenderer.transform.localRotation, Quaternion.Euler(0, 0, AngleDeg), 0.1f);


            float targetDistance = Vector2.Distance(transform.position, currentTarget.position);

            if (targetDistance < towerConfig.Range)
            {
                Attack(true, currentTarget);
            }
            else
            {
                Attack(false, currentTarget);
            }
        }

        void Attack(bool isActive, Transform currentTarget)
        {
            if (isActive)
            {
                if (currentCooldown <= 0f)
                {
                    Fire(currentTarget);
                    currentCooldown = towerConfig.Speed;
                }
                else
                {
                    currentCooldown -= Time.deltaTime;
                }
            }
            else
            {
                currentCooldown -= Time.deltaTime;
            }
        }

        void Fire(Transform currentTarget)
        {
            var bullet = Instantiate(damageSubstance, shootingPoint.transform);
            var BulletData = bullet.GetComponent<Bullet>();

            BulletData.target = currentTarget;

            float critRandomizer = Random.Range(0.0f, 1.0f);
            if(critRandomizer <= towerConfig.CritChance){
                BulletData.damage = towerConfig.Damage * 2;
                BulletData.criticalHit = true;
            }
            else{
                BulletData.damage = towerConfig.Damage;
                BulletData.criticalHit = false;
            }
            BulletData.AOE = towerConfig.AOE;

            // nên có access Enum để có nhiều lựa chọn Effect pass cho Bullet (slow,shock,burn,poison)
            BulletData.slowEffect = towerConfig.SlowEffect;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, towerConfig.Range);
        }
    }
}