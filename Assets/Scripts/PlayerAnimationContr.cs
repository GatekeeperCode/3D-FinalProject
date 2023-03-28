using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationContr : MonoBehaviour
{
    // Start is called before the first frame update
    Animator _playerAnim;
    PlayerMovement _playerMoveScript;
    Rigidbody _rb;
    void Start()
    {
        _playerAnim = gameObject.GetComponent<Animator>();
        _playerMoveScript = gameObject.GetComponent<PlayerMovement>();
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerAnim.SetFloat("xVelocity", _rb.velocity.x);
        _playerAnim.SetFloat("zVelocity", _rb.velocity.z);
        _playerAnim.SetBool("isJumping", _playerMoveScript._isjumping);
    }
}
