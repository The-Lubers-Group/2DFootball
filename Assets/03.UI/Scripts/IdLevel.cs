using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdLevel : MonoBehaviour
{
    public int level;
    public static IdLevel instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //SceneManager.sceneLoaded += checkLevel;
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    // Verificar o nível
    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        level = SceneManager.GetActiveScene().buildIndex;
    }
}
