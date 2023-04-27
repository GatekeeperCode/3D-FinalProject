using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookScript : MonoBehaviour
{
    Transform _transform;
    public Transform _head;
    float verticalAngle = 0;
    public float dirMovement;
    bool canLook = false;

    [Range(1f, 5f)]
    public float sensitivity = 3f;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Messenger.AddListener("StartLook", canLookNow);

        if(canLook)
        {
            float horizontalAngle = _head.eulerAngles.y + sensitivity * Input.GetAxis("Mouse X");
            //Debug.Log(horizontalAngle);
            dirMovement = horizontalAngle;
            verticalAngle -= sensitivity * Input.GetAxis("Mouse Y");
            verticalAngle = Mathf.Clamp(verticalAngle, -60, 60);
            _transform.rotation = Quaternion.Euler(0, horizontalAngle, 0);
            _head.localRotation = Quaternion.Euler(verticalAngle, 0, 0);
        }
    }

    void canLookNow()
    {
        Messenger.RemoveListener("StartLook", canLookNow);
        canLook = true;
    }
}
