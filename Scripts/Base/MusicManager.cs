using UnityEngine;

/// <summary>
/// Music manager
/// </summary>

namespace RoSS
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] AudioClip[] _lobbyMusic;
        [SerializeField] AudioClip[] _battleMusic;
        [SerializeField] AudioSource _audioSource;

        AudioClip[] _activePlaylist;
        int _lastSongIndex;
        void Start()
        {
            _activePlaylist = _lobbyMusic;
            if (_activePlaylist.Length == 0)
            {
                Debug.LogError("No more songs in active playlist");
                return;
            }
            PlayNewSong();
        }

        void Update()
        {
            if (_audioSource.isPlaying) return;
            PlayNewSong();
        }

        void PlayNewSong()
        {
            _lastSongIndex = RandomizeSong(_activePlaylist.Length);
            _audioSource.clip = _activePlaylist[_lastSongIndex];
            _audioSource.Play();
        }

        int RandomizeSong(int maxIndex)
        {
            if (maxIndex == 1) return 0;
            int newSongIndex = Random.Range(0, maxIndex);
            while (newSongIndex == _lastSongIndex)
                newSongIndex = Random.Range(0, maxIndex);
            return newSongIndex;
        }

        public void ChangePlaylist(GameState gameState)
        {
            AudioClip[] previousPlaylist = _activePlaylist;

            switch (gameState)
            {
                case GameState.Battle:
                    _activePlaylist = _battleMusic;
                    break;
                default:
                    _activePlaylist = _lobbyMusic;
                    break;
            }
            if (_activePlaylist.Length == 0)
            {
                Debug.LogError("No more songs in active playlist");
                return;
            }
            if (previousPlaylist != _activePlaylist) _audioSource.Stop();
        }


    }
}