using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _maxSpeed = 8f;
    private float _crouchMax = 4f;
    private float originMax;
    private float _speed = 2000f;
    private float _thresh = 0.01f;
    // OLD Movement adjusters 
    //private float _speedMult = 10f;
    //private float _walkSpeed = 4f;
    //private float _moveSpeed = 6f;
    //private float acceleration = 5f;

    //if the player reaches a certain point, this is used to make them fall faster. Jump force is how high the player will jump
    /**
     * _fallMult is the multiplier for when the player is falling back down (reaches the top and starts falling, how fast will they fall)
     * _jumpForce is the upward force applied when the player jumps
     * _slideForce is the force applied when a player begins to slide
     * */
    public float _fallMult = 0.5f;
    private float _jumpForce = 175f;
    private float _slideforce = 500f;
    //drag adjustment (basically how fast the player will stop)
    private float _groundDrag = 1f;

    /**
     * This is the scaling for when the player is standing up or crouching
     * */
    private float _startYScale;
    private float _slideYScale;

    private float _mult = 5f;
    public float _airMult = 0.25f;
    private float xVelocity;
    private float yVelocity;
    private float zVelocity;

    //_moveDir is uses for player direction 
    private Vector3 _dirMove;
    Rigidbody _playerRbody;


    //Check values
    private bool _isSliding;
    private bool _isCrouched;
    private bool _isGrounded;
    public bool _isjumping;
    private bool _readyToJump;

    public LayerMask _ground;
    Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        originMax = _maxSpeed;
        _isGrounded = true;
        _readyToJump = true;
        _isjumping = false;
        _isCrouched = false;
        _playerRbody = gameObject.GetComponent<Rigidbody>();
        _playerRbody.useGravity = true;
        trans = gameObject.transform;
        _playerRbody.drag = _groundDrag;
        _startYScale = trans.localScale.y;
        _slideYScale = _startYScale / 2;

    }

    // Update is called once per frame
    void Update()
    {
        InputDetection();
        //Raycast check to see if the player is on the ground (AT THE MOMENT CHECKING FOR GROUND LAYER)
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, _ground);
        Debug.DrawRay(trans.position, Vector3.down, Color.red);
        //_isCrouched = Input.GetKey(KeyCode.C);
        //Debug.Log(_isCrouched);
        if (_isGrounded)
        {
            _isjumping = false;
        }


        /*if (_isCrouched)
        {
            StartCrouch();
        }
        else
        {
            EndCrouch();
        }*/

    }
    private void InputDetection()
    {
        _isCrouched = Input.GetKey(KeyCode.LeftControl);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            // _isSliding = true;
            StartSlide();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopSlide();
        }

    }
    private void FixedUpdate()
    {
        //Debug.Log("_isSliding: " + _isSliding);
        //Debug.Log("_readyToJump: " + _readyToJump);
        Movement();
        //Direction of movement based on player rotation
        xVelocity = Input.GetAxisRaw("Horizontal");
        zVelocity = Input.GetAxisRaw("Vertical");
        _dirMove = trans.forward * zVelocity + trans.right * xVelocity;

        //Jump Check
        if (Input.GetKey(KeyCode.Space) && !_isjumping)
        {
            // Debug.Log("Jumping");
            Jump();
        }
        //Check to see if the player is falling (negative y velocity) and adds more downward force to make the jump feel better
        if (_playerRbody.velocity.y < 0f)
        {
            _playerRbody.velocity += Vector3.up * _fallMult * Physics.gravity.y * Time.fixedDeltaTime;
        }
        //This is just a check for when the player is standing still so they they don't slide/jitter around
        /* if (xVelocity == 0 && zVelocity == 0)
         {
             _playerRbody.velocity = new Vector3(0, _playerRbody.velocity.y, 0);
         }*/
    }
    //function to jump, adds 
    private void Jump()
    {
        if (!_isjumping && _readyToJump)
        {
            _isjumping = true;
            _readyToJump = false;

            _playerRbody.AddForce(Vector2.up * _jumpForce * 1.2f);
            _playerRbody.AddForce(Vector3.up * _jumpForce * 0.5f);
            Invoke("JumpReady", 1f);
        }
        //_playerRbody.velocity = new Vector3(_playerRbody.velocity.x, 0, _playerRbody.velocity.z);



    }
    private void StartCrouch()
    {
        // _isCrouched = true;
        trans.localScale = new Vector3(trans.localScale.x, _slideYScale, trans.localScale.z);
    }
    private void EndCrouch()
    {
        // _isCrouched = false;
        trans.localScale = new Vector3(trans.localScale.x, _startYScale, trans.localScale.z);
    }
    private void StartSlide()
    {
        //Debug.Log("Magnitutde at Start: " + _playerRbody.velocity.magnitude);

        trans.localScale = new Vector3(trans.localScale.x, _slideYScale, trans.localScale.z);
        if (_playerRbody.velocity.magnitude > 1f)
        {
            if (_isGrounded)
            {
                _isSliding = true;
                _playerRbody.AddForce(trans.forward * _slideforce);
            }

        }


    }
    private void StopSlide()
    {
        _isSliding = false;
        trans.localScale = new Vector3(trans.localScale.x, _startYScale, trans.localScale.z);
    }
    private void Movement()
    {
        _playerRbody.AddForce(Vector3.down * Time.deltaTime * 10f);
        float magnitude = _playerRbody.velocity.magnitude;
        float moveAngle = Mathf.Atan2(_playerRbody.velocity.x, _playerRbody.velocity.z) * Mathf.Rad2Deg;
        float zMag = magnitude * Mathf.Cos(Mathf.DeltaAngle(trans.eulerAngles.y, moveAngle) * Mathf.Deg2Rad);
        float xMag = magnitude * Mathf.Cos((90 - Mathf.DeltaAngle(trans.eulerAngles.y, moveAngle)) * Mathf.Deg2Rad);

        //ymag and xmag are the magnitude of movement on the x and z axis
        if (Mathf.Abs(xMag) > _thresh && Mathf.Abs(xVelocity) < 0.05f || (xMag < -_thresh && xVelocity > 0) || (xMag > _thresh && xVelocity < 0))
        {
            _playerRbody.AddForce(_speed * trans.right * Time.deltaTime * -xMag * 0.175f);
        }
        if (Mathf.Abs(zMag) > _thresh && Mathf.Abs(zVelocity) < 0.05f || (zMag < -_thresh && zVelocity > 0) || (zMag > _thresh && zVelocity < 0))
        {
            _playerRbody.AddForce(_speed * trans.forward * Time.deltaTime * -zMag * 0.175f);
        }

        if (_isSliding)
        {
            _playerRbody.AddForce(_speed * Time.fixedDeltaTime * -_playerRbody.velocity.normalized * 0.01f);
        }

        if (xVelocity > 0 && xMag > _maxSpeed)
        {
            xVelocity = 0;
        }
        if (xVelocity < 0 && xMag < -_maxSpeed)
        {
            xVelocity = 0;
        }
        if (zVelocity > 0 && zMag > _maxSpeed)
        {
            zVelocity = 0;
        }
        if (zVelocity < 0 && zMag < -_maxSpeed)
        {
            zVelocity = 0;
        }



        float multiplier = 1f;
        float multiplier2 = 1f;
        if (_isGrounded && _isSliding && _isCrouched)
        {
            multiplier2 = 0f;
        }
        if (_isCrouched && !_isSliding)
        {
            multiplier2 = 0.5f;
            _maxSpeed = _crouchMax;
        }
        if (_isjumping)
        {
            multiplier = _airMult;
            multiplier2 = _airMult;
        }
        else
        {
            _maxSpeed = originMax;
            multiplier = 1f;
        }
        //Debug.Log("Mult1: " + multiplier);
        Debug.Log("Mult2: " + multiplier2);
        _playerRbody.AddForce(trans.right * xVelocity * _speed * Time.fixedDeltaTime * multiplier);
        _playerRbody.AddForce(trans.forward * zVelocity * _speed * Time.deltaTime * multiplier * multiplier2);
    }
    private void JumpReady()
    {
        _readyToJump = true;
    }

}
