using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] private ParticleSystem []damageParticle;

    private void Start()
    {
        GameEventManager.Instance.OnDamageParticleSpawn += SpawnDamageParticle;
    }

    public void SpawnDamageParticle(Vector3 spawnPosition)
    {
        var rnd = Random.Range(0, 4);
        var instance = LeanPool.Spawn(damageParticle[rnd]);
        instance.transform.position =new Vector3(spawnPosition.x,spawnPosition.y+.4f,spawnPosition.z) ;
        instance.gameObject.SetActive(true);
        instance.Play();
        DOVirtual.DelayedCall(1f, () => { 
            LeanPool.Despawn(instance);
            instance.gameObject.SetActive(false);
            
        });
    }
    
}
