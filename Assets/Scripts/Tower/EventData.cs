using System.Collections;
using System.Collections.Generic;
using AdvancedModule.ObserverPattern;
using UnityEngine;

namespace TowerDefense
{
    public struct KillEnemyEvent : IEventData
    {
        public GameObject enemy;
    }
}

