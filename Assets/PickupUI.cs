using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupUI : MonoBehaviour {

    PlayerScript playerScript;
    Text text;
	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

        text.text = "Pickups: " + playerScript._itemCount;
	}
}
