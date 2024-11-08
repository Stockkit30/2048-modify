using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startscript : MonoBehaviour
{
    public GameObject infopanel;
    private void Start()
    {
        infopanel.SetActive(false);
    }
    
    public void OpenGameLoad()
    {

        SceneManager.LoadScene("Home");
    }

    public void Exitgame()
    {
        Application.Quit();
    }

    public void openinfoPanel()
    {
        infopanel.SetActive(true);
    }

    public void closeinfoPanel()
    {
        infopanel.SetActive(false);
    }
}
