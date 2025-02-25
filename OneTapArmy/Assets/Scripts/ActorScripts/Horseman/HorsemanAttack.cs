using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTapArmyCore
{
    public class HorsemanAttack : SoldierAttack
    {
        public override void Attack()
        {
            if (!bCanAttack || targetSoldier == null) return;
            targetSoldier.TakeDamage(damage);
            audioSource.PlayOneShot(audioSource.clip);
            SpawnParticle();


        }
    }
}
