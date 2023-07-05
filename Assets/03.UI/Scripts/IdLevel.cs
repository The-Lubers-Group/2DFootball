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
    }

    // Verificar o nível
    void checkLevel(Scene scene, LoadSceneMode mode)
    {
        level = SceneManager.GetActiveScene().buildIndex;
    }
}
