using System;
using System.Collections;
using System.Collections.Generic;
using OneTapArmyCore;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Base : MonoBehaviour,IDamagable
{
    private bool isAlive = true;
    [SerializeField]private float _maxHp=1000;
    private float _currentHp,_baseLevel=1;
    [SerializeField]private Image _hpBar;
    [SerializeField]private TextMeshProUGUI _hpText,_baseLevelText;
    [SerializeField]private GameObject castlePrefab3_4,castlePrefab5,lastSpawnedCastle;
    [SerializeField] private int playerIndex;
    [SerializeField] private ParticleSystem onDeathBombParticle;

    public event UnityAction OnBaseDeath;

    private void Start()
    {
        _currentHp = _maxHp;
        _hpText.text = _currentHp.ToString();
        GameEventManager.Instance.OnLevelUpCastle += LevelUpCastle;
    }

    public void LevelUpCastle(float bonusHealt,float level)
    {
        float bonusHp = _maxHp * bonusHealt / 100;
        _maxHp += bonusHp;
        _currentHp += bonusHp;
        _hpText.text = _currentHp.ToString();
        _hpBar.fillAmount = _currentHp / _maxHp;
        level++;
        _baseLevel++;
        _baseLevelText.text = _baseLevel.ToString();
        if (level==3)
        {
Destroy(lastSpawnedCastle);
lastSpawnedCastle=Instantiate(castlePrefab3_4);
        }
        else if (level==5)
        {
            Destroy(lastSpawnedCastle);
            lastSpawnedCastle=Instantiate(castlePrefab5);
        }

    }
    public void TakeDamage(float damage)
    {
        if (!isAlive)
        {
            return;
        }
        _currentHp -= damage;
        _hpText.text = _currentHp.ToString("F0");

        if (_currentHp<=0)
        {
            OnBaseDeath?.Invoke();
            _currentHp = 0;
           // GameManager.Instance.spawnManager.StopSpawn(playerIndex);
            GameEventManager.Instance.OnOnStopSpawn(playerIndex);
            isAlive = false;
           // GameManager.Instance.uIManager.EndGame(playerIndex);
            GameEventManager.Instance.OnOnEndGameCheck(playerIndex);
            transform.position = new Vector3(-1000, -1000, -1000);
            onDeathBombParticle.Play();
            gameObject.SetActive(false);
        }
        _hpBar.fillAmount = _currentHp / _maxHp;
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void ListenOnDeadEvent(UnityAction action)
    {
        OnBaseDeath += action;
    }
    public void UnListenOnDeadEvent(UnityAction action)
    {
        OnBaseDeath -= action;
    }
}
