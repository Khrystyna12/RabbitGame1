using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroOrc_1 : MonoBehaviour
{
    public Vector3 MoveBy;
    Vector3 pointA;
    Vector3 pointB;
    Vector3 rabit_pos;

    Animator animatorOrc;
    Animator animatorRabit;

    public enum Mode
    {
        GoToA,
        GoToB,
        Attack
        //...
    }
    Mode mode = Mode.GoToB;

    IEnumerator orcDies(float duration)
    {
        //Perform action ...
        animatorOrc.SetTrigger("die");
        //Wait
        yield return new WaitForSeconds(duration);
        //Continue excution in few seconds
        //Other actions...
        Destroy(this.gameObject);
    }

    IEnumerator rabitDies(float duration)
    {
        //Perform action ...
        animatorOrc.SetTrigger("attack");
        animatorRabit.SetTrigger("die");
        //Wait
        yield return new WaitForSeconds(duration);
        //Continue excution in few seconds
        //Other actions...
        LevelController.current.onRabitDeath(HeroRabit.lastRabit);
        mode = Mode.GoToA;
    }

    bool isArrived(Vector3 pos, Vector3 target)
    {
        pos.z = 0;
        target.z = 0;
        return Mathf.Abs(pos.x - target.x) < 0.02f;
    }

    float getDirection()
    {
        Vector3 my_pos = this.transform.position;
        if (mode == Mode.GoToA)
        {
            //Direction depending on target
            if (my_pos.x < pointA.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        if (mode == Mode.GoToB)
        {
            //Direction depending on target
            if (my_pos.x < pointB.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        else
        {//if (mode == Mode.Attack)
            {
                //Move towards rabit
                if (my_pos.x < rabit_pos.x)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        this.pointA = this.transform.position;
        this.pointB = this.pointA + MoveBy;
        animatorOrc = GetComponent<Animator>();
        animatorRabit = HeroRabit.lastRabit.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 my_pos = this.transform.position;
        rabit_pos = HeroRabit.lastRabit.transform.position;
        bool RabitIsBig = false;
        if (HeroRabit.lastRabit.transform.localScale.y > 9f) RabitIsBig = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        //переключення режимів патрюлювання
        if (mode == Mode.GoToA)
        {
            if (isArrived(my_pos, pointA))
            {
                mode = Mode.GoToB;
            }
        }
        else if (mode == Mode.GoToB)
        {
            if (isArrived(my_pos, pointB))
            {
                mode = Mode.GoToA;
            }
        }

        //перевірка чи кролик зайшов в зону патрулювання
        if (rabit_pos.x > Mathf.Min(pointA.x, pointB.x)
        && rabit_pos.x < Mathf.Max(pointA.x, pointB.x))
        {
            mode = Mode.Attack;
        }

        //перевірка чи час атакувати кролика
        if (!RabitIsBig && Mathf.Abs(my_pos.x - rabit_pos.x) < 9f||
            RabitIsBig && Mathf.Abs(my_pos.x - rabit_pos.x) < 14f)
        {
            if ((my_pos.y + 11f) < rabit_pos.y)
            {
                StartCoroutine(orcDies(0.4f));
            }
            else
            {
                StartCoroutine(rabitDies(0.2f));
            }
        }

        //рух
        Vector3 target;
        float value = this.getDirection();
        if (mode == Mode.GoToA) target = pointA;
        else if (mode == Mode.GoToB) target = pointB;
        else target = rabit_pos;
        if (value > 0)
        {
            sr.flipX = true;
        }
        else if(value < 0)
        {
            sr.flipX = false;
        }
        this.transform.position = Vector3.MoveTowards(my_pos, target, 8 * Time.deltaTime);
    }
}
