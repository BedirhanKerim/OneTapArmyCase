using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace OneTapArmyCore
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        private void Start()
        {
            GameEventManager.Instance.OnSpawnBullet += SpawnBullet;
        }

        public void SpawnBullet(Vector3 spawnPosition, IDamagable target, float damage,AudioSource audioSource)
        {
            if (target == null) return;

            if (!target.IsAlive())return;
           
            var instance = LeanPool.Spawn(bulletPrefab);
            instance.transform.position = spawnPosition;
            instance.GetComponent<Bullet>().SetBullet(target, damage);
            audioSource.PlayOneShot(audioSource.clip);

        }
    }
}