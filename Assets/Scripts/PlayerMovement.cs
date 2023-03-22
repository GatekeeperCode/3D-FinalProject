using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _speed = 100f;
    private Vector3 _dirMove;
    Rigidbody _playerRbody;
    private float _jumpForce = 20f;
    // Start is called before the first frame update
    void Start()
    {
        _playerRbody = gameObject.GetComponent<Rigidbody>();
        _playerRbody.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
        _dirMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        
    }

    private void FixedUpdate()
    {
        _playerRbody.velocity = _dirMove * _speed * Time.fixedDeltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Jump();
            _playerRbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
    //function to jump
    private void Jump()
    {
        Debug.Log("Jumping");
        _playerRbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}
