using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _expCountTopSideText, _levelCountText, _goldCountText;
    [SerializeField] private Slider _expSlider;
    [SerializeField] private Button ReplayButton;
    private int loseBaseCount = 0;

    private void Start()
    {
        GameEventManager.Instance.OnExperienceChanged += SetUIExpValues;
        GameEventManager.Instance.OnSetUILevelValue += SetUILevelValue;
        GameEventManager.Instance.OnEndGameCheck += EndGame;
        GameEventManager.Instance.OnSetGoldText += SetGoldText;
    }

    public void SetUIExpValues(float expCount, float requirementExp)
    {
        _expSlider.value = expCount / requirementExp;
        _expCountTopSideText.text = expCount + "/" + requirementExp;
    }

    public void SetUILevelValue(float levelCount)
    {
        _levelCountText.text = levelCount.ToString();
    }

    public void EndGame(int playerIndex)
    {
        if (playerIndex > 0)
        {
            loseBaseCount++;
            if (loseBaseCount == 2)
            {
                DOVirtual.DelayedCall(1f, () => { Time.timeScale = 0; });
                ReplayButton.gameObject.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 0;
            ReplayButton.gameObject.SetActive(true);
        }
    }

    public void SetGoldText(int goldCount)
    {
        _goldCountText.text = goldCount.ToString();
    }
}