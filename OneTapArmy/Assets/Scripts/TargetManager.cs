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

        private void Update()
        {
            SetTargets();
        }

        private void SetTargets()
        {
            var allies = GameManager.Instance.armyManager.MySoldiers;
            var enemies = GameManager.Instance.armyManager.EnemySoldiers;

            foreach (Soldier mySoldier in allies)
            {
                Transform nearestEnemy = null;
                IDamagable damagableRef=null;

                float nearestDistance = 100;
                if (mySoldier.soldierAttack.soldierType == SoldierType.Melee)
                {
                    nearestDistance = soldierAggroMelee;

                }
                else if (mySoldier.soldierAttack.soldierType == SoldierType.Range)
                {
                    nearestDistance = soldierAggroRange;

                }

                foreach (Soldier enemySoldier in enemies)
                {
                    float distanceForDrone =
                        (enemySoldier.transform.position - mySoldier.transform.position).sqrMagnitude;

                    if (distanceForDrone < nearestDistance)
                    {
                        nearestDistance = distanceForDrone;
                        nearestEnemy = enemySoldier.transform;
                        damagableRef = enemySoldier.IDamagableRef;

                    }

                }
                mySoldier.soldierMovement.GetTargetTransform(nearestEnemy,nearestDistance,damagableRef);
                
            }
            
            
            
            foreach (Soldier mySoldier in enemies)
            {
                Transform nearestEnemy = null;
                IDamagable damagableRef=null;
                float nearestDistance = 100;
                if (mySoldier.soldierAttack.soldierType == SoldierType.Melee)
                {
                    nearestDistance = soldierAggroMelee;

                }
                else if (mySoldier.soldierAttack.soldierType == SoldierType.Range)
                {
                    nearestDistance = soldierAggroRange;

                }

                foreach (Soldier enemySoldier in allies)
                {
                    float distanceForDrone =
                        (enemySoldier.transform.position - mySoldier.transform.position).sqrMagnitude;

                    if (distanceForDrone < nearestDistance)
                    {
                        nearestDistance = distanceForDrone;
                        nearestEnemy = enemySoldier.transform;
                        damagableRef = enemySoldier.IDamagableRef;
                    }

                }

               
                mySoldier.soldierMovement.GetTargetTransform(nearestEnemy,nearestDistance,damagableRef);
                
            }
        }
    }
}