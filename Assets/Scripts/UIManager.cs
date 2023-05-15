using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public bool _isPaused;
    public GameObject _pauseUI;
    public GameObject _mainGameUI;
    public Slider _sensitvitySlider;
    public Text _sensText;
    // Start is called before the first frame update
    void Start()
    {
        _isPaused = false;
        _pauseUI = GameObject.FindGameObjectWithTag("PauseMenu");
        _mainGameUI = GameObject.FindGameObjectWithTag("MainUI");

        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("ISPaused: " + _isPaused);
        _sensText.text = FindObjectOfType<PlayerLookScript>().sensitivity.ToString();
        /*if(_pauseUI == null && _mainGameUI == null)
        {
            _pauseUI = GameObject.FindGameObjectWithTag("PauseMenu");
            _mainGameUI = GameObject.FindGameObjectWithTag("MainUI");
        }*/
        _pauseUI.SetActive(_isPaused);
        _mainGameUI.SetActive(!_isPaused);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                _isPaused = false;
                Time.timeScale = 1.0f;
            }
            else
            {
                _isPaused = true;
                Cursor.lockState = CursorLockMode.None;
                
                Time.timeScale = 0;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    


}
