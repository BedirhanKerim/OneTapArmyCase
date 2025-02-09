using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInGameUpgradeUnit", menuName = "InGameUpgrade")]
public class UpgradeUnit : ScriptableObject
{
    public UpgradeInfoSo[] upgradeValues = new UpgradeInfoSo[5];

    [Serializable]
    public class UpgradeInfoSo
    {
        public Sprite upgradeImage;
        public float[] values = new float[3]; // 3 değer
        public string[] valueTexts = new string[3]; // 3 açıklama

    }
}