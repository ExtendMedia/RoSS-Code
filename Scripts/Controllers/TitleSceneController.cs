using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls title scene
/// </summary>
namespace RoSS
{
    public class TitleSceneController : MonoBehaviour
    {
        [SerializeField] Button _startButton;

        void Start()
        {
            _startButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Lobby));

        }


    }
}