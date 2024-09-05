using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float moveSpeed;
        
        // Start is called before the first frame update
        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;            
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
}