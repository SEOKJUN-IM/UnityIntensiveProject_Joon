using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "New StageData")]
public class StageData : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Stage Info")]
    public string stageName;
    public StageType stageType;
    public int stageNum;    
    public GameObject[] spawnList;
    public int gold;

    public StageType StageType { get; set; }
    public int StageNum { get; set; }

    public void OnAfterDeserialize()
    {
        StageType = stageType;
        StageNum = stageNum;
    }

    public void OnBeforeSerialize()
    {

    }
}
