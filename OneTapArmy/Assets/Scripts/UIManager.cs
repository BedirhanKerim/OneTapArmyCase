using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _expCountTopSideText,_levelCountText;
  [SerializeField] private Slider _expSlider;
  [SerializeField] private Button ReplayButton;

  private void Start()
  {
      GameEventManager.Instance.OnExperienceChanged += SetUIExpValues;
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

   public void EndGame()
   {
       Time.timeScale = 0;
       ReplayButton.gameObject.SetActive(true);
   }
}
