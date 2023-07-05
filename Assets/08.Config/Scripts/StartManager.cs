using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartManager : MonoBehaviour
{
    // Id do menu da loja
    const int ID_MENU = 1;
    const string TAG = "BtnPlay";

    public void PlayGame()
    {
        SceneManager.LoadScene(ID_MENU);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
