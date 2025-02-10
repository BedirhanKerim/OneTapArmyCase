using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour,IDamagable
{
    private bool isAlive = true;
    [SerializeField]private float _maxHp=1000;
    private float _currentHp,_baseLevel=1;
    [SerializeField]private Image _hpBar;
    [SerializeField]private TextMeshProUGUI _hpText,_baseLevelText;
    [SerializeField]private GameObject castlePrefab3_4,castlePrefab5,lastSpawnedCastle;

    private void Start()
    {
        _currentHp = _maxHp;
        _hpText.text = _currentHp.ToString();
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
        _currentHp -= damage;
        _hpText.text = _currentHp.ToString("F0");

        if (_currentHp<=0)
        {
            GameManager.Instance.uIManager.EndGame();
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
}
