using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "sceneDB", menuName = "Scene Data/Database")]
public class ScenesData : ScriptableObject
{
    public List<Level> levels = new List<Level>();
    public List<Menu> menus = new List<Menu>();
    public int CurrentLevelIndex = 4;

    public void LoadLevelWithIndex(int index)
    {
        if (index <= levels.Count)
        {
            SceneManager.LoadSceneAsync(index.ToString(), LoadSceneMode.Additive);
        }
        else CurrentLevelIndex = 4;
    }

    public void NextLevel()
    {
        CurrentLevelIndex++;
        LoadLevelWithIndex(CurrentLevelIndex);
    }

    public void RestartLevel()
    {
        LoadLevelWithIndex(CurrentLevelIndex);
    }

    public void NewGame()
    {
        LoadLevelWithIndex(4);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Main_Menu].sceneName);
    }
    public void LoadPauseMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Pause_Menu].sceneName);
    }
}
