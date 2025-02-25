using System.Collections;
using System.Collections.Generic;
using OneTapArmyCore;
using UnityEngine;
using UnityEngine.Events;

public interface IDamagable
{
    void TakeDamage(float damage);
    bool IsAlive();
    Transform GetTransform();
    void ListenOnDeadEvent(UnityAction action);
    void UnListenOnDeadEvent(UnityAction action);

}    

