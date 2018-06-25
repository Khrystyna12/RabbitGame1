using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {

    Vector3 my_pos;
    Vector3 target;

    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }

    IEnumerator rabitDies(float duration)
    {
        //Perform action ...
        Animator animatorRabit = HeroRabit.lastRabit.GetComponent<Animator>();
        animatorRabit.SetTrigger("die");
        //Wait
        yield return new WaitForSeconds(duration);
        //Continue excution in few seconds
        //Other actions...
        LevelController.current.onRabitDeath(HeroRabit.lastRabit);
    }

    void Start()
    {
        StartCoroutine(destroyLater());
    }

    void Update()
    {
        my_pos = this.transform.position;
        target = HeroRabit.lastRabit.transform.position;
        this.transform.position = Vector3.MoveTowards(my_pos, target, 8 * Time.deltaTime);
    }

    protected override void OnRabitHit(HeroRabit rabit)
    {
        StartCoroutine(rabitDies(0.2f));
        this.CollectedHide();
        LevelController.current.onRabitDeath(HeroRabit.lastRabit);
    }
}
