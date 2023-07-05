using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * LevelManager -- Criar e gerir a tela de sele��o de n�vel
 */

public class LevelManager : MonoBehaviour
{

    const string UI_MANAGER = "UIManager(Clone)";
    const string GAME_MANAGER = "GameManager(Clone)";

    //Classe que cont�m as informa��es b�sicas do objeto level
    [System.Serializable]public class Level
    {
        public string levelName;
        public bool isEnable;
        public int unlock;
    }

    public GameObject levelBtn;
    public Transform localBtn;
    public List<Level> levelsList;

    // Cria��o de uma lista de bot�es para mostrar as fases dispon�veis
    void ListAdd()
    {
        // Percorra a lista de fases
        foreach (Level level in levelsList) 
        {
            GameObject newBtn = Instantiate(levelBtn) as GameObject;

            //Instanciando � classe BtnLevel
            BtnLevel btnNew = newBtn.GetComponent<BtnLevel>();
            btnNew.levelTextBTN.text = level.levelName;

            if(PlayerPrefs.GetInt("Level"+ btnNew.levelTextBTN.text) == 1)
            {
                level.unlock = 1;
                level.isEnable = true;
            }

            btnNew.unlockBTN = level.unlock;
            btnNew.GetComponent<Button>().interactable = level.isEnable;

            // Quando disparar o evento click no btn - chama o m�todo ClickLevel()
            btnNew.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level"+btnNew.levelTextBTN.text));

            newBtn.transform.SetParent(localBtn,false);
 
        }
    }

    // Fun��o respons�vel por enviar o jogador para o n�vel selecionado
    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    private void Awake()
    {
        Destroy(GameObject.Find(UI_MANAGER));
        Destroy(GameObject.Find(GAME_MANAGER));
    }

    void Start()
    {
        ListAdd();
    }
}
