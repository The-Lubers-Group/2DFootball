using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IdLevel : MonoBehaviour
{
    const int ID_START_LEVEL    = 0;
    const int ID_MENU_LEVEL     = 1;
    const int ID_STORE_LEVEL    = 2;
    
    public int level = -1;

    [SerializeField] private GameObject uIManagerGO;
    [SerializeField] private GameObject gameManagerGO;

    public static IdLevel instance;

    //private float orthoSize = 5;
    //[SerializeField] private float aspect = 1.66f;

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
        /*
         * 
        string levelName = SceneManager.GetActiveScene().name;
        if (levelName.Contains("Level_"))
        {
            return;
        }
        string levelID = levelName.Split('_')[1];
       
        level = Convert.ToInt32(levelID);
        
        Instantiate(uIManagerGO);
        Instantiate(gameManagerGO);
        */
        level = SceneManager.GetActiveScene().buildIndex;
        if (level != ID_START_LEVEL && level != ID_MENU_LEVEL && level != ID_STORE_LEVEL)
        {
            Instantiate(uIManagerGO);
            Instantiate(gameManagerGO);
        }



        //Ajuste da camera e dos objetos
        //Camera.main.projectionMatrix = Matrix4x4.Ortho(-orthoSize * aspect, orthoSize * aspect,-orthoSize, orthoSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);    

    }
}
