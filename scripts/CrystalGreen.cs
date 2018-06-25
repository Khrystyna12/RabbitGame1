using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalGreen : MonoBehaviour
{

    public static CrystalGreen current;

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
