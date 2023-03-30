using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSliding : MonoBehaviour
{
    Transform trans;
    Rigidbody _playerRbody;
    private Vector3 _dirMove;
    //slide scaling 
    private float _startYScale;
    private float _slideYScale;
    private float _slideForce = 100f;
    //_maxSlideTime is how long the slide lasts for
    private float _slideTimer;
    private float _maxSlideTime = 0.5f;
    private bool _isSliding;
    private float xVelocity;
    private float zVelocity;
    private float _speed = 500f;
    // Start is called before the first frame update
    void Start()

    {
        trans = gameObject.transform;
        _startYScale = trans.localScale.y;
        _slideYScale = _startYScale / 2;
        _playerRbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Slide Timer: " + _slideTimer);
        xVelocity = Input.GetAxis("Horizontal");
        zVelocity = Input.GetAxis("Vertical");
        //Sliding checks 
        if (Input.GetKeyDown(KeyCode.LeftControl) && (xVelocity != 0 || zVelocity != 0))
        {
            StartSlide();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && _isSliding)
        {
            StopSlide();
        }
    }
    private void FixedUpdate()
    {
        if (_isSliding)
        {
            SlideMove();
        }
    }
    private void StartSlide()
    {
        _isSliding = true;
        trans.localScale = new Vector3(trans.localScale.x, _slideYScale, trans.localScale.z);
        _playerRbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        _slideTimer = _maxSlideTime;
    }

    private void SlideMove()
    {
        _dirMove = trans.forward * zVelocity + trans.right * xVelocity;
        _playerRbody.AddForce(_dirMove.normalized * _slideForce, ForceMode.Force);
        _slideTimer -= Time.deltaTime;
        if (_slideTimer <= 0)
        {
            StopSlide();
        }
    }
    private void StopSlide()
    {
        _isSliding = false;
        trans.localScale = new Vector3(trans.localScale.x, _startYScale, trans.localScale.z);
    }
    public bool getSlideStat()
    {
        return _isSliding;
    }

    private void CounterSlide()
    {
        _playerRbody.AddForce(_speed * Time.deltaTime * -_playerRbody.velocity.normalized * 0.2f);
    }
}
