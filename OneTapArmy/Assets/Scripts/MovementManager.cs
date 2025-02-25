using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OneTapArmyCore
{
    public class MovementManager : MonoBehaviour
    {
       [SerializeField] private Transform[] soldierPositionsObj;
       [SerializeField] private Transform[] soldierBasePositionsObj;
       [SerializeField] private Transform[] enemySoldierBasePositionsObj;
       [SerializeField] private Transform[] enemySoldierBasePositionsObj2;
        public int totalWaitingSoldier, totalWaitingSoldierEnemy, totalWaitingSoldierEnemy2;

        private void Start()
        {
            GameEventManager.Instance.OnSpawnToMoveBaseQueue += MoveBaseQueue;
            GameEventManager.Instance.OnMoveEnemyArmy1 += MoveEnemyArmy;
            GameEventManager.Instance.OnMoveEnemyArmy2 += MoveEnemyArmy2;
            GameEventManager.Instance.OnMovePlayerArmy += MovePlayerArmy;


        }

        public void MovePlayerArmy()
        {
            int i = 0;
           // var soldierList = GameManager.Instance.armyManager.GetSoldierList(0);
            var soldierList = GameEventManager.Instance.OnOnSoldierListRequested(0);

            foreach (Soldier enemySoldier in soldierList)
            {
                //float distanceForTargetLoc = (enemySoldier.transform.position - movementPosition).sqrMagnitude;


                enemySoldier.soldierMovement.SetMovementData(soldierPositionsObj[i].position);
                i++;
                i %= 35;
            }

            totalWaitingSoldier = 0;
        }

        public void MoveBaseQueue(Soldier soldier, int playerIndex)
        {
            if (playerIndex == 0)
            {
                soldier.soldierMovement.SetMovementBaseData(soldierBasePositionsObj[totalWaitingSoldierEnemy]
                    .position);
                totalWaitingSoldierEnemy++;
                totalWaitingSoldierEnemy %= 35;
            }
            else if (playerIndex == 1)
            {
                soldier.soldierMovement.SetMovementBaseData(enemySoldierBasePositionsObj[totalWaitingSoldier].position);
                totalWaitingSoldier++;
                totalWaitingSoldier %= 35;
            }
            else if (playerIndex == 2)
            {
                soldier.soldierMovement.SetMovementBaseData(enemySoldierBasePositionsObj2[totalWaitingSoldierEnemy2].position);
                totalWaitingSoldierEnemy2++;
                totalWaitingSoldierEnemy2 %= 35;
            }
        }

        public void MoveEnemyArmy()
        {
            int rnd = Random.Range(0, 100);
            //var soldierList = GameManager.Instance.armyManager.GetSoldierList(1);
            var soldierList = GameEventManager.Instance.OnOnSoldierListRequested(1);

            if (rnd > 50)
            {
                foreach (Soldier enemySoldier in soldierList)
                {
                    enemySoldier.soldierMovement.SetMovementData(GameManager.Instance.playerBase.transform.position);
                }
            }
            else
            {
                foreach (Soldier enemySoldier in soldierList)
                {
                    enemySoldier.soldierMovement.SetMovementData(GameManager.Instance.enemyBase2.transform.position);
                }
            }

            totalWaitingSoldierEnemy = 0;
        }


        public void MoveEnemyArmy2()
        {
            int rnd = Random.Range(0, 100);
            //var soldierList = GameManager.Instance.armyManager.GetSoldierList(2);
            var soldierList = GameEventManager.Instance.OnOnSoldierListRequested(2);

            if (rnd > 50)
            {
                foreach (Soldier enemySoldier in soldierList)
                {
                    enemySoldier.soldierMovement.SetMovementData(GameManager.Instance.enemyBase.transform.position);
                }

            }
            else
            {
                foreach (Soldier enemySoldier in soldierList)
                {
                    enemySoldier.soldierMovement.SetMovementData(GameManager.Instance.playerBase.transform.position);
                }

            }
            totalWaitingSoldierEnemy2 = 0;

        }

       
    }
}