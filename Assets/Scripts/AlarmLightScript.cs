using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLightScript : MonoBehaviour
{
    Rigidbody _rbody;
    public int rotateAmount;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody>();
        rotateAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _rbody.transform.rotation = Quaternion.Euler(0, rotateAmount, 0);
        rotateAmount += 2;
    }
}
