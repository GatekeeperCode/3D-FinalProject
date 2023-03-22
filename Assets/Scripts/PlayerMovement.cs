using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _speed = 500f;
    private float xVelocity;
    private float yVelocity;
    private float zVelocity;

    private Vector3 _dirMove;
    Rigidbody _playerRbody;
    private float _jumpForce = 10f;
    private bool _isGrounded;
    private bool _isjumping;
    public LayerMask _ground;
    // Start is called before the first frame update
    void Start()
    {
        _isGrounded = true;
        _isjumping = false;
        _playerRbody = gameObject.GetComponent<Rigidbody>();
        _playerRbody.useGravity = true;
    }

    // Update is called once per frame
    void Update()
    {
       // _dirMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
       // xVelocity = Input.GetAxis("Horizontal") * _speed * Time.deltaTime;
       // zVelocity = Input.GetAxis("Vertical") * _speed * Time.deltaTime;
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, _ground);
        if (_isGrounded)
        {
            _isjumping = false;
        }   
    }

    private void FixedUpdate()
    {
        xVelocity = Input.GetAxis("Horizontal") * _speed * Time.fixedDeltaTime;
        zVelocity = Input.GetAxis("Vertical") * _speed * Time.fixedDeltaTime;
        _playerRbody.velocity = new Vector3(xVelocity, _playerRbody.velocity.y, zVelocity);
        if (Input.GetKey(KeyCode.Space) && !_isjumping)
        {
            //Jump();
            _isjumping = true;
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
