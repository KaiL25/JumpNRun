using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    float _speed;
    float _jumpForce;
    public int _itemCount;
    bool _jumping;

	// Use this for initialization
	void Start () {
        _itemCount = 0;
        _jumping = false;
        _speed = 10f;
        _jumpForce = 250f;
	}

    // Update is called once per frame
    void Update() {

        Debug.Log(_itemCount);
        float hMovement = Input.GetAxis("Horizontal") * _speed;
        float vMovement = Input.GetAxis("Vertical") * _speed;

        hMovement *= Time.deltaTime;
        vMovement *= Time.deltaTime;

        transform.Translate(hMovement, 0, 0);
        transform.Translate(0, 0, vMovement);

        if (Input.GetKeyDown(KeyCode.Space) && _jumping == false)
        {
            Jump();
        } 
            
    }

    void Jump()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * _jumpForce);
        _jumping = true;
        Invoke("JumpReset", 1f);
    }

    void JumpReset()
    {
        _jumping = false;
    }
}
