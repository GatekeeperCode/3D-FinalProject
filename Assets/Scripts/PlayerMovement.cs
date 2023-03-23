using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _speed = 500f;
    private float _speedMult = 10f;
    private float _walkSpeed = 4f;
    private float _moveSpeed = 6f;
    private float acceleration = 5f;
    private float _fallMult = 0.5f;


    private float _groundDrag = 4f;


    private float _airMult = 0.1f;
    private float xVelocity;
    private float yVelocity;
    private float zVelocity;
    
    private Vector3 _dirMove;
    Rigidbody _playerRbody;
    
    private float _jumpForce = 5f;
    private bool _isGrounded;
    private bool _isjumping;
    public LayerMask _ground;
    Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        _isGrounded = true;
        _isjumping = false;
        _playerRbody = gameObject.GetComponent<Rigidbody>();
        _playerRbody.useGravity = true;
        trans = gameObject.transform;
        _playerRbody.drag = _groundDrag;
    }

    // Update is called once per frame
    void Update()
    {
       
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
        if(_playerRbody.velocity.y > 2f)
        {
            Debug.Log(_playerRbody.velocity.y);
        }
        //Direction of movement based on player rotation
        xVelocity = Input.GetAxis("Horizontal");
        zVelocity = Input.GetAxis("Vertical");
        _dirMove = trans.forward * zVelocity + trans.right * xVelocity;

        _moveSpeed = Mathf.Lerp(_moveSpeed, _walkSpeed, acceleration * Time.fixedDeltaTime);
        _playerRbody.AddForce(_dirMove.normalized * _moveSpeed * _speedMult, ForceMode.Acceleration);
        
        if (Input.GetKey(KeyCode.Space) && !_isjumping)
        {
            //Jump();
            _playerRbody.velocity = new Vector3(_playerRbody.velocity.x, 0, _playerRbody.velocity.z);
            _isjumping = true;
            _playerRbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            
        }

        if(_playerRbody.velocity.y < 0)
        {
            _playerRbody.velocity -= Vector3.up * _fallMult;
        }

        if (xVelocity == 0 && zVelocity == 0)
        {
            _playerRbody.velocity = new Vector3(0, _playerRbody.velocity.y, 0);
        }


    }
    //function to jump
    private void Jump()
    {
        Debug.Log("Jumping");
        _playerRbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }
}
