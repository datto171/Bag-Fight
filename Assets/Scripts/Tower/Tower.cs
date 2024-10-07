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
                // AimWeapon();
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

        void ChangeTarget(Transform target, Transform newTarget){
            // Need implementation
        }

        void AimWeapon(Transform currentTarget)
        {
            // var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // mouseWorldPos.z = 0f;

            // Target = new Vector2(mouseWorldPos.x, mouseWorldPos.y);

            if (currentTarget == null)
            {
                return;
            }
            // Get Angle in Radians
            float AngleRad = Mathf.Atan2(currentTarget.position.y - transform.position.y, currentTarget.position.x - transform.position.x);
            // Get Angle in Degrees
            float AngleDeg = (180 / Mathf.PI) * AngleRad;
            // Rotate Object
            spriteRenderer.transform.localRotation = Quaternion.Lerp(spriteRenderer.transform.localRotation,Quaternion.Euler(0, 0, AngleDeg),0.1f);

            // target = mouseWorldPos;

            float targetDistance = Vector2.Distance(transform.position, currentTarget.position);

            // transform.LookAt(target);
            // Debug.Log(targetDistance);

            if (targetDistance < towerConfig.Range)
            {
                Attack(true, currentTarget);
            }
            else
            {
                Attack(false, currentTarget);
            }
        }

        // void Attack(bool isActive)
        // {
        //     var emissionModule = projectileParticles.emission;
        //     emissionModule.enabled = isActive;
        // }

        void Attack(bool isActive, Transform currentTarget)
        {
            if (isActive)
            {
                // var emissionModule = towerConfig.DamagingSubstance.emission;
                // emissionModule.enabled = isActive;
                if (currentCooldown <= 0f)
                {
                    // RequestFireBullet(this, )
                    var bullet = Instantiate(damageSubstance, shootingPoint.transform);
                    var BulletData = bullet.GetComponent<Bullet>();
                    BulletData.target = currentTarget;
                    // BulletData.lastTargetPosition = target.position;
                    BulletData.damage = towerConfig.Damage;
                    BulletData.AOE = towerConfig.AOE;

                    // nên có access Enum để có nhiều lựa chọn Effect pass cho Bullet (slow,shock,burn,poison)
                    BulletData.slowEffect = towerConfig.SlowEffect;

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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, towerConfig.Range);
        }
    }
}