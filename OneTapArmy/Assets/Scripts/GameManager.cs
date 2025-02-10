using System.Collections;
using System.Collections.Generic;
using OneTapArmyCore;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ArmyManager armyManager;
    public UpgradeManager upgradeManager;
    public SpawnManager spawnManager;
    public BulletManager bulletManager;
    public MovementManager movementManager;
    public UIManager uIManager;
    public Base playerBase;
    private float _expCount=0,_requirementExp=5,_levelCount=1;

    public void AddExp(float gainedExp)
    {
        _expCount += gainedExp;
        if (_expCount>=_requirementExp)
        {
            _expCount -= _requirementExp;
            _levelCount++;
            _requirementExp += 10;
            uIManager.SetUILevelValue(_levelCount);
            upgradeManager.OpenUpgradePanel();

        }
        uIManager.SetUIExpValues(_expCount,_requirementExp);
        GameEventManager.Instance.OnOnExperienceChanged(_expCount, _requirementExp);
        
    }
}
