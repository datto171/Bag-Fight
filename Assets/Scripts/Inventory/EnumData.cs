using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BagFight
{
    public enum StateTowersItem{
        Active,
        Inactive
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