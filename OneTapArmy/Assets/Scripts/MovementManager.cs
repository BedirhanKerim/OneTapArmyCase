using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTapArmyCore
{
    public class MovementManager : MonoBehaviour
    {
        public Transform[] soldierPositionsObj; 
        public Transform[] soldierBasePositionsObj;
        public Transform[] enemySoldierBasePositionsObj;
        public int totalWaitingSoldier,totalWaitingSoldierEnemy;
        public void MovePlayerArmy()
        {
            int i = 0;
            var soldierList = GameManager.Instance.armyManager.GetSoldierList(false);
            foreach (Soldier enemySoldier in soldierList)
            {
                //float distanceForTargetLoc = (enemySoldier.transform.position - movementPosition).sqrMagnitude;

               
                    enemySoldier.soldierMovement.SetMovementData(soldierPositionsObj[i].position);
                    i++;

            }
            GameManager.Instance.movementManager.totalWaitingSoldier = 0;

        }

        public void MoveBaseQueue(Soldier soldier,bool isEnemy)
        {
            if (isEnemy)
            {
                soldier.soldierMovement.SetMovementBaseData(enemySoldierBasePositionsObj[totalWaitingSoldierEnemy].position);
                totalWaitingSoldierEnemy++;
            }
            else
            {
                soldier.soldierMovement.SetMovementBaseData(soldierBasePositionsObj[totalWaitingSoldier].position);
                totalWaitingSoldier++;
            }
        }
    }
}