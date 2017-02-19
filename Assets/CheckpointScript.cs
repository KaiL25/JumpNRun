using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour {

    public static event System.Action<int> CHECKPOINTACTIVATED;

    [SerializeField]
    int index;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CHECKPOINTACTIVATED(index);
        }
    }
}
