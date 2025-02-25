using System.Collections;
using System.Collections.Generic;
using OneTapArmyCore;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
   // public ParticleSpawner particleSpawner;
   // public CoinSpawner coinSpawner;
   // public VibrationManager vibrationManager;
   // public ArmyManager armyManager;
   // public UpgradeManager upgradeManager;
   // public SpawnManager spawnManager;
   // public BulletManager bulletManager;
   // public MovementManager movementManager;
   // public UIManager uIManager;
   
   
   
   
    public Transform playerBase,enemyBase,enemyBase2;
    private float _expCount=0,_requirementExp=5,_levelCount=1;
    private int _goldCount = 0;
    public void AddExp(float gainedExp)
    {
        _expCount += gainedExp;
        if (_expCount>=_requirementExp)
        {
            _expCount -= _requirementExp;
            _levelCount++;
            _requirementExp += 10;
           // uIManager.SetUILevelValue(_levelCount);
            GameEventManager.Instance.OnOnSetUILevelValue(_levelCount);
           // upgradeManager.OpenUpgradePanel();
            GameEventManager.Instance.OnOnOpenUpgradePanel();

        }
       // uIManager.SetUIExpValues(_expCount,_requirementExp);
        GameEventManager.Instance.OnOnExperienceChanged(_expCount, _requirementExp);
        
    }

    public void AddGold(int goldValue)
    {
         _goldCount+=goldValue;
         GameEventManager.Instance.OnOnSetGoldText(_goldCount);
        // uIManager.SetGoldText(_goldCount);
    }


    
}
