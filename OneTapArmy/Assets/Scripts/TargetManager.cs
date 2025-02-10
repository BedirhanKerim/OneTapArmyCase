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
        [SerializeField] private Transform playerCastle, enemyCastle;
         private IDamagable _playerCastleIDamagable, _enemyCastleIDamagable;

         private void Start()
         {
             _playerCastleIDamagable = playerCastle.GetComponent<IDamagable>();
             _enemyCastleIDamagable = enemyCastle.GetComponent<IDamagable>();

         }

         private void Update()
        {
            SetTargets();
        }

         private void SetTargets()
         {
             AssignTargets(GameManager.Instance.armyManager.MySoldiers, enemyCastle, _enemyCastleIDamagable, GameManager.Instance.armyManager.EnemySoldiers);
             AssignTargets(GameManager.Instance.armyManager.EnemySoldiers, playerCastle, _playerCastleIDamagable, GameManager.Instance.armyManager.MySoldiers);
         }

         private void AssignTargets(List<Soldier> soldiers, Transform castle, IDamagable castleDamagable, List<Soldier> enemies)
         {
             foreach (Soldier soldier in soldiers)
             {
                 Transform nearestEnemy = null;
                 IDamagable damagableRef = null;
                 float nearestDistance = (soldier.soldierAttack.soldierType == SoldierType.Melee) ? soldierAggroMelee : soldierAggroRange;

                 float castleDistance = (castle.position - soldier.transform.position).sqrMagnitude;
                 if (castleDistance < nearestDistance)
                 {
                     nearestDistance = castleDistance;
                     nearestEnemy = castle;
                     damagableRef = castleDamagable;
                 }

                 foreach (Soldier enemy in enemies)
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