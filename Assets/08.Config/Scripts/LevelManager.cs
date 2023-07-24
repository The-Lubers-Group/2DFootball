using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * LevelManager -- Criar e gerir a tela de seleção de nível
 */

public class LevelManager : MonoBehaviour
{

    //const string UI_MANAGER = "UIManager(Clone)";
    //const string GAME_MANAGER = "GameManager(Clone)";
    const string GAME_LEVEL = "Level_";

    private UIManager uiManager;
    private GameManager gameManager;

    //Classe que contém as informações básicas do objeto level
    [System.Serializable]public class Level
    {
        public int levelID;
        public string levelName;
        public bool isEnable;
        public int unlock;
    }

    public GameObject levelBtn;
    public Transform localBtn;
    public List<Level> levelsList;

    // Criação de uma lista de botões para mostrar as fases disponíveis
    void ListAdd()
    {
        // Percorra a lista de fases
        foreach (Level level in levelsList) 
        {
            GameObject newBtn = Instantiate(levelBtn) as GameObject;

            //Instanciando à classe BtnLevel
            BtnLevel btnNew = newBtn.GetComponent<BtnLevel>();
            //btnNew.levelTextBTN.text = level.levelName;
            btnNew.levelTextBTN.text = level.levelID.ToString();
            
            //if(PlayerPrefs.GetInt(GAME_LEVEL + btnNew.levelTextBTN.text) == 1)

            
            if(PlayerPrefs.GetInt(GAME_LEVEL + level.levelID) == 1)
            {
                level.unlock = 1;
                level.isEnable = true;
            }

            //Debug.Log(GAME_LEVEL + btnNew.levelTextBTN.text + " <--- levelTextBTN.text ");

            //Debug.Log(GAME_LEVEL + level.levelName + " --- isEnable: " + level.isEnable);

            btnNew.unlockBTN = level.unlock;
            btnNew.GetComponent<Button>().interactable = level.isEnable;

            // Quando disparar o evento click no btn - chama o método ClickLevel()
            //btnNew.GetComponent<Button>().onClick.AddListener(() => ClickLevel(GAME_LEVEL + btnNew.levelTextBTN.text));
            btnNew.GetComponent<Button>().onClick.AddListener(() => ClickLevel(GAME_LEVEL + btnNew.levelTextBTN.text));

            newBtn.transform.SetParent(localBtn,false);
 
        }
    }

    // Função responsável por enviar o jogador para o nível selecionado
    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();


    }

    void Start()
    {
        //SceneManager.UnloadSceneAsync(0);
        ListAdd();

        if(uiManager != null)
        {
            Destroy(uiManager.gameObject);
        }
        if(gameManager != null)
        {
            Destroy(gameManager.gameObject);
        }

    }
}
