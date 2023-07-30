using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainMenu : MonoBehaviour
{
    //public GameObject PlayBtn;
    //public GameObject SettingsBtn;
    //public GameObject ExitBtn;
    [SerializeField ] private List<GameObject> buttonsList = new List<GameObject>();

    public GameObject loadingInterface;
    public Image loadingProgressBar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    public void StartGame()
    {

        //SceneManager.LoadScene(1);


        HideMenu();
        ShowLoadingScreen();

        scenesToLoad.Add(SceneManager.LoadSceneAsync(1));
        //scenesToLoad.Add(SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive));
        StartCoroutine(LoadingScreen());

    }

    public void StartGameSO()
    {
        HideMenu();
        ShowLoadingScreen();
        StartCoroutine(LoadingScreen());
    }

    public void HideMenu()
    {
        //PlayBtn.SetActive(false);
        foreach (GameObject btn in buttonsList)
        {
            btn.SetActive(false);   
        }
    }

    public void ShowLoadingScreen()
    {
        loadingInterface.SetActive(true);
    }
    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for (int i = 0; i < scenesToLoad.Count; ++i)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                loadingProgressBar.fillAmount = totalProgress / scenesToLoad.Count;
                yield return null;
            }
        }
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
