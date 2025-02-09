using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OneTapArmyCore.Enums;
namespace OneTapArmyCore
{
    public class SoldierAttack : MonoBehaviour
    {
        public IDamagable targetSoldier;
        public SoldierType soldierType;
        public float range;
        private bool bCanAttack = true;
        public AttackAnimHandler attackAnimHandler;
        [SerializeField] private Transform bulletSpawnLoc;
        private float _baseDamage;
        private float damage;
        private void Start()
        {
            attackAnimHandler.Attack += Attack;

        }

        public void SetDamage(float extraBuffDamage)
        {
            damage = (extraBuffDamage / 100 * _baseDamage) + _baseDamage;
        }
        public void CheckRange(float distance)
        {
            if (distance < range && bCanAttack)
            {
                bCanAttack = true;
            }
            else
            {
                bCanAttack = false;

            }
        }
        public void Attack()
        {
            if (!bCanAttack&&targetSoldier==null)
            {return;
            }

            switch (soldierType)
            {
                case SoldierType.Melee:
                    targetSoldier.TakeDamage(damage);
                    break;
                case SoldierType.Range:
                  GameManager.Instance.bulletManager.SpawnBullet(bulletSpawnLoc.position, targetSoldier, damage);
                    break;
            }
            //audioSource.Play();

        }
    }
}