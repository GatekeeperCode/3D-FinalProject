using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    /**
     * _timerText is the UI element to display the timer
     * _timePlayed is the total time played (can change this if we want it to be per level)
     * _elapsedTime is the amount of time that has passed
     * _timerRunning says if it is currently running or not;
     */
    public Text _timerText;
    private TimeSpan _timePlayed;
    public float _elapsedTime;
    private bool _timerRunning;
    // Start is called before the first frame update
    void Start()
    {
        //At the start of the first level, the timer begins running from an elapsed time of 0
        DontDestroyOnLoad(this.gameObject);
        _timerText.text = "Time: 00:00";
        _timerRunning = true;
        _elapsedTime = 0;
        StartCoroutine(UpdateTimer());
    }

    // Update is called once per frame
    void Update()
    {
        //If it isn't the first scene, destroy the timer inside of the current scene (If i recall, there has to be a timer in the scene but we want to
        //replace it with the timer that has the time already.
        if(SceneManager.GetActiveScene().buildIndex > 1)
        {
            Destroy(GameObject.FindGameObjectWithTag("Timer"));
        }
    }
    /**
     * StartTimer() and StopTimer() are public methods that can be called from managers, other objects to start and stop the timer
     */
    public void StartTimer()
    {
        _timerRunning = true;
    }
    public void StopTimer()
    {
        _timerRunning = false;
    }
    /**
     * Update timer updates the timer every second, the TimeSpan object then takes the seconds elapsed and converts it to a string of the form MM:SS
     * (only while timer is running)
     */
    private IEnumerator UpdateTimer()
    {
        while (_timerRunning)
        {
            _elapsedTime += Time.deltaTime;
            _timePlayed = TimeSpan.FromSeconds(_elapsedTime);
            _timerText.text = "Time: " + _timePlayed.ToString("mm':'ss");
            yield return null;
        }
    }
}
