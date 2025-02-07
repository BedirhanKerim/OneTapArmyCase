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
                Debug.Log(soldier.name + " düşman listesine eklendi!");
            }
            else
            {
                MySoldiers.Add(soldier);
                GameManager.Instance.movementManager.MoveBaseQueue(soldier,false);
                Debug.Log(soldier.name + " benim orduma eklendi!");
            }
        }

        public void RemoveSoldier(Soldier soldier, bool isEnemy)
        {
            if (isEnemy)
            {
                EnemySoldiers.Remove(soldier);
                Debug.Log(soldier.name + " düşman listesinden çıkarıldı!");
            }
            else
            {
                MySoldiers.Remove(soldier);
                Debug.Log(soldier.name + " benim ordumdan çıkarıldı!");
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