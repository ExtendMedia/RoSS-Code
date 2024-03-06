using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Scriptable Object class for stages
/// </summary>
[CreateAssetMenu(fileName = "New Stage", menuName = "Data/Stages")]
public class StageSO : SerializedScriptableObject
{
    [SerializeField] EnemySO _enemySO;
    [SerializeField] float _stageTime;
    [SerializeField] string _stageName;
    [SerializeField] public int Exp { get; private set; }


    public EnemySO GetEnemySO() => _enemySO;
    public float GetStageTime() => _stageTime;
    public string GetStageName() => _stageName;
}
