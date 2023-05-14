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
        _levelUI._sensitvitySlider.value = sensitivity;

        Debug.Log("lookLock at start: " + _lookLock);
    }
    
    // Update is called once per frame
    void Update()
    {
        Debug.Log("canlook: " +_levelUI._isPaused);
        Debug.Log("Looklock: " + _lookLock);
        
        if (_lookLock)
        {
            canLook = false;
        }
        else
        {
            canLook = !_levelUI._isPaused;
        }
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_levelUI._isPaused && !_lookLock)
            {
                Debug.Log("Got to true");
                canLook = true;
            }
            else
            {
                Debug.Log("Getting to here");
                canLook = false;
            }
        }*/
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
