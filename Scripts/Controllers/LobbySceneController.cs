using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the lobby scene
/// </summary>
public class LobbySceneController : MonoBehaviour
{
    [SerializeField] Button _startBattleButton;
    [SerializeField] Button _shipyardButton;
    [SerializeField] Button _missionsButton;
    [SerializeField] Button _labButton;
    [SerializeField] Button _factoryButton;
    [SerializeField] Button _shopButton;

    [Header("User Info")]
    [SerializeField] TMP_Text playerName;
    [SerializeField] TMP_Text playerExpText;
    [SerializeField] Slider playerExpSlider;
    [SerializeField] TMP_Text playerLevel;
    [SerializeField] TMP_Text playerWins;
    [SerializeField] Color expColor;

    void Start()
    {
        _startBattleButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Battle));
        _shipyardButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Shipyard));
        _missionsButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Missions));
        _labButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Lab));
        _factoryButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Factory));
        _shopButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Shop));
        SetPlayerVisuals();
    }

    void SetPlayerVisuals()
    {
        var playerSO = GameManager.Instance.Player.PlayerSO;
        var expRange = playerSO.PlayerLevels.GetLevelExpRange(playerSO.Level);

        playerName.text = playerSO.Name;
        playerLevel.text = playerSO.Level.ToString();
        playerWins.text = playerSO.Wins.ToString();
        playerExpText.text = playerSO.Experience.ToString() + "<#" + ColorUtility.ToHtmlStringRGB(expColor) + "> / " + playerSO.PlayerLevels.GetNextLevelExp(playerSO.Level).ToString();

        playerExpSlider.value = (playerSO.Experience - expRange.min) / (float)(expRange.max - expRange.min);
    }
}
