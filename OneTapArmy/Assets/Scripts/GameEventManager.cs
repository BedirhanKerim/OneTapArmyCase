using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : Singleton<GameEventManager>
{
    public event Action<float, float> OnExperienceChanged;

    public  void OnOnExperienceChanged(float arg1, float arg2)
    {
        OnExperienceChanged?.Invoke(arg1, arg2);
    }
}
