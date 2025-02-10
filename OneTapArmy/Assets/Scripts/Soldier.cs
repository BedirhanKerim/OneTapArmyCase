using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OneTapArmyCore.Enums;

namespace OneTapArmyCore
{
   public class Soldier : MonoBehaviour
   {
       public EUpgradeType soldierType;
       public SoldierMovement soldierMovement;
       public SoldierAttack soldierAttack;
       public SoldierHealth soldierHealth;
       public IDamagable IDamagableRef;
     

       private void Awake()
       {
         Invoke(nameof(GetAllBuffs),2f); 
       }
       private void Start()
       {
           IDamagableRef = GetComponent<IDamagable>();
       }
       private void GetAllBuffs()
       {
           if (soldierMovement.teamType==TeamType.Enemy)
           {
               return;
           }
         var buffs=  GameManager.Instance.upgradeManager.GetExtraSoldierBuffs(soldierType);
         soldierHealth.SetHp(buffs[0]);
         soldierAttack.SetDamage(buffs[1]);
         soldierMovement.SetSpeed(buffs[2]);

       }

       public void Die()
       {
           soldierMovement.Die();
       }
   }
}