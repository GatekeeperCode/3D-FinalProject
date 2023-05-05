using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    float degreesPerSecond = 40;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.transform.position, Vector3.up, degreesPerSecond * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Messenger.Broadcast(Messages.LIGHT_ALERT);
        }
    }
}
