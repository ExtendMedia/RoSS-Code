using System;
using UnityEngine;

/// <summary>
/// Main Game Manager
/// </summary>

namespace RoSS
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameState GameState { get; private set; }
        public Player Player { get; private set; }

        public StageManager StageManager { get; private set; }

        public MusicManager MusicManager { get; private set; }

        public SceneManager SceneManager { get; private set; }

        public bool gameOver = false;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                Player = GetComponentInChildren<Player>();
                SceneManager = GetComponentInChildren<SceneManager>();
                StageManager = GetComponentInChildren<StageManager>();
                MusicManager = GetComponentInChildren<MusicManager>();

            }
        }

        private void Start()
        {
            SceneManager.UnloadAllScenes();
            ChangeState(GameState.Title);
        }
        public void ChangeState(GameState newState)
        {
            var oldState = GameState;
            GameState = newState;
            SceneManager.UnloadScenes(oldState);
        }

        public void LoadNewState()
        {
            switch (GameState)
            {
                case GameState.Title:
                    HandleTitle();
                    break;
                case GameState.Lobby:
                    HandleLobby();
                    break;
                case GameState.Battle:
                    HandleBattle();
                    break;
                default:
                    HandleScene(GameState);
                    break;
            }

        }

        private void HandleBattle()
        {
            SceneManager.LoadScenes(GameState.Battle);
            MusicManager.ChangePlaylist(GameState.Battle);

        }

        private void HandleScene(GameState gameState)
        {
            SceneManager.LoadScenes(gameState);
        }

        private void HandleLobby()
        {
            SceneManager.LoadScenes(GameState.Lobby);
            MusicManager.ChangePlaylist(GameState.Lobby);
        }

        private void HandleTitle()
        {
            SceneManager.LoadScenes(GameState.Title);
        }
    }
    [Serializable]
    public enum GameState
    {
        Start,
        Title,
        Lobby,
        Shipyard,
        Missions,
        Lab,
        Factory,
        Shop,
        Battle,
    }
}