using System.Collections;
using System.Collections.Generic;
using MoreMountains.NiceVibrations;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    private void Start()
    {
        MMVibrationManager.SetHapticsActive(true);
        GameEventManager.Instance.OnVibrate += Vibrate;
    }

    public void Vibrate(HapticTypes targetHapticType)
    {
        MMVibrationManager.Haptic(targetHapticType);
    }

}
