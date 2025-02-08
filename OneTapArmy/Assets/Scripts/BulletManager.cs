using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTapArmyCore
{
    public class BulletManager : MonoBehaviour
    {
       [SerializeField] private GameObject bulletPrefab;
        public void SpawnBullet(Vector3 spawnPosition, IDamagable target, float damage)

        {

          

            if (target == null) return;

            //var instance = PoolingManager.Instance.Spawn(ref bulletPrefabInstance);
            // var instance = Instantiate(bulletPrefabInstance);
         var instance=  Instantiate(bulletPrefab);
            instance.transform.position = spawnPosition;
            instance.GetComponent<Bullet>().SetBullet(target, damage);
        }
    }
}