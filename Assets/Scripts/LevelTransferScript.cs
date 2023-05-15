using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransferScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            print("Found Player");
            Messenger.Broadcast(Messages.LEVEL_TRANSFER);
        }
    }
}
