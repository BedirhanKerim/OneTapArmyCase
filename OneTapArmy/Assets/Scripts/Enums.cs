using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTapArmyCore
{
    public class Enums : MonoBehaviour
    {
        public enum SoldierBehaviourState
        {
            None,
            WaitingOnBase,
            Waiting,
            Walking,
            Charging,
            Attacking
        }

        public enum TeamType
        {
            Player,
            Enemy
        }

        public enum SoldierType
        {
            Melee,
            Range
        }

        public enum EUpgradeType
        {
            Empty,
            Swordsman,
            Warrior,
            Horseman,
            Archer,
            Castle,
            Giant
        }
    }
}