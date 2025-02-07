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
    }
}