using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class LevelManager : MonoBehaviour
{

    [System.Serializable]public class Level
    {
        public string levelName;
        public bool enable;
        public int unlock;
    }

    public GameObject levelBtn;
    public Transform localBtn;
    public List<Level> levelsList;

    void ListAdd()
    {
        foreach (Level level in levelsList) 
        {
            GameObject newBtn = Instantiate(levelBtn) as GameObject;
            newBtn.transform.SetParent(localBtn, false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ListAdd();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
