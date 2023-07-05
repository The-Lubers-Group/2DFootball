using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdLevel : MonoBehaviour
{
    const int ID_MENU_LEVEL = 4;
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

    // Verificar o nível
    private void checkLevel(Scene arg0, LoadSceneMode modo)
    {
        level = SceneManager.GetActiveScene().buildIndex;

        if(level != ID_MENU_LEVEL)
        {
            Instantiate(uIManagerGO);
            Instantiate(gameManagerGO);
        }
    }
}
