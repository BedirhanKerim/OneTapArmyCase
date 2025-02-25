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
     [SerializeField]  private SkinnedMeshRenderer[] allBodyParts;
     [SerializeField] private Material originalMaterial, deathMaterial;

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
           if (soldierMovement.teamType==TeamType.EnemyRed||soldierMovement.teamType==TeamType.EnemyYellow)
           {
               return;
           }
         //var buffs=  GameManager.Instance.upgradeManager.GetExtraSoldierBuffs(soldierType);
         var buffs = GameEventManager.Instance.OnOnExtraSoldierBuffsRequested(soldierType);
         soldierHealth.SetHp(buffs[0]);
         soldierAttack.SetDamage(buffs[1]);
         soldierMovement.SetSpeed(buffs[2]);

       }

       public void Die()
       {
           soldierMovement.Die();
           for (int i = 0; i < 4; i++)
           {
               allBodyParts[i].material = deathMaterial;
           }
           Invoke(nameof(SetNormalMaterial),1f);
       }

       private void SetNormalMaterial()
       {
           for (int i = 0; i < 4; i++)
           {
               allBodyParts[i].material = originalMaterial;
           }
       }
   }
}