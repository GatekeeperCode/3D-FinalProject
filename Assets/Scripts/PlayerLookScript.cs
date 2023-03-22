using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookScript : MonoBehaviour
{
    Transform _transform;
    public Transform _head;
    float verticalAngle = 0;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAngle = _head.eulerAngles.y + 5 * Input.GetAxis("Mouse X");


        verticalAngle -= 2 * Input.GetAxis("Mouse Y");
        verticalAngle = Mathf.Clamp(verticalAngle, -60, 60);
        _transform.rotation = Quaternion.Euler(0, horizontalAngle, 0);
        _head.localRotation = Quaternion.Euler(verticalAngle, 0, 0);
    }
}
