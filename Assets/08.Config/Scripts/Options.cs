using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void LoadSelectedLevel()
    {
        SceneManager.LoadScene(1);
    }


    public void LoadStore()
    {
        SceneManager.LoadScene(2);
    }
}
