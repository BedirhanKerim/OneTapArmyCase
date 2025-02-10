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

        public Transform spawnPoint, enemyspawnPoint; // Spawnlanacak nokta
        [SerializeField] private Image spawnProgressBarPlayer, spawnProgressBarEnemy;
        [SerializeField] private float spawnRateTimePlayer, spawnRateTimeEnemy;
        private float spawnTimerPlayer = 0f;
        private float spawnTimerEnemy = 0f;
        private int spawnQueueCounterPlayer = 0;
        private int spawnQueueCounterEnemy = 0;
        private int botSpawnUnitCounter = 1;

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
            InvokeRepeating(nameof(BotSpawnCounterAdd), 20f, 20f);
            //InvokeRepeating(nameof(SpawnEnemySoldier), 2f, 2f);
        }

        private void Update()
        {
            spawnTimerPlayer += Time.deltaTime;
            spawnTimerEnemy += Time.deltaTime;
            spawnProgressBarPlayer.fillAmount = spawnTimerPlayer / spawnRateTimePlayer;
            spawnProgressBarEnemy.fillAmount = spawnTimerEnemy / spawnRateTimeEnemy;

            if (spawnTimerPlayer >= spawnRateTimePlayer)
            {
                spawnTimerPlayer = 0;
                SpawnSoldier();
            }

            if (spawnTimerEnemy >= spawnRateTimeEnemy)
            {
                spawnTimerEnemy = 0;
                SpawnEnemySoldier();
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
                GameManager.Instance.armyManager.AddSoldier(soldier, false);
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
                var newSoldier = LeanPool.Spawn(_spawnPrefabsEnemy[spawnQueueCounterPlayer].GetSoldierPrefab());
                newSoldier.transform.position = enemyspawnPoint.position;
                var soldier = newSoldier.GetComponent<Soldier>();
                GameManager.Instance.armyManager.AddSoldier(soldier, true);
            }
            else
            {
                SpawnSoldier();
            }
        }

        public void LevelUp(float bonusSpawnRateValue)
        {
            spawnRateTimePlayer -= spawnRateTimePlayer * bonusSpawnRateValue / 100;
        }
    }
}