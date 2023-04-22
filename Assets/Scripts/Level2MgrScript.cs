using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2MgrScript : MonoBehaviour
{
    private TimerScript _timer;
    void Start()
    {
        StartCoroutine(holdLook());

        Messenger.AddListener(Messages.LEVEL_TRANSFER, changeLevel);
        _timer = GameObject.FindObjectOfType<TimerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void changeLevel()
    {
        PlayerPrefs.SetFloat("PlayerLevel2Time", _timer._elapsedTime-PlayerPrefs.GetFloat("PlayerLevel1Time"));
        PlayerPrefs.SetFloat("PlayerTotalTime", _timer._elapsedTime);
        Messenger.RemoveListener(Messages.LEVEL_TRANSFER, changeLevel);
        SceneManager.LoadScene("WinScene");
    }

    IEnumerator holdLook()
    {
        yield return new WaitForSeconds(8);
        Messenger.Broadcast("StartLook");
    }
}
