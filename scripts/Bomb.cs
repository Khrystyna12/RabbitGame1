using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Collectable {

    IEnumerator rabitDies(float duration, HeroRabit rabit)
    {
        Animator animatorRabit = rabit.GetComponent<Animator>();
        //Perform action ...
        animatorRabit.SetTrigger("die");
        //Wait
        yield return new WaitForSeconds(duration);
        //Continue excution in few seconds
        //Other actions...
        LevelController.current.onRabitDeath(rabit);
    }

    protected override void OnRabitHit(HeroRabit rabit)
    {
        if (rabit.isBig) rabit.Small();
        else
        {
            StartCoroutine(rabitDies(0.2f, rabit));
            LevelController.current.onRabitDeath(rabit);
        }

        this.CollectedHide();
    }
}
