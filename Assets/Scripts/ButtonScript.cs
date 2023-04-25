using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClick()
    {
        SceneManager.LoadScene("Level 1 - Chase");
    }

    public void MenuClick()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void ControlsClick()
    {
        panel.SetActive(true);
    }

    public void BackClick()
    {
        panel.SetActive(false);
    }

    public void QuitClick()
    {
        Application.Quit();
    }
}
