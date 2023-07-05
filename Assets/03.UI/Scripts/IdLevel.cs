using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdLevel : MonoBehaviour
{
    const int ID_MENU_LEVEL = 4;
    const int ID_STORE_LEVEL = 5;
    const int ID_START_LEVEL = 6;
    public int level = -1;

    [SerializeField] private GameObject uIManagerGO;
    [SerializeField] private GameObject gameManagerGO;

    public static IdLevel instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += checkLevel;
    }

    // Verificar o n�vel
    private void checkLevel(Scene arg0, LoadSceneMode modo)
    {
        level = SceneManager.GetActiveScene().buildIndex;

        if(level != ID_MENU_LEVEL && level != ID_STORE_LEVEL && level != ID_START_LEVEL)
        {
            Instantiate(uIManagerGO);
            Instantiate(gameManagerGO);
        }
    }
}
