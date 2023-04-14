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
        if(PlayerPrefs.GetFloat("Level1BestTime") > PlayerPrefs.GetFloat("PlayerLevel1Time"))
        {
            PlayerPrefs.SetFloat("Level1BestTime", PlayerPrefs.GetFloat("PlayerLevel1Time"));
        }
        if (PlayerPrefs.GetFloat("Level2BestTime") > PlayerPrefs.GetFloat("PlayerLevel2Time"))
        {
            PlayerPrefs.SetFloat("Level2BestTime", PlayerPrefs.GetFloat("PlayerLevel2Time"));
        }

        _playerScores.text = "Your Score \n" + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("PlayerLevel1Time")).ToString() + "\n" + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("PlayerLevel2Time")).ToString();
        _highScoreText.text = "HighScores \n" + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level1BestTime")).ToString() + "\n" + TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level2BestTime")).ToString();
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
