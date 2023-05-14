using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3MgrScript : MonoBehaviour
{
    private bool paused;
    private TimerScript _timer;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(holdLook());
        //_timer = GameObject.FindObjectOfType<TimerScript>();
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
                //pause();
            }
            else
            {
               // unpause();
            }
        }
    }

    void changeLevel()
    {
        PlayerPrefs.SetFloat("PlayerLevel3Time", _timer._elapsedTime - PlayerPrefs.GetFloat("PlayerLevel2Time"));
        PlayerPrefs.SetFloat("PlayerTotalTime", _timer._elapsedTime);
        Messenger.RemoveListener(Messages.LEVEL_TRANSFER, changeLevel);
        SceneManager.LoadScene("WinScene");
    }
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
        Messenger.Broadcast("StartMove");
    }
}
