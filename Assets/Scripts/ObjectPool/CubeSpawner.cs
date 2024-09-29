using System;
using System.Collections;
using System.Collections.Generic;
using Ftech.Utilities;
using UnityEngine;

namespace ObjectPool_1
{
    public class CubeSpawner : MonoBehaviour
    {
        // private ObjectPooler objectPooler;

        private ObjectPool objectPool;
        [SerializeField] private GameObject obj;

        private void Start()
        {
            // objectPooler = ObjectPooler.Instance;

            objectPool = ObjectPool.instance;
        }

        private void FixedUpdate()
        {
            var item = ObjectPool.Spawn(obj, transform.position);
            item.gameObject.SetActive(true);
            item.transform.position = new Vector3(1, 1, 1);
        }
    }
}