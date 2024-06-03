using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Main battle manager
/// </summary>
namespace RoSS
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        AsyncOperationHandle<GameObject> _enemySpaceshipHandle;

        [SerializeField] Camera _battleCamera;
        [SerializeField] Transform _player;
        [SerializeField] PlayerPoolManager _playerPool;
        [SerializeField] EnemyPoolManager _enemyPool;
        [SerializeField] Transform _spawner;
        [SerializeField] Spaceship _enemySpaceship;
        [SerializeField] SpaceshipsListSO _spaceshipsListSO;
        [SerializeField] BattleUIManager _battleUIManager;
        [SerializeField] BattlePrepareController _battlePrepareController;
        [SerializeField] Vector3 _defaultEnemyPosition;
        [SerializeField] Quaternion _defaultEnemyRotation;

        [SerializeField] float _defeatMultiplier = 0.25f;


        public BattleState BattleState { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;

                BattleState = BattleState.Prepare;
                _enemySpaceship.SpaceshipSO = GameManager.Instance.StageManager.ActiveStage.GetEnemySO().GetSpaceshipSO();
            }
        }

        private void OnDisable()
        {
            if (_enemySpaceship != null && _enemySpaceship.TryGetComponent<EnemyStatsController>(out var enemyStatsController)) enemyStatsController.OnDie -= Victory;
            if (_player != null && _player.TryGetComponent<PlayerStatsController>(out var playerStatsController)) playerStatsController.OnDie -= Defeat;
            Timer.OnTimeIsUp -= TimeIsUp;
            _battlePrepareController.OnPrepareForBattleEnded -= StartBattle;
            Addressables.Release(_enemySpaceshipHandle);

        }

        void Start()
        {
            LoadEnemySpaceship();
        }


        private void LoadEnemySpaceship()
        {
            if (!_enemySpaceshipHandle.IsValid())
                _enemySpaceship.SpaceshipSO.PrefabARef.LoadAssetAsync<GameObject>().Completed += AfterLoadEnemySpaceship;
            else
                AfterLoadEnemySpaceship(_enemySpaceshipHandle);

        }

        private void AfterLoadEnemySpaceship(AsyncOperationHandle<GameObject> asyncOperationHandle)
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                _enemySpaceshipHandle = asyncOperationHandle;

                InitEnemySpaceship(asyncOperationHandle.Result);

                InitPlayer();

                InitBattleUI();

                PrepareForBattle();

            }
            else
            {
                Debug.LogError("Failed to load spaceship prefab asset reference: " + _enemySpaceship.SpaceshipSO.PrefabARef);
            }

        }

        private void InitEnemySpaceship(GameObject spaceshipPrefab)
        {
            _enemySpaceship.GetComponent<EnemyStatsController>().SetHealth(_enemySpaceship.SpaceshipSO.Health);
            _enemySpaceship.GetComponent<EnemyStatsController>().OnDie += Victory;
            var spaceshipGO = SpawnEnemyShip(spaceshipPrefab).transform;
            _enemySpaceship.SetSpaceshipGO(spaceshipGO);
            _enemySpaceship.SpawnEnemyShipWeapons(spaceshipGO);
        }
        private void InitPlayer()
        {
            _player.GetComponent<PlayerStatsController>().SetHealth(GameManager.Instance.Player.ActiveSpaceship.Health);
            _player.GetComponent<PlayerStatsController>().OnDie += Defeat;
        }
        private void InitBattleUI()
        {
            _battleUIManager.CreatePlayerUI(GameManager.Instance.Player.PlayerSO);
            _battleUIManager.CreateEnemyUI(GameManager.Instance.StageManager.ActiveStage.GetEnemySO());
            _battleUIManager.InitTimer(GameManager.Instance.StageManager.ActiveStage.GetStageTime());
            Timer.OnTimeIsUp += TimeIsUp;
        }

        void PrepareForBattle()
        {
            _battlePrepareController.OnPrepareForBattleEnded += StartBattle;
            _enemySpaceship.transform.SetPositionAndRotation(_defaultEnemyPosition, _defaultEnemyRotation);
            LayerTools.SetLayerRecursively(_enemySpaceship.gameObject, Settings.enemyLayer);
            _battlePrepareController.gameObject.SetActive(true);
            _battlePrepareController.PrepareForBattle();
            _enemySpaceship.GetComponent<SmoothMove>().StartMove();
        }


        void StartBattle()
        {
            BattleState = BattleState.Battle;
            _battleUIManager.StartTimer();
            _battleUIManager.ShowPlayerUI();
            _battleUIManager.ShowEnemyUI();

        }

        void TimeIsUp()
        {
            _enemySpaceship.GetComponent<SmoothMove>().StartMove();
            BattleState = BattleState.Escape;
            _battleUIManager.ShowBattleResultUI(BattleState);

        }

        void Victory()
        {
            BattleState = BattleState.Victory;
            GameManager.Instance.Player.PlayerSO.AddExp(GameManager.Instance.StageManager.ActiveStage.Exp);
            GameManager.Instance.Player.PlayerSO.AddWin(1);
            GameManager.Instance.StageManager.ChangeStage(1);

            _battleUIManager.ShowBattleResultUI(BattleState);
        }
        void Defeat()
        {
            BattleState = BattleState.Defeat;
            GameManager.Instance.Player.PlayerSO.AddExp((int)Math.Round(GameManager.Instance.StageManager.ActiveStage.Exp * _defeatMultiplier));
            _battleUIManager.ShowBattleResultUI(BattleState);
        }
        SpaceshipSO GetRandomSpaceship() => _spaceshipsListSO.SpaceshipsList[UnityEngine.Random.Range(0, _spaceshipsListSO.SpaceshipsList.Count)];


        GameObject SpawnEnemyShip(GameObject spaceshipPrefab)
        {
            //return Instantiate<Transform>(_enemySpaceship.SpaceshipSO.Prefab, _enemySpaceship.transform);
            return Instantiate(spaceshipPrefab, _enemySpaceship.transform);
        }


        public Camera GetBatlleCamera() => _battleCamera;
        public Transform GetTarget() => _player;
        public Transform GetSpawner() => _spawner;
        public PlayerPoolManager GetPlayerPool() => _playerPool;
        public EnemyPoolManager GetEnemyPool() => _enemyPool;
        public Spaceship GetEnemySpaceship() => _enemySpaceship;

    }


    public enum BattleState
    {
        Prepare, Battle, Victory, Defeat, Escape
    }
}