using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _speed = 100f;
    private Vector3 _dirMove;
    Rigidbody _playerRbody;
    // Start is called before the first frame update
    void Start()
    {
        _playerRbody = gameObject.GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        _dirMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        _playerRbody.velocity = _dirMove * _speed * Time.fixedDeltaTime;
    }
    //function to jump
    private void Jump()
    {

    }
}
