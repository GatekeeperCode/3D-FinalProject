using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Level1MgrScript _managerScript;

    public AudioSource _playerAudio;
    public AudioClip _landingSound;
    public AudioClip _slideSound;
    public bool isDead;
    private bool _crouchReady;
    private float _maxSpeed;
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
    public GameObject _legs;
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
    public bool _isCrouched;
    private bool _isGrounded;
    private RaycastHit ray;
    public bool _isjumping;
    private bool _readyToJump;
    private bool _onLadder;
    private bool _hitGroundSound;
    public LayerMask _ground;
    Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        _managerScript = FindObjectOfType<Level1MgrScript>();
        isDead = false;
        _maxSpeed = 8f;
        originMax = _maxSpeed;
        _isGrounded = true;
        _readyToJump = true;
        _crouchReady = true;
        _isjumping = false;
        _isCrouched = false;
        _onLadder = false;
        _hitGroundSound = true;
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
        if (_isCrouched)
        {
            _maxSpeed = 4f;
        }
        else
        {
            _maxSpeed = 8f;
        }
        
        if (!isDead)
        {
            InputDetection();
            //Raycast check to see if the player is on the ground (AT THE MOMENT CHECKING FOR GROUND LAYER)
            _isGrounded = Physics.SphereCast(trans.position, 0.52f, -trans.up, out ray, 0.52f, _ground);
           
            if (_isGrounded)
            {
                _isjumping = false;
               
            }

            _legs.SetActive(_isSliding);
           
        }
    }
    private void InputDetection()
    {
        _isCrouched = Input.GetKey(KeyCode.C);
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCrouch();
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            EndCrouch();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _isSliding = true;
            StartSlide();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            StopSlide();
        }

    }
    private void FixedUpdate()
    {
        if (!isDead)
        {
           
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
            if (_playerRbody.velocity.y < 0f && !_onLadder)
            {
                _playerRbody.velocity += Vector3.up * _fallMult * Physics.gravity.y * Time.fixedDeltaTime;
            }
            

            //This is for ladder Climbing - Mike
            if (Input.GetKey(KeyCode.LeftShift) && _onLadder)
            {
                _playerRbody.velocity = new Vector3(xVelocity, 2, zVelocity);
            }
        }
    }
    //function to jump, adds 
    private void Jump()
    {
        if (!_isjumping && _readyToJump && !_onLadder)
        {
            _isjumping = true;
            _readyToJump = false;
            _hitGroundSound = true;
            _playerRbody.AddForce(Vector2.up * _jumpForce * 1.2f);
            _playerRbody.AddForce(Vector3.up * _jumpForce * 0.5f);
            Invoke("JumpReady", 0.5f);
        }
       



    }
    private void StartCrouch()
    {
        // _isCrouched = true;
        trans.localScale = new Vector3(trans.localScale.x, _slideYScale, trans.localScale.z);
        if (_isGrounded)
        {
            _playerRbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
       // _playerRbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }
    private void EndCrouch()
    {
        // _isCrouched = false;
        trans.localScale = new Vector3(trans.localScale.x, _startYScale, trans.localScale.z);
    }
    private void StartSlide()
    {
        //Debug.Log("Magnitutde at Start: " + _playerRbody.velocity.magnitude);
        if (_crouchReady)
        {
            _playerRbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            trans.localScale = new Vector3(trans.localScale.x, _slideYScale, trans.localScale.z);
            
            //trans.position = new Vector3(trans.position.x, trans.position.y - 0.5f, trans.position.z);
            if (_playerRbody.velocity.magnitude > 1f)
            {
                if (_isGrounded)
                {
                    _playerAudio.PlayOneShot(_slideSound);
                    _crouchReady = false;
                    _isSliding = true;
                    _playerRbody.AddForce(trans.forward * _slideforce);
                }

            }
        }
        


    }
    private void StopSlide()
    {
        
        _isSliding = false;
        trans.localScale = new Vector3(trans.localScale.x, _startYScale, trans.localScale.z);
        trans.position = new Vector3(trans.position.x, trans.position.y + 0.5f, trans.position.z);
        Invoke("CrouchReady", 1f);
    }
    private void Movement()
    {
        Debug.Log(_maxSpeed);
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
            return;
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
        if (_isGrounded && _isSliding)
        {
            multiplier2 = 0f;
        }
        if (_isCrouched)
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

    private void CrouchReady()
    {
        Debug.Log("CrouchReady");
        _crouchReady = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            isDead = true;
            _managerScript.playerDeath();
        }
        else if(other.tag.Equals("Ladder"))
        {
            _playerRbody.useGravity = false;
            _onLadder = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Ladder"))
        {
            _playerRbody.useGravity = true;
            _onLadder = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("PLAYING SOUND");
        if(collision.gameObject.tag == "Untagged")
        { 
            //_hitGroundSound = true;
            if(!_playerAudio.isPlaying && _hitGroundSound)
            {
                _playerAudio.PlayOneShot(_landingSound);
                _hitGroundSound = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gameObject.transform.position-gameObject.transform.up*0.52f, 0.52f);
    }

}
