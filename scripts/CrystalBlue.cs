using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBlue : MonoBehaviour {

    public static CrystalBlue current;

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
