using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public static event System.Action RESPAWNPLAYER;
    public static event System.Action <int> PROJECTILECOLLISION;

    private int index;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            RESPAWNPLAYER();
        }
        PROJECTILECOLLISION(index);
    }

    public void SetProjectileIndex(int newIndex)
    {
        index = newIndex;
    }


}
