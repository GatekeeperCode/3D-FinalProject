using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2MgrScript : MonoBehaviour
{
    private TimerScript _timer;
    private bool paused;
    public GameObject pauseMenu;
    void Start()
    {
        StartCoroutine(holdLook());
        
        Messenger.AddListener(Messages.LEVEL_TRANSFER, changeLevel);
        _timer = GameObject.FindObjectOfType<TimerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                pause();
            }
            else
            {
                unpause();
            }
        }
    }

    void changeLevel()
    {
        PlayerPrefs.SetFloat("PlayerLevel2Time", _timer._elapsedTime-PlayerPrefs.GetFloat("PlayerLevel1Time"));
        PlayerPrefs.SetFloat("PlayerTotalTime", _timer._elapsedTime);
        Messenger.RemoveListener(Messages.LEVEL_TRANSFER, changeLevel);
        SceneManager.LoadScene("WinScene");
    }

    //Pauses game and opens pause menu
    private void pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        paused = true;
    }

    //Unpauses game and closes pause menu
    private void unpause()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        paused = false;
    }

    IEnumerator holdLook()
    {
        yield return new WaitForSeconds(8);
        Messenger.Broadcast("StartLook");
        _timer.StartTimer();

    }
}
