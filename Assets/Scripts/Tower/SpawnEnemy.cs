using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TowerDefense
{
    public class SpawnEnemy : MonoBehaviour
    {
        [SerializeField] private List<GameObject> listGates;
        [SerializeField] private float timeSpawn;
        [SerializeField] private Enemy prefEnemySpawn;
        [SerializeField] private Transform posContain;

        private void Start()
        {
            StartCoroutine(CreateEnemy());
        }

        IEnumerator CreateEnemy()
        {
            while (true)
            {
                int randomGate = Random.Range(0, listGates.Count);
                var enemyNew = Instantiate(prefEnemySpawn, posContain);
                enemyNew.transform.position = listGates[randomGate].transform.position;

                yield return new WaitForSeconds(timeSpawn);
            }
        }
    }
}