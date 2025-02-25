using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OneTapArmyCore.Enums;

namespace OneTapArmyCore
{
    public abstract class SoldierAttack : MonoBehaviour
    {
        public IDamagable targetSoldier;
        public float range;
        public float aggroRange;

        protected bool bCanAttack = true;
        public AttackAnimHandler attackAnimHandler;
        [SerializeField] protected float _baseDamage;
        protected float damage;
        public AudioSource audioSource;

        protected virtual void Awake()
        {
            damage = _baseDamage;
        }

        protected virtual void Start()
        {
            attackAnimHandler.Attack += Attack;
        }

        public void SetDamage(float extraBuffDamage)
        {
            damage = (extraBuffDamage / 100 * _baseDamage) + _baseDamage;
        }

        public void CheckRange(float distance)
        {
            bCanAttack = distance < range;
        }


        public void SpawnParticle()
        {
            GameEventManager.Instance.OnOnDamageParticleSpawn(targetSoldier.GetTransform().position);

        }
        public abstract void Attack();
    }
}