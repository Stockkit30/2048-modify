using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OpenPanellDeletruser : MonoBehaviour
{
    public GameObject deletepanel; 
    public GameObject buttoneclosepanel;
    // Start is called before the first frame update
    void Start()
    {
        deletepanel.SetActive(false);
    }

    public void OpenDeletePanel()
    {
        deletepanel.SetActive(true);
    }

    public void CLoseDeletePanel()
    {
        deletepanel.SetActive(false);
    }
}
