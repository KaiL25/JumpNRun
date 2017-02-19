using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] _checkpoints;
    public GameObject _player;
    GameObject _currentCheckpoint;

    // Use this for initialization
    void Start () {
        _currentCheckpoint = _checkpoints[0];
        _player.transform.position = _checkpoints[0].transform.position;
        CheckpointScript.CHECKPOINTACTIVATED += NewCheckpointActivated;
        Projectile.RESPAWNPLAYER += Respawn;
	}
	
	// Update is called once per frame
	void Update () {

        if (_player.transform.position.y < -5)
            Respawn();
	}

    void NewCheckpointActivated(int newIndex)
    {
        _currentCheckpoint = _checkpoints[newIndex];
    }

    void Respawn()
    {
        _player.transform.position = _currentCheckpoint.transform.position;
    }
}
