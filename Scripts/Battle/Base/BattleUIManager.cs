using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI manager in battle scene
/// </summary>
namespace RoSS
{
    public class BattleUIManager : MonoBehaviour
    {
        string _stageName;

        [Header("Victory Panel")]
        [SerializeField] RectTransform _battleVictoryUI;
        [SerializeField] Button _nextBattleButton;
        [SerializeField] Button _backToLobbyButton;
        [SerializeField] ExperienceSlider _expSliderVictory;
        [SerializeField] TMP_Text _stageNameTextVictory;

        [Header("Defeat Panel")]
        [SerializeField] RectTransform _battleDefeatUI;
        [SerializeField] Button _defeatRetryButton;
        [SerializeField] Button _defeatBackToLobbyButton;
        [SerializeField] ExperienceSlider _expSliderDefeat;
        [SerializeField] TMP_Text _stageNameTextDefeat;


        [Header("Characters UI")]
        [SerializeField] CharacterUI _playerUI;
        [SerializeField] CharacterUI _enemyUI;

        [Header("Timer UI")]
        [SerializeField] Timer _timerUI;


        void Start()
        {
            _nextBattleButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Battle));
            _backToLobbyButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Lobby));
            _defeatRetryButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Battle));
            _defeatBackToLobbyButton.onClick.AddListener(() => GameManager.Instance.ChangeState(GameState.Lobby));
            _stageName = GameManager.Instance.StageManager.ActiveStage.GetStageName();
        }


        void ShowVictoryPanel()
        {
            if (GameManager.Instance.gameOver) _nextBattleButton.gameObject.SetActive(false);
            var playerSO = GameManager.Instance.Player.PlayerSO;
            _stageNameTextVictory.text = _stageName;
            _battleVictoryUI.gameObject.SetActive(true);
            _expSliderVictory.StartSliderAnimation(playerSO.Level, playerSO.Experience);
        }

        void ShowDefeatPanel()
        {
            if (GameManager.Instance.gameOver) _nextBattleButton.gameObject.SetActive(false);
            var playerSO = GameManager.Instance.Player.PlayerSO;
            _stageNameTextDefeat.text = _stageName;
            _battleDefeatUI.gameObject.SetActive(true);
            _expSliderDefeat.StartSliderAnimation(playerSO.Level, playerSO.Experience);
        }

        public void ShowBattleResultUI(BattleState state)
        {
            _timerUI.StopTimer();

            switch (state)
            {
                case BattleState.Victory:
                    Invoke("ShowVictoryPanel", 1f);
                    break;
                case BattleState.Defeat:
                    Invoke("ShowDefeatPanel", 1f);
                    break;
                case BattleState.Escape:
                    Invoke("ShowDefeatPanel", 1f);
                    break;
            }
        }

        public void CreatePlayerUI(PlayerSO playerSO)
        {
            _playerUI.SetName(playerSO.Name);
            _playerUI.SetImage(playerSO.Image);
            _playerUI.SetHealth(GameManager.Instance.Player.ActiveSpaceship.Health);
            _playerUI.SetLevel(playerSO.Level);
            _playerUI.SetWins(playerSO.Wins);

            _expSliderVictory.Init(playerSO.Level, playerSO.Experience);
            _expSliderDefeat.Init(playerSO.Level, playerSO.Experience);
        }

        public void CreateEnemyUI(EnemySO enemySO)
        {
            _enemyUI.SetName(enemySO.Name);
            _enemyUI.SetImage(enemySO.Image);
            _enemyUI.SetHealth(enemySO.GetSpaceshipSO().Health);
            _enemyUI.SetLevel(enemySO.GetSpaceshipSO().Level);
        }

        public void InitTimer(float timeValue)
        {
            _timerUI.SetTimer(timeValue);
        }

        public void StartTimer()
        {
            _timerUI.StartTimer();
        }
        public void ShowPlayerUI()
        {
            _playerUI.GetComponent<SmoothMove>().StartMove();
        }
        public void ShowEnemyUI()
        {
            _enemyUI.GetComponent<SmoothMove>().StartMove();
        }
    }
}