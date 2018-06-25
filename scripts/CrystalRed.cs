using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRed : MonoBehaviour
{

    public static CrystalRed current;

    void Awake()
    {
        current = this;
    }

    public void crystalAppear()
    {
        UI2DSprite crystalSpite = this.GetComponent<UI2DSprite>();
        crystalSpite.depth = 7;
    }
}
