using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjPool2
{
    public static class ObjectPooler
    {
        public static Dictionary<string, Queue<Component>> poolDict = new Dictionary<string, Queue<Component>>();

        public static void EnQueueObject<T>(T item, string name) where T : Component
        {
            if (!item.gameObject.activeSelf)
            {
                return;
            }

            item.transform.position = Vector2.zero;
            poolDict[name].Enqueue(item);
            item.gameObject.SetActive(false);
        }

        public static T DequeueObject<T>(string key) where T : Component
        {
            return (T)poolDict[key].Dequeue();
        }

        public static void SetupPool<T>(T pooledItemPref, int poolSize, string dictEntry) where T : Component
        {
            poolDict.Add(dictEntry, new Queue<Component>());

            for (int i = 0; i < poolSize; i++)
            {
                T pooledInstance = Object.Instantiate(pooledItemPref);
                pooledInstance.gameObject.SetActive(false);
                poolDict[dictEntry].Enqueue((T)pooledInstance);
            }
        }
    }
}