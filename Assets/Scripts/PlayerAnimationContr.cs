using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationContr : MonoBehaviour
{
    // Start is called before the first frame update
    Animator _playerAnim;
    PlayerMovement _playerMoveScript;
    Rigidbody _rb;
    Transform trans;
    void Start()
    {
        _playerAnim = gameObject.GetComponent<Animator>();
        _playerMoveScript = gameObject.GetComponent<PlayerMovement>();
        _rb = gameObject.GetComponent<Rigidbody>();
        trans = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float magnitude = _rb.velocity.magnitude;
        float moveAngle = Mathf.Atan2(_rb.velocity.x, _rb.velocity.z) * Mathf.Rad2Deg;
        float zMag = magnitude * Mathf.Cos(Mathf.DeltaAngle(trans.eulerAngles.y, moveAngle) * Mathf.Deg2Rad);
        float xMag = magnitude * Mathf.Cos((90 - Mathf.DeltaAngle(trans.eulerAngles.y, moveAngle)) * Mathf.Deg2Rad);
        Debug.Log(zMag);
        _playerAnim.SetFloat("xVelocity", /*_rb.velocity.x*/ xMag);
        _playerAnim.SetFloat("zVelocity", /*_rb.velocity.z*/ zMag);
        _playerAnim.SetBool("isJumping", _playerMoveScript._isjumping);
    }
}
