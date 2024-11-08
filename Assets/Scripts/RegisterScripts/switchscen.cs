using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class switchscen : MonoBehaviour
{
    public void regscen()
    {
        SceneManager.LoadScene("Registration");
    }

    public void autscen()
    {
        SceneManager.LoadScene("Home");
    }
}
