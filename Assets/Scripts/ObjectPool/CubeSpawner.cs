using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool_1
{
    public class CubeSpawner : MonoBehaviour
    {
        private ObjectPooler objectPooler;

        private void Start()
        {
            objectPooler = ObjectPooler.Instance;
        }

        private void FixedUpdate()
        {
            objectPooler.SpawnFromPool("Cube", transform.position, Quaternion.identity);
        }
    }
}