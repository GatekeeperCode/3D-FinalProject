using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreenManager : MonoBehaviour
{
    public AudioSource _musicPlayer;
    public AudioSource _effectManager;
    // Start is called before the first frame update
    void Start()
    {
        _musicPlayer.Pause();

    }

    // Update is called once per frame
    void Update()
    {
        if(!_effectManager.isPlaying && !_musicPlayer.isPlaying) {
            _musicPlayer.Play();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

   public void OnHomeButtonCLick()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Level 1 - Chase");
    }
    private void OnGUI()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
