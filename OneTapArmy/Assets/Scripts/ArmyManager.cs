using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTapArmyCore
{
    public class ArmyManager : MonoBehaviour
    {
        public List<Soldier> MySoldiers = new List<Soldier>();
        public List<Soldier> EnemySoldiers = new List<Soldier>();
        public List<Soldier> EnemySoldiers2= new List<Soldier>();

        private void Start()
        {
            GameEventManager.Instance.OnRemoveSoldier += RemoveSoldier;
            GameEventManager.Instance.OnAddSoldier += AddSoldier;
            GameEventManager.Instance.OnSoldierListRequested += GetSoldierList;

        }

        public void AddSoldier(Soldier soldier, int playerIndex)
        {
            if (playerIndex==0)
            {
                MySoldiers.Add(soldier);
               // GameManager.Instance.movementManager.MoveBaseQueue(soldier,0);
            }
            else if (playerIndex==1)
            {
                EnemySoldiers.Add(soldier);
            }
            else if (playerIndex == 2)
            {
                EnemySoldiers2.Add(soldier);
            }
            GameEventManager.Instance.OnOnSpawnToMoveBaseQueue(soldier,playerIndex);

        }

     
        public void RemoveSoldier(Soldier soldier,int playerIndex)
        {

            if (playerIndex==0)
            {
                MySoldiers.Remove(soldier);

            }
            else if (playerIndex==1)
            {
                EnemySoldiers.Remove(soldier);

            }
            else if (playerIndex == 2)
            {
                EnemySoldiers2.Remove(soldier);
            }
        }

        public List<Soldier> GetSoldierList( int playerIndex)
        {
            if (playerIndex==0)
            {
                return MySoldiers;

            }
            else if (playerIndex==1)
            {
                return EnemySoldiers;

            }
            else if (playerIndex == 2)
            {
                return EnemySoldiers2;
            }

            return null;
        }
    }
}