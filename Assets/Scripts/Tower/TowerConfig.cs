using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BagFight
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "ScriptableObjects/TowerConfig")]
    public class TowerConfig : ScriptableObject
    {

        [Tooltip("float Damage 0.1f to 200f")]
        [SerializeField] [Range(0.1f,200f)] private float damage;

        [Tooltip("int Range 0 to 20 (unit)")]
        [SerializeField] [Range(0,20)] private int range;

        [Tooltip("float AOE 0 to 20 (unit)")]
        [SerializeField] [Range(0.5f,20f)] private float areaOfEffect;

        [Tooltip("float Speed 0.1f to 10f | lower = faster attack rate")]
        [SerializeField] [Range(0.1f, 10f)] private float speed;

        // Slow 1f = Stun
        [Tooltip("float Slow 0f to 1f | Slow 1f = Stun")]
        [SerializeField] [Range(0f,1f)] private float slowEffect;

        [Tooltip("float CritChance 0f to 1f")]
        [SerializeField] [Range(0f,1f)] private float critChance;

        // công cụ để gây sát thương của trụ
        [SerializeField] private ParticleSystem damagingSubstance;

        public float Damage => damage;
        public int Range => range;
        public float AOE => areaOfEffect;
        public float Speed => speed;
        public float SlowEffect => slowEffect;
        public float CritChance => critChance;

        public ParticleSystem DamagingSubstance => damagingSubstance;

    }
}
