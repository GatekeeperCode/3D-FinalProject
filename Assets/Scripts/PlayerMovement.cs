using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _maxSpeed = 20f;
    private float _thresh = 0.01f;
    //Movement adjusters 
    private float _speedMult = 10f;
    private float _walkSpeed = 4f;
    private float _moveSpeed = 6f;
    private float acceleration = 5f;

    //if the player reaches a certain point, this is used to make them fall faster. Jump force is how high the player will jump
    private float _fallMult = 0.8f;
    private float _jumpForce = 10f;
    //drag adjustment (basically how fast the player will stop)
    private float _groundDrag = 4f;



    private float _mult = 5f;
    private float _airMult = 1f;
    private float xVelocity;
    private float yVelocity;
    private float zVelocity;
    
    //_moveDir is uses for player direction 
    private Vector3 _dirMove;
    Rigidbody _playerRbody;
    
    
    //Check values
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
       //Raycast check to see if the player is on the ground (AT THE MOMENT CHECKING FOR GROUND LAYER)
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, _ground);
        if (_isGrounded)
        {
            _isjumping = false;
        }   
    }

    private void FixedUpdate()
    {
        //Direction of movement based on player rotation
        xVelocity = Input.GetAxis("Horizontal");
        zVelocity = Input.GetAxis("Vertical");
        _dirMove = trans.forward * zVelocity + trans.right * xVelocity;
        

        //Player speed moves up to walk speed (may need to check if this is working)
        if (!gameObject.GetComponent<PlayerSliding>().getSlideStat())
        {
            _moveSpeed = Mathf.Lerp(_moveSpeed, _walkSpeed, acceleration * Time.fixedDeltaTime);
        }


        //Player accelerates through the vector _dirMove multiplied by move speed and the speed multiplier
        //_playerRbody.AddForce(_dirMove.normalized * _moveSpeed * _speedMult, ForceMode.Acceleration);
        if (_isGrounded)
        {
            _playerRbody.AddForce(_dirMove.normalized * _moveSpeed * _speedMult, ForceMode.Force);
        }
        else
        {
            _playerRbody.AddForce(_dirMove.normalized * _moveSpeed * _airMult * _mult, ForceMode.Force);
        }
        
        //Jump Check
        if (Input.GetKey(KeyCode.Space) && !_isjumping)
        {
            Jump();
        }
        //Check to see if the player is falling (negative y velocity) and adds more downward force to make the jump feel better
        if(_playerRbody.velocity.y < 0)
        {
            _playerRbody.velocity -= Vector3.up * _fallMult;
        }
        //This is just a check for when the player is standing still so they they don't slide/jitter around
        if (xVelocity == 0 && zVelocity == 0)
        {
            _playerRbody.velocity = new Vector3(0, _playerRbody.velocity.y, 0);
        }

        
        


    }
    //function to jump, adds 
    private void Jump()
    {
        _playerRbody.velocity = new Vector3(_playerRbody.velocity.x, 0, _playerRbody.velocity.z);
        _isjumping = true;
        _playerRbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
    }

   
}
