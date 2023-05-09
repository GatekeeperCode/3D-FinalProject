using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookScript : MonoBehaviour
{
    Transform _transform;
    public Transform _head;
    float verticalAngle = 0;
    public float dirMovement;
    private bool canLook = false;
    private bool _lookLock = true;
    private UIManager _levelUI;
    [Range(1f, 5f)]
    public float sensitivity = 3f;



    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _levelUI = GameObject.FindObjectOfType<UIManager>();

    }
    
    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_levelUI._isPaused && !_lookLock)
            {
                canLook = true;
            }
            else
            {
                canLook = false;
            }
        }
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
    public void AdjustSensitrivity(float sens)
    {
        sensitivity = sens;
    }

    private void OnGUI()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }
    void canLookNow()
    {
        Messenger.RemoveListener("StartLook", canLookNow);
        canLook = true;
        _lookLock = false;
    }
}
