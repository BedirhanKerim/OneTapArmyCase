using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Events;
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
        private int playerIndex;
        [SerializeField] private Image hpBar;
        public UnityAction OnSoldierDeath;

        private void Awake()
        {
            switch (teamType)
            {
                case TeamType.PlayerBlue:
                    playerIndex = 0;
                    break;
                case TeamType.EnemyRed:
                    playerIndex = 1;
                    break;
                case TeamType.EnemyYellow:
                    playerIndex = 2;
                    break;
            }
        }

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
                OnSoldierDeath?.Invoke();
                var soldier = GetComponent<Soldier>();
                soldier.Die();
                if (playerIndex != 0)
                {
                    //GameManager.Instance.coinSpawner.SendUiCoin(transform, null, 1);
                    for (int i = 0; i < 3; i++)
                    {
                        GameEventManager.Instance.OnOnCoinSpawn(transform, null, 1);

                    }


                }

               // GameManager.Instance.armyManager.RemoveSoldier(soldier, playerIndex);
                GameEventManager.Instance.OnOnRemoveSoldier(soldier, playerIndex);

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

        public void ListenOnDeadEvent(UnityAction action)
        {
            OnSoldierDeath += action;
        }

        public void UnListenOnDeadEvent(UnityAction action)
        {
            OnSoldierDeath -= action;
        }
    }
}