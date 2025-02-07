using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTapArmyCore
{
    public class MovementManager : MonoBehaviour
    {
        public Transform[] soldierPositionsObj; 
        public Transform[] soldierBasePositionsObj;
        public int totalWaitingSoldier;
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
        }

        public void MoveBaseQueue(Soldier soldier,bool isEnemy)
        {
            if (isEnemy)
            {
            }
            else
            {
                soldier.soldierMovement.SetMovementBaseData(soldierBasePositionsObj[totalWaitingSoldier].position);
                totalWaitingSoldier++;
            }
        }
    }
}