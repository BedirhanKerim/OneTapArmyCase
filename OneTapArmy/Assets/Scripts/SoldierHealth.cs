using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OneTapArmyCore.Enums;

namespace OneTapArmyCore
{
    public class SoldierHealth : MonoBehaviour, IDamagable
    {
        public float maxHp, currentHp;
        public TeamType teamType;
        private bool isAlive;
        private void Start()
        {
            currentHp = maxHp;
            isAlive = true;
        }

        public void TakeDamage(float damage)
        {
            if (!isAlive)
            {
                return;
            }
            currentHp -= damage;
            if (currentHp <= 0)
            {
                isAlive = false;
            var soldier=GetComponent<Soldier>();
            if (teamType==TeamType.Player)
            {
                GameManager.Instance.armyManager.RemoveSoldier(soldier,false);

            }
            else
            {
                GameManager.Instance.armyManager.RemoveSoldier(soldier,true);

            }
                Destroy(this.gameObject);
            }
        }

        public bool IsAlive()
        {
            return true;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}