using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable
{

    public string color;

    protected override void OnRabitHit(HeroRabit rabit)
    {
        LevelController.current.addCrystals(1, color);
        this.CollectedHide();
    }
}