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

    const string UI_MANAGER = "UIManager(Clone)";
    const string GAME_MANAGER = "GameManager(Clone)";

    //Classe que contém as informações básicas do objeto level
    [System.Serializable]public class Level
    {
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
            btnNew.levelTextBTN.text = level.levelName;

            if(PlayerPrefs.GetInt("Level"+ btnNew.levelTextBTN.text) == 1)
            {
                level.unlock = 1;
                level.isEnable = true;
            }

            btnNew.unlockBTN = level.unlock;
            btnNew.GetComponent<Button>().interactable = level.isEnable;

            // Quando disparar o evento click no btn - chama o método ClickLevel()
            btnNew.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level"+btnNew.levelTextBTN.text));

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
        Destroy(GameObject.Find(UI_MANAGER));
        Destroy(GameObject.Find(GAME_MANAGER));
    }

    void Start()
    {
        ListAdd();
    }
}
