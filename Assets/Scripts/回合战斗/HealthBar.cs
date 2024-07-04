using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : Bar
{
    public void ShowHealth(int hp, int hpMax)
    {
        AsBar($"{hp}/{hpMax}", (float)hp / (float)hpMax);
    }
}
