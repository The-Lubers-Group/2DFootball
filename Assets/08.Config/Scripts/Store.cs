using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Store : MonoBehaviour
{
    // Id do menu da loja
    const int ID_STORE = 2;
    public void StoreGo()
    {
        SceneManager.LoadScene(ID_STORE);
    }
}
