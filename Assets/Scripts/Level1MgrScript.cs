using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1MgrScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Messenger.AddListener(Messages.LEVEL_TRANSFER, changeLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeLevel()
    {
        Messenger.RemoveListener(Messages.LEVEL_TRANSFER, changeLevel);
        SceneManager.LoadScene(2);
    }
}
