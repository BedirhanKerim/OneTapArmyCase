using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.UI;
using static OneTapArmyCore.Enums;

namespace OneTapArmyCore
{
    public class SoldierHealth : MonoBehaviour, IDamagable
    {
        private float maxHp, currentHp;
        [SerializeField] private float _baseHp;
        public TeamType teamType;
        private bool isAlive;
        [SerializeField] private Image hpBar;

        private void OnEnable()
        {
            isAlive = true;
            maxHp = _baseHp;
            currentHp = maxHp;
            hpBar.fillAmount = 1;
        }

        public void SetHp(float extraBuffHp)
        {
            maxHp = (extraBuffHp / 100 * _baseHp) + _baseHp;
            currentHp = maxHp;
        }

        public void TakeDamage(float damage)
        {
            if (!isAlive)
            {
                return;
            }

            currentHp -= damage;
            hpBar.fillAmount = currentHp / maxHp;
            if (currentHp <= 0)
            {
                isAlive = false;

                var soldier = GetComponent<Soldier>();
                soldier.Die();
                if (teamType == TeamType.Player)
                {
                    GameManager.Instance.armyManager.RemoveSoldier(soldier, false);
                }
                else
                {
                    GameManager.Instance.armyManager.RemoveSoldier(soldier, true);
                }

                GameManager.Instance.AddExp(1);
                Invoke(nameof(Destroy), 1f);
            }
        }

        private void Destroy()
        {
            var gObj = this.gameObject;
            LeanPool.Despawn(gObj);
            gObj.SetActive(false);
        }

        public bool IsAlive()
        {
            return isAlive;
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}