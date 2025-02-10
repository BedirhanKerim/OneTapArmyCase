using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTapArmyCore
{
    public class ArmyManager : MonoBehaviour
    {
        public List<Soldier> MySoldiers = new List<Soldier>();
        public List<Soldier> EnemySoldiers = new List<Soldier>();


        public void AddSoldier(Soldier soldier, bool isEnemy)
        {
            if (isEnemy)
            {
                EnemySoldiers.Add(soldier);
                GameManager.Instance.movementManager.MoveBaseQueue(soldier,true);
            }
            else
            {
                MySoldiers.Add(soldier);
                GameManager.Instance.movementManager.MoveBaseQueue(soldier,false);
            }
        }

        public void RemoveSoldier(Soldier soldier, bool isEnemy)
        {
            if (isEnemy)
            {
                EnemySoldiers.Remove(soldier);
            }
            else
            {
                MySoldiers.Remove(soldier);
            }
        }

        public List<Soldier> GetSoldierList( bool isEnemy)
        {
            if (isEnemy)
            {
                return EnemySoldiers;
            }
            else
            {
                return MySoldiers;

            } 
        }
    }
}