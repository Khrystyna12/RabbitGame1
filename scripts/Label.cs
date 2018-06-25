using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Label : MonoBehaviour {

    public static UILabel coinsLabel = null;

    // Use this for initialization
    void Start () {
        coinsLabel = this.GetComponent<UILabel>();
        coinsLabel.depth = 6;
}
	
	// Update is called once per frame
	void Update () {
        coinsLabel.text = LevelController.current.getCoins();
    }
}
