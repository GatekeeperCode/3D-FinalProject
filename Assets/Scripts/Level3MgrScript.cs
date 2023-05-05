using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3MgrScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(holdLook());
        //_timer = GameObject.FindObjectOfType<TimerScript>();
        Messenger.AddListener(Messages.LEVEL_TRANSFER, changeLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeLevel()
    {
        //PlayerPrefs.SetFloat("PlayerLevel2Time", _timer._elapsedTime - PlayerPrefs.GetFloat("PlayerLevel1Time"));
        //PlayerPrefs.SetFloat("PlayerTotalTime", _timer._elapsedTime);
        Messenger.RemoveListener(Messages.LEVEL_TRANSFER, changeLevel);
        SceneManager.LoadScene("WinScene");
    }

    IEnumerator holdLook()
    {
        yield return new WaitForSeconds(8);
        Messenger.Broadcast("StartLook");
    }
}
