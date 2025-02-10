using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static OneTapArmyCore.Enums;

namespace OneTapArmyCore
{
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] public InGameUpgrade[] inGameUpgrades;
        [SerializeField] public InGameUpgradeUnit[] inGameUpgradeUnits;
        [SerializeField] private GameObject upgradePanel;
        [SerializeField] private Sprite emptyStarSprite,fillStarSprite;
        private bool _firstPanel = false,_isPanelOpen=false;
        [Serializable]
        public class InGameUpgrade
        {
            public EUpgradeType inGameUpgradeType;
            public int level;
            public UpgradeUnit scriptableObj;
            public Sprite upgradeBackgroundImage;
            public int maxLevelCount;
            public string upgradeName;
            public bool bIsTaken = false;
        }

        [Serializable]
        public class InGameUpgradeUnit
        {
            public Button upgradeButton;
            public TextMeshProUGUI  upgradeName;
            public TextMeshProUGUI[] bonusTexts = new TextMeshProUGUI[5];
            public Image upgradeImage, upgradeBackgroundImage;
            public Image[] stars = new Image[5];
        }

        private void Start()
        {
            OpenUpgradePanel();
        }

        public void OpenUpgradePanel()
        {
            if (_isPanelOpen)
            {
                return;
            }
            Time.timeScale = 0f; // Zamanı durdur

            _isPanelOpen = true;
            InGameUpgrade[] selectedBuffs = GetRandomBuffs();
            upgradePanel.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                SetBuffCards(inGameUpgradeUnits[i], selectedBuffs[i]);
            }
        }

        public void CloseUpgradePanel()
        {
        }

        private void SetBuffCards(InGameUpgradeUnit upgradeUnit, InGameUpgrade inGameUpgrade)
        {
            Button button = upgradeUnit.upgradeButton;
            int currentLevel = inGameUpgrade.level;
            for (int i = 0; i < 3; i++)
            {
                float bonusValue = inGameUpgrade.scriptableObj.upgradeValues[currentLevel].values[i];
                string bonusText = inGameUpgrade.scriptableObj.upgradeValues[currentLevel].valueTexts[i] + " %" + bonusValue.ToString();
                if (bonusValue==0)
                {
                    bonusText = "";
                }
                upgradeUnit.bonusTexts[i].text = bonusText; // Tek bir dizi ile işlem yap
            }
            upgradeUnit.upgradeImage.sprite = inGameUpgrade.scriptableObj.upgradeValues[currentLevel ].upgradeImage;
            upgradeUnit.upgradeBackgroundImage.sprite = inGameUpgrade.upgradeBackgroundImage;
            upgradeUnit.upgradeName.text = inGameUpgrade.upgradeName;
            for (int j = 0; j < currentLevel+1; j++)
            {
                upgradeUnit.stars[j].sprite=fillStarSprite;
            }
            button.onClick.AddListener(() => LevelUpUpgrade(inGameUpgrade));
        }
        private InGameUpgrade[] GetRandomBuffs()
        {
            List<InGameUpgrade> list = new();
            for (int i = 0; i < inGameUpgrades.Length; i++)
            {
                if (inGameUpgrades[i].maxLevelCount != inGameUpgrades[i].level)
                {
                    list.Add(inGameUpgrades[i]);
                    
                }
            }

            if (!_firstPanel)
            {
                _firstPanel = true;
                list.Remove(inGameUpgrades[0]);
            }
            if (list.Count<3)
            {
                list.Add(list[0]);
                list.Add(list[0]);

            }
            var orderedArray = list.OrderBy(n => Guid.NewGuid()).Take(3).ToArray();
            return orderedArray;
        }

        private void LevelUpUpgrade( InGameUpgrade newUpgradeValue)
        {            CleanAllButtons();

            newUpgradeValue.level++;
            GameManager.Instance.spawnManager.TakenSoldier(newUpgradeValue.inGameUpgradeType);
            //inGameUpgrade.
            switch (newUpgradeValue.inGameUpgradeType)
            {
                case EUpgradeType.Castle:
                    GameManager.Instance.playerBase.LevelUpCastle(newUpgradeValue.scriptableObj.upgradeValues[newUpgradeValue.level-1].values[0],newUpgradeValue.level);
                    GameManager.Instance.spawnManager.LevelUp(newUpgradeValue.scriptableObj.upgradeValues[newUpgradeValue.level-1].values[1]);
                    break;
                case EUpgradeType.Warrior:
                    break;
                case EUpgradeType.Horseman:
                    break;
                case EUpgradeType.Archer:
                    break;
                case EUpgradeType.Swordsman:
                    break;
                case EUpgradeType.Giant:
                    break;
            }
            _isPanelOpen = false;
            Time.timeScale = 1f; // Zamanı durdur

        }
        
        
        
        
        public void CleanAllButtons()
        {
            for (int i = 0; i < 3; i++)
            {
                inGameUpgradeUnits[i].upgradeButton.onClick.RemoveAllListeners();
               
            }

            upgradePanel.transform.DOScale(Vector3.zero, .5f)
                .SetEase(Ease.InBack) // Daha doğal bir küçülme efekti
                .OnComplete(() =>
                {
                    upgradePanel.SetActive(false);
                    upgradePanel.transform.localScale = Vector3.one;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            inGameUpgradeUnits[i].stars[j].sprite=emptyStarSprite;
                        }
                    }
                });

        }
           
        

        public float[] GetExtraSoldierBuffs(EUpgradeType buffType)
        {
            float[] buffValues = new float[3];
            for (int i = 0; i < inGameUpgrades.Length; i++)
            {
                if (inGameUpgrades[i].inGameUpgradeType == buffType)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        buffValues[j]=inGameUpgrades[i].scriptableObj.upgradeValues[inGameUpgrades[i].level-1].values[j];

                    }

                    break;

                }
            }

            return buffValues;
        }
    }
    
}