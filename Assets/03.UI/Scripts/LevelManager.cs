using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * LevelManager -- Criar e gerir a tela de sele��o de n�vel
 */

public class LevelManager : MonoBehaviour
{

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
            btnNew.unlockBTN = level.unlock;
            btnNew.GetComponent<Button>().interactable = level.isEnable;

            newBtn.transform.SetParent(localBtn, false);
        }
    }

    void Start()
    {
        ListAdd();
    }
}
