using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Back to main lobby scene
/// </summary>
namespace RoSS
{
    public class BackToLobby : MonoBehaviour
    {
        void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Lobby));
            HideInLobby(button);
        }

        void HideInLobby(Button button)
        {
            if (GameManager.Instance.GameState == GameState.Lobby) button.gameObject.SetActive(false);
            else button.gameObject.SetActive(true);
        }
    }
}