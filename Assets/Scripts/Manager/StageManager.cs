using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] monsters;

    [Header("Stage01")]
    public int count;

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
    }

    private void Update()
    {
        
    }

    
}
