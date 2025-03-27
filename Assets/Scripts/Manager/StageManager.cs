using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum StageType
{
    Stage01,
    Stage02,
    Stage03,
    Stage04,
    Stage05,
    Stage06
}

public class StageManager : MonoBehaviour
{
    public StageType curStageType = StageType.Stage01;
    public StageData curStageData;
    public StageData[] stages;

    public List<GameObject> spawnedMonsters = new List<GameObject>();    

    private static StageManager _instance;
    public static StageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("StageManager").AddComponent<StageManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Update()
    {
        SetCurStageData();                
    }

    public void SetCurStageData()
    {
        if (curStageType == StageType.Stage01) curStageData = stages[0];
        else if (curStageType == StageType.Stage02) curStageData = stages[1];
        else if (curStageType == StageType.Stage03) curStageData = stages[2];
        else if (curStageType == StageType.Stage04) curStageData = stages[3];
        else if (curStageType == StageType.Stage05) curStageData = stages[4];
        else if (curStageType == StageType.Stage06) curStageData = stages[5];
    }

    public void SpawnMonsters()
    {
        for (int i = 0; i < curStageData.spawnList.Length; i++)
        {
            GameObject monster = Instantiate(curStageData.spawnList[i], Vector3.forward * 30 ,Quaternion.identity);            
            
            spawnedMonsters.Add(monster);
        }
    }   

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene == SceneManager.GetSceneByBuildIndex(1)) SpawnMonsters();    
    }  
}
