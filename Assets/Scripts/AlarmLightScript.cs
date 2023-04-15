using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLightScript : MonoBehaviour
{
    Rigidbody _rbody;
    private TimerScript _timer;
    public int rotateAmount;
    public int rotateIncrease;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody>();
        _timer = FindObjectOfType<TimerScript>();
        rotateAmount = 0;
        rotateIncrease = 1;
    }

    // Update is called once per frame
    void Update()
    {
        _rbody.transform.rotation = Quaternion.Euler(0, rotateAmount, 0);
        rotateAmount += rotateIncrease;

        if (_timer._elapsedTime < 10)
        {
            rotateIncrease = 2;
        }
        else if (_timer._elapsedTime < 20)
        {
            rotateIncrease = 4;
        }
        else if (_timer._elapsedTime < 30)
        {
            rotateIncrease = 6;
        }
        else
        {
            rotateIncrease = 8;
        }
    }
}
