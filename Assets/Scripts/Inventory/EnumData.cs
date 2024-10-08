using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BagFight
{
    public enum EnemyEffectState{
        Shock,
        Slow,
        Poison,
        Burn,
        RecieveCriticalHits
    }
    public enum StateTilesItem
    {
        HoverHighlight,
        HoverError,
        RemoveOldPosItem
    }

    public enum Rotate
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum StateTile
    {
        None,
        Wall
    }
}