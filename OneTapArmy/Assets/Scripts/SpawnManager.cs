using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static OneTapArmyCore.Enums;

namespace OneTapArmyCore
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject soldierPrefab; // Spawnlanacak asker prefabı
        public GameObject soldierEnemyPrefab; // Spawnlanacak asker prefabı


        [SerializeField] private SpawnInfo[] _spawnPrefabs;
        [SerializeField] private SpawnInfo[] _spawnPrefabsEnemy;
        [SerializeField] private SpawnInfo[] _spawnPrefabsEnemy2;

        public Transform spawnPoint, enemyspawnPoint,enemyspawnPoint2; // Spawnlanacak nokta
        [SerializeField] private Image spawnProgressBarPlayer, spawnProgressBarEnemy,spawnProgressBarEnemy2;
        [SerializeField] private float spawnRateTimePlayer, spawnRateTimeEnemy;
        private float spawnTimerPlayer = 0f;
        private float spawnTimerEnemy = 0f;
        private float spawnTimerEnemy2 = 0f;
        private int spawnQueueCounterPlayer = 0;
        private int spawnQueueCounterEnemy = 0;
        private int spawnQueueCounterEnemy2 = 0;
        private int botSpawnUnitCounter = 1;
        private bool enemyBaseAlive = true, enemyBaseAlive2 = true;

        [Serializable]
        public class SpawnInfo
        {
            [SerializeField] private EUpgradeType _upgradeType;
            [SerializeField] private GameObject _soldierPrefab;
            public bool isTaken = false;

            public EUpgradeType GetSoldierType()
            {
                return _upgradeType;
            }

            public GameObject GetSoldierPrefab()
            {
                return _soldierPrefab;
            }
        }


        private void Start()
        {
            InvokeRepeating(nameof(BotSpawnCounterAdd), 15f, 15f);
            //InvokeRepeating(nameof(SpawnEnemySoldier), 2f, 2f);
            GameEventManager.Instance.OnStopSpawn += StopSpawn;
            GameEventManager.Instance.OnLevelUpBonusSpawnRateValue += LevelUp;
            GameEventManager.Instance.OnTakenSoldier += TakenSoldier;
        }

        private void Update()
        {
            spawnTimerPlayer += Time.deltaTime;
            spawnTimerEnemy += Time.deltaTime;
            spawnProgressBarPlayer.fillAmount = spawnTimerPlayer / spawnRateTimePlayer;
            spawnProgressBarEnemy.fillAmount = spawnTimerEnemy / spawnRateTimeEnemy;
            spawnProgressBarEnemy2.fillAmount = spawnTimerEnemy / spawnRateTimeEnemy;

            if (spawnTimerPlayer >= spawnRateTimePlayer)
            {
                spawnTimerPlayer = 0;
                SpawnSoldier();
            }

            if (spawnTimerEnemy >= spawnRateTimeEnemy)
            {
                spawnTimerEnemy = 0;
                if (enemyBaseAlive)
                {
                    SpawnEnemySoldier();
                }

                if (enemyBaseAlive2)
                {
                    SpawnEnemySoldier2();
                }

            }
        }

        private void BotSpawnCounterAdd()
        {
            if (botSpawnUnitCounter < 5)
            {
                botSpawnUnitCounter++;
            }
            else
            {
                CancelInvoke(nameof(BotSpawnCounterAdd));
            }
        }

        public void TakenSoldier(EUpgradeType takenSoldierType)
        {
            for (int i = 0; i < _spawnPrefabs.Length; i++)
            {
                if (_spawnPrefabs[i].GetSoldierType() == takenSoldierType)
                {
                    _spawnPrefabs[i].isTaken = true;
                    break;
                }
            }
        }

        public void SpawnSoldier()
        {
            spawnQueueCounterPlayer++;
            spawnQueueCounterPlayer %= 5;
            if (_spawnPrefabs[spawnQueueCounterPlayer].isTaken)
            {
                var newSoldier = LeanPool.Spawn(_spawnPrefabs[spawnQueueCounterPlayer].GetSoldierPrefab());
                newSoldier.transform.position = spawnPoint.position;
                var soldier = newSoldier.GetComponent<Soldier>();
                //GameManager.Instance.armyManager.AddSoldier(soldier, 0);
                GameEventManager.Instance.OnOnAddSoldier(soldier,0);
            }
            else
            {
                SpawnSoldier();
            }
        }

        public void SpawnEnemySoldier()
        {
            spawnQueueCounterEnemy++;
            spawnQueueCounterEnemy %= botSpawnUnitCounter;
            if (_spawnPrefabsEnemy[spawnQueueCounterEnemy].isTaken)
            {
                var newSoldier = LeanPool.Spawn(_spawnPrefabsEnemy[spawnQueueCounterEnemy].GetSoldierPrefab());
                newSoldier.transform.position = enemyspawnPoint.position;
                var soldier = newSoldier.GetComponent<Soldier>();
               // GameManager.Instance.armyManager.AddSoldier(soldier, 1);
                GameEventManager.Instance.OnOnAddSoldier(soldier,1);

            }
            else
            {
                SpawnEnemySoldier();
            }
        }
        public void SpawnEnemySoldier2()
        {
            spawnQueueCounterEnemy2++;
            spawnQueueCounterEnemy2 %= botSpawnUnitCounter;
            if (_spawnPrefabsEnemy2[spawnQueueCounterEnemy2].isTaken)
            {
                var newSoldier = LeanPool.Spawn(_spawnPrefabsEnemy2[spawnQueueCounterEnemy2].GetSoldierPrefab());
                newSoldier.transform.position = enemyspawnPoint2.position;
                var soldier = newSoldier.GetComponent<Soldier>();
               // GameManager.Instance.armyManager.AddSoldier(soldier, 2);
                GameEventManager.Instance.OnOnAddSoldier(soldier,2);

            }
            else
            {
                SpawnEnemySoldier2();
            }
        }

        public void StopSpawn(int playerIndex)
        {
            if (playerIndex==1)
            {
                enemyBaseAlive = false;
                GameManager.Instance.enemyBase = GameManager.Instance.playerBase;
     

            }
            if (playerIndex==2)
            {
                enemyBaseAlive2 = false;
                GameManager.Instance.enemyBase2 = GameManager.Instance.playerBase;

            }
        }
        public void LevelUp(float bonusSpawnRateValue)
        {
            spawnRateTimePlayer -= spawnRateTimePlayer * bonusSpawnRateValue / 100;
        }
    }
}