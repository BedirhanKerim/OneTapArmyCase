using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace OneTapArmyCore
{
    public class BulletManager : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;

        public void SpawnBullet(Vector3 spawnPosition, IDamagable target, float damage)

        {
            if (target == null) return;


            var instance = LeanPool.Spawn(bulletPrefab);
            instance.transform.position = spawnPosition;
            instance.GetComponent<Bullet>().SetBullet(target, damage);
        }
    }
}