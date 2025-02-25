using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTapArmyCore
{
    public class ArcherAttack : SoldierAttack
    {
        [SerializeField] private Transform bulletSpawnLoc;
        public override void Attack()
        {
            if (!bCanAttack || targetSoldier == null) return;
           // GameManager.Instance.bulletManager.SpawnBullet(bulletSpawnLoc.position, targetSoldier, damage,audioSource);
            GameEventManager.Instance.OnOnSpawnBullet(bulletSpawnLoc.position, targetSoldier, damage,audioSource);
        }
    }
}