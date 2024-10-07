using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BagFight
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [Tooltip("float Movement Speed 0.1f to 10f")]
        [SerializeField][Range(0.1f, 10f)] private float moveSpeed;

        [Tooltip("float Hit Points 0,1f to 1500f")]
        [SerializeField][Range(0.1f, 1500f)] private float hitPoints;

        public float MoveSpeed => moveSpeed;
        public float HP => hitPoints;
    }
}
