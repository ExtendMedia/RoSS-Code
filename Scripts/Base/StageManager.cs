using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stages manager
/// </summary>
namespace RoSS
{
    public class StageManager : MonoBehaviour
    {

        [SerializeField] List<StageSO> _stageList = new List<StageSO>();

        int _stageIndex = 0;


        public StageSO ActiveStage { get; private set; }
        void Start()
        {
            ActiveStage = _stageList[_stageIndex];
        }

        public void ChangeStage(int indexToAdd)
        {
            if (_stageIndex + indexToAdd >= _stageList.Count)
            {
                GameManager.Instance.gameOver = true;
            }
            else
            {
                _stageIndex += indexToAdd;
                ActiveStage = _stageList[_stageIndex];
            }
        }
    }
}