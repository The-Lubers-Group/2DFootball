using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    const string TAG_COIN = "NCoins";
    
    [SerializeField] private TMP_Text pointsUI;
    
    [SerializeField] private GameObject selectBallMenu;
    [SerializeField] private GameObject winGameMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;

    private void Awake()
    {
        pointsUI = GameObject.Find(TAG_COIN).GetComponent<TMP_Text>();
    }

    private void Start()
    {
        LoadSelectBallMenu();
    }

    public void LoadSelectBallMenu()
    {
        selectBallMenu.SetActive(true);
    }

    public void LoadWinGameMenu()
    {
        winGameMenu.SetActive(true);
    }
    public void LoadGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void LoadPauseMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void SceneNextLevel()
    {
        if (GameManager.instance.win)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            //Debug.Log(SceneManager.GetSceneByBuildIndex(nextSceneIndex).name.Contains("Level_"));
            //Debug.Log(SceneManager.GetSceneByBuildIndex(nextSceneIndex).path);
            SceneManager.LoadScene(nextSceneIndex);
            if (SceneManager.GetSceneByBuildIndex(nextSceneIndex).name.Contains("Level_"))
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                SceneManager.LoadScene(1);
            }

        }
    }

    public void SceneRetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        pauseMenu.SetActive(false);
        Time.timeScale = 0;
        LoadSelectBallMenu();
    }


    public void SceneListLevels()
    {
        SceneManager.LoadScene("LevelsMenu");
    }
}
