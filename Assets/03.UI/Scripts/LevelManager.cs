using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 * LevelManager -- Criar e gerir a tela de seleção de nível
 */

public class LevelManager : MonoBehaviour
{

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
