using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2MgrScript : MonoBehaviour
{
    private TimerScript _timer;
    void Start()
    {
        Messenger.AddListener(Messages.LEVEL_TRANSFER, changeLevel);
        _timer = GameObject.FindObjectOfType<TimerScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void changeLevel()
    {
        PlayerPrefs.SetFloat("Level2Time", _timer._elapsedTime);
        Messenger.RemoveListener(Messages.LEVEL_TRANSFER, changeLevel);
        SceneManager.LoadScene(0);
    }
}
