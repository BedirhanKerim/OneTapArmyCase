using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OneTapArmyCore.Enums;

namespace OneTapArmyCore
{
    public class TargetManager : MonoBehaviour
    {
        [SerializeField] private float soldierAggroMelee, soldierAggroRange;
        [SerializeField] private Transform playerCastle, enemyCastle,enemyCastle2;
         private IDamagable _playerCastleIDamagable, _enemyCastleIDamagable,_enemyCastleIDamagable2;

         private void Start()
         {
             _playerCastleIDamagable = playerCastle.GetComponent<IDamagable>();
             _enemyCastleIDamagable = enemyCastle.GetComponent<IDamagable>();
             _enemyCastleIDamagable2 = enemyCastle2.GetComponent<IDamagable>();

         }

         private void Update()
        {
            SetTargets();
        }

         private void SetTargets()
         {

          /* AssignTargets(GameManager.Instance.armyManager.MySoldiers, enemyCastle,enemyCastle2, _enemyCastleIDamagable,
               _enemyCastleIDamagable2, GameManager.Instance.armyManager.EnemySoldiers, GameManager.Instance.armyManager.EnemySoldiers2);
           
           AssignTargets(GameManager.Instance.armyManager.EnemySoldiers, playerCastle,enemyCastle2, _playerCastleIDamagable,
               _enemyCastleIDamagable2, GameManager.Instance.armyManager.MySoldiers, GameManager.Instance.armyManager.EnemySoldiers2);
           
             AssignTargets(GameManager.Instance.armyManager.EnemySoldiers2, playerCastle,enemyCastle, _playerCastleIDamagable,
                 _enemyCastleIDamagable, GameManager.Instance.armyManager.MySoldiers, GameManager.Instance.armyManager.EnemySoldiers);
*/

          var mySoldierList = GameEventManager.Instance.OnOnSoldierListRequested(0);
          var enemySoldierList = GameEventManager.Instance.OnOnSoldierListRequested(1);
          var enemy2SoldierList = GameEventManager.Instance.OnOnSoldierListRequested(2);

          AssignTargets(mySoldierList, enemyCastle,enemyCastle2, _enemyCastleIDamagable,
              _enemyCastleIDamagable2, enemySoldierList, enemy2SoldierList);
           
          AssignTargets(enemySoldierList, playerCastle,enemyCastle2, _playerCastleIDamagable,
              _enemyCastleIDamagable2, mySoldierList, enemy2SoldierList);
           
          AssignTargets(enemy2SoldierList, playerCastle,enemyCastle, _playerCastleIDamagable,
              _enemyCastleIDamagable, mySoldierList, enemySoldierList);

         }

         private void AssignTargets(List<Soldier> soldiers, Transform castle,Transform castle2, IDamagable castleDamagable,IDamagable castleDamagable2, List<Soldier> enemies, List<Soldier> enemies2)
         {
             List<Soldier> allEnemies=new ();
             allEnemies.AddRange(enemies);
             allEnemies.AddRange(enemies2);

             foreach (Soldier soldier in soldiers)
             {
                 Transform nearestEnemy = null;
                 IDamagable damagableRef = null;
                // float nearestDistance = (soldier.soldierAttack.soldierType == SoldierType.Melee) ? soldierAggroMelee : soldierAggroRange;
                float nearestDistance = soldier.soldierAttack.aggroRange;
                 float castleDistance = (castle.position - soldier.transform.position).sqrMagnitude;
                 float castleDistance2 = (castle2.position - soldier.transform.position).sqrMagnitude;

                 if (castleDistance < nearestDistance)
                 {
                     nearestDistance = castleDistance;
                     nearestEnemy = castle;
                     damagableRef = castleDamagable;
                 }
                 else if (castleDistance2 < nearestDistance)
                 {
                     nearestDistance = castleDistance2;
                     nearestEnemy = castle2;
                     damagableRef = castleDamagable2;
                 }

                 foreach (Soldier enemy in allEnemies)
                 {
                     float enemyDistance = (enemy.transform.position - soldier.transform.position).sqrMagnitude;
                     if (enemyDistance < nearestDistance)
                     {
                         nearestDistance = enemyDistance;
                         nearestEnemy = enemy.transform;
                         damagableRef = enemy.IDamagableRef;
                     }
                 }

                 soldier.soldierMovement.GetTargetTransform(nearestEnemy, nearestDistance, damagableRef);
             }
         }

    }
}