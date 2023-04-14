using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class WinScreenMgr : MonoBehaviour
{
    // Start is called before the first frame update
    public Text _highScoreText;
    public Text _playerScores;
    public GameObject _highScorePanel;
    private bool _highScoreShowing;
    void Start()
    {
        _highScoreShowing = false;
        if(PlayerPrefs.GetFloat("Level1BestTime") > PlayerPrefs.GetFloat("PlayerLevel1Time") || PlayerPrefs.GetFloat("Level1BestTime") == 0f)
        {
            PlayerPrefs.SetFloat("Level1BestTime", PlayerPrefs.GetFloat("PlayerLevel1Time"));
        }
        if (PlayerPrefs.GetFloat("Level2BestTime") > PlayerPrefs.GetFloat("PlayerLevel2Time") || PlayerPrefs.GetFloat("Level2BestTime") == 0f)
        {
            PlayerPrefs.SetFloat("Level2BestTime", PlayerPrefs.GetFloat("PlayerLevel2Time"));
        }

        _playerScores.text = "Your Score \n" + "Level 1: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("PlayerLevel1Time")).ToString("mm':'ss") + "\n" + "Level 2: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("PlayerLevel2Time")).ToString("mm':'ss");
        _highScoreText.text = "HighScores \n" + "Level 1: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level1BestTime")).ToString("mm':'ss") + "\n" + "Level 2: " + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level2BestTime")).ToString("mm':'ss");
    }
    
    // Update is called once per frame
    void Update()
    {
        _highScorePanel.SetActive(_highScoreShowing);
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
    public void OnHighscoreButtonClick()
    {
        if (_highScoreShowing)
        {
            _highScoreShowing = false;
        }
        else
        {
            _highScoreShowing = true;
        }
    }
}
