using System;
using System.Collections;
using System.Collections.Generic;
using Ftech.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ObjectPool_1
{
    public class Cube : MonoBehaviour, IPooledObject
    {
        public float upForce = 1f;
        public float sideForce = 0.1f;

        private float lifeTime = 2f;

        public void OnObjectSpawn()
        {
            float xForce = Random.Range(-sideForce, sideForce);
            float yForce = Random.Range(upForce / 2f, upForce);
            float zForce = Random.Range(-sideForce, sideForce);

            Vector3 force = new Vector3(xForce, yForce, zForce);

            GetComponent<Rigidbody>().velocity = force;
        }

        private void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                ObjectPool.Recycle(this.gameObject);
                lifeTime = 2f;
            }
        }
    }
}