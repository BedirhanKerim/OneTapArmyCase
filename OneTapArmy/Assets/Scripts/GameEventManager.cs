using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using OneTapArmyCore;
using UnityEngine;
using UnityEngine.Events;
using static OneTapArmyCore.Enums;


    public class GameEventManager : Singleton<GameEventManager>
    {
        //UIManager
        public event UnityAction<float> OnSetUILevelValue;
        public event UnityAction<int> OnEndGameCheck;
        public event UnityAction<int> OnSetGoldText;
        public event Action<float, float> OnExperienceChanged;
        //ParticleManager
        public event UnityAction<Vector3> OnDamageParticleSpawn;
        //CoinManager
        public event UnityAction<Transform, Action, int> OnCoinSpawn;
        //VibrationManager
        public event UnityAction<HapticTypes> OnVibrate;
        //ArmyManager
        public event UnityAction<Soldier, int> OnAddSoldier;
        public event UnityAction<Soldier, int> OnRemoveSoldier;
        public event Func<int, List<Soldier>> OnSoldierListRequested;
        //UpgradeManager
        public event UnityAction OnOpenUpgradePanel;
        public event Func<EUpgradeType, float[]> OnExtraSoldierBuffsRequested;
        //SpawnManager
        public event UnityAction<int> OnStopSpawn;
        public event UnityAction<float> OnLevelUpBonusSpawnRateValue;
        public event UnityAction<EUpgradeType> OnTakenSoldier;
        //MovementManager
        public event UnityAction OnMoveEnemyArmy1;
        public event UnityAction OnMoveEnemyArmy2;
        public event UnityAction OnMovePlayerArmy;
        public event UnityAction<Soldier, int> OnSpawnToMoveBaseQueue;
        //BulletManager
        public event UnityAction<Vector3, IDamagable,float,AudioSource> OnSpawnBullet;
        //Base
        public event UnityAction<float, float> OnLevelUpCastle;
        //GameManager

        public void OnOnExperienceChanged(float arg1, float arg2)
        {
            OnExperienceChanged?.Invoke(arg1, arg2);
        }

        //+polish //Done
        //+events


        //+        if (target != null&&targetDamagable.IsAlive())//Done
        //+1v1v1//Done
        //+ targetRotation = (teamType == TeamType.Player)//Done
        //  ? Quaternion.LookRotation(Vector3.forward)//Done
        // : Quaternion.LookRotation(Vector3.back);//Done
        //moduler soldier yapısı switch case yapısı kalkacak.//Done
        // mermi sistemi moduler(zorunlu degil)
        // kart seçiminde polish//Done
        // haptic eklenecek//Done
        //ui revize,pointer ui //Done
        public void OnOnDamageParticleSpawn(Vector3 arg0)
        {
            OnDamageParticleSpawn?.Invoke(arg0);
        }

        public void OnOnCoinSpawn(Transform arg0, Action arg1, int arg2)
        {
            OnCoinSpawn?.Invoke(arg0, arg1, arg2);
        }

        public void OnOnVibrate(HapticTypes arg0)
        {
            OnVibrate?.Invoke(arg0);
        }

        public void OnOnAddSoldier(Soldier arg0, int arg1)
        {
            OnAddSoldier?.Invoke(arg0, arg1);
        }

        public void OnOnRemoveSoldier(Soldier arg0, int arg1)
        {
            OnRemoveSoldier?.Invoke(arg0, arg1);
        }

        public List<Soldier> OnOnSoldierListRequested(int arg)
        {
            return OnSoldierListRequested?.Invoke(arg);
        }

        public void OnOnOpenUpgradePanel()
        {
            OnOpenUpgradePanel?.Invoke();
        }

        public float[] OnOnExtraSoldierBuffsRequested(EUpgradeType arg)
        {
          return  OnExtraSoldierBuffsRequested?.Invoke(arg);
        }

        public void OnOnStopSpawn(int arg0)
        {
            OnStopSpawn?.Invoke(arg0);
        }

        public void OnOnLevelUpBonusSpawnRateValue(float arg0)
        {
            OnLevelUpBonusSpawnRateValue?.Invoke(arg0);
        }

        public void OnOnTakenSoldier(EUpgradeType arg0)
        {
            OnTakenSoldier?.Invoke(arg0);
        }

        public void OnOnSpawnBullet(Vector3 arg0, IDamagable arg1, float arg2, AudioSource arg3)
        {
            OnSpawnBullet?.Invoke(arg0, arg1, arg2, arg3);
        }

        public void OnOnMoveEnemyArmy1()
        {
            OnMoveEnemyArmy1?.Invoke();
        }

        public void OnOnMoveEnemyArmy2()
        {
            OnMoveEnemyArmy2?.Invoke();
        }

        public void OnOnMovePlayerArmy()
        {
            OnMovePlayerArmy?.Invoke();
        }

        public  void OnOnSpawnToMoveBaseQueue(Soldier arg0, int arg1)
        {
            OnSpawnToMoveBaseQueue?.Invoke(arg0, arg1);
        }

        public void OnOnSetUILevelValue(float arg0)
        {
            OnSetUILevelValue?.Invoke(arg0);
        }

        public  void OnOnEndGameCheck(int arg0)
        {
            OnEndGameCheck?.Invoke(arg0);
        }

        public void OnOnSetGoldText(int arg0)
        {
            OnSetGoldText?.Invoke(arg0);
        }

        public  void OnOnLevelUpCastle(float arg0, float arg1)
        {
            OnLevelUpCastle?.Invoke(arg0, arg1);
        }
    }
