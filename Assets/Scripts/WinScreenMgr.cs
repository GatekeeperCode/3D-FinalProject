using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnMenuButtonClick()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
