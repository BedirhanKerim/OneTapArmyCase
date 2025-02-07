using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTapArmyCore
{
    public class SpawnManager : MonoBehaviour
    {
        public GameObject soldierPrefab; // Spawnlanacak asker prefabı
        public Transform spawnPoint; // Spawnlanacak nokta

        private void Awake()
        {
            InvokeRepeating(nameof(SpawnSoldier), 2f, 2f);
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
    }
}