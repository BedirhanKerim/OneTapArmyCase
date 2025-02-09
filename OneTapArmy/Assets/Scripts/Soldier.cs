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
       private void Start()
       {
           IDamagableRef = GetComponent<IDamagable>();
       }

       private void GetAllBuffs()
       {
         var buffs=  GameManager.Instance.upgradeManager.GetExtraSoldierBuffs(soldierType);
         
         soldierAttack.SetDamage(buffs[1]);
       }
   }
}