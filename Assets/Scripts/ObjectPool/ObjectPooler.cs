using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool_1
{
    public class ObjectPooler : MonoBehaviour
    {
        #region Singleton

        public static ObjectPooler Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        public List<Pool> pools;
        private Dictionary<string, Queue<GameObject>> poolDictionary;

        private void Start()
        {
            poolDictionary = new Dictionary<string, Queue<GameObject>>();

            foreach (Pool pool in pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for (int i = 0; i < pool.size; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
            }
        }

        public GameObject SpawnFromPool(string tag, Vector3 pos, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(tag))
            {
                Debug.LogWarning("Pool with tag" + tag + "doesn't exist");
                return null;
            }

            GameObject objToSpawn = null;
            if (poolDictionary[tag].TryDequeue(out var item))
            {
                objToSpawn = item;
                objToSpawn.SetActive(true);
                objToSpawn.transform.position = pos;
                objToSpawn.transform.rotation = rotation;

                IPooledObject pooledObject = objToSpawn.GetComponent<IPooledObject>();
                if (pooledObject != null)
                {
                    pooledObject.OnObjectSpawn();
                }
            }

            GameObject objectCreate = null;
            foreach (var pool in pools)
            {
                if (pool.tag == tag)
                {
                    objectCreate = pool.prefab;
                }
            }
            
            objToSpawn = Instantiate(objectCreate);
            objToSpawn.SetActive(true);
            objToSpawn.transform.position = pos;
            objToSpawn.transform.rotation = rotation;
            poolDictionary[tag].Enqueue(objToSpawn);

            return objToSpawn;
        }

        public void EnqueueObject(string tagPref,GameObject objRecycle)
        {
            poolDictionary[tagPref].Enqueue(objRecycle);
        }
    }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
}