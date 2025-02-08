using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTapArmyCore
{
   public class Soldier : MonoBehaviour
   {
       public SoldierMovement soldierMovement;
       public SoldierAttack soldierAttack;
       public SoldierHealth soldierHealth;
       public IDamagable IDamagableRef;
       private void Start()
       {
           IDamagableRef = GetComponent<IDamagable>();
       }
   }
}