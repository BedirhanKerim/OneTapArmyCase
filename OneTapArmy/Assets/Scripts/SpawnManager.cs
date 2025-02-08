using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTapArmyCore
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject soldierPrefab; // Spawnlanacak asker prefabı
        public GameObject soldierEnemyPrefab; // Spawnlanacak asker prefabı

        public Transform spawnPoint,enemyspawnPoint; // Spawnlanacak nokta

        private void Awake()
        {
            InvokeRepeating(nameof(SpawnSoldier), 2f, 2f);
            InvokeRepeating(nameof(SpawnEnemySoldier), 2f, 2f);

        }


        public void SpawnSoldier()
        {
            if (soldierPrefab == null || spawnPoint == null) return;

            // Prefabı belirtilen konumda ve rotasyonla oluştur
            GameObject newSoldier = Instantiate(soldierPrefab, spawnPoint.position, spawnPoint.rotation);
            var soldier = newSoldier.GetComponent<Soldier>();
            // Opsiyonel: Spawnlanan asker ileriye baksın
            GameManager.Instance.armyManager.AddSoldier(soldier, false);
        }
        public void SpawnEnemySoldier()
        {
            if (soldierPrefab == null || spawnPoint == null) return;

            // Prefabı belirtilen konumda ve rotasyonla oluştur
            GameObject newSoldier = Instantiate(soldierEnemyPrefab, enemyspawnPoint.position, enemyspawnPoint.rotation);
            var soldier = newSoldier.GetComponent<Soldier>();
            GameManager.Instance.armyManager.AddSoldier(soldier, true);
        }
    }
}