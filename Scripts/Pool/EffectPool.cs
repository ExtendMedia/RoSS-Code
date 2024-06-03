using UnityEngine;

/// <summary>
/// Effect's pool
/// </summary>
namespace RoSS
{
    public class EffectPool : BattlePool<Effect>
    {
        public EffectPool(Effect prefab, Transform parent, AudioClip[] audioClips, int maxPoolSize = 100, bool collectionChecks = true)
        {
            if (!prefab) Debug.LogError("Prefab for Pool " + typeof(EffectPool) + " is null");
            _prefab = prefab;
            _parent = parent;
            _audioClips = audioClips;
            _maxPoolSize = maxPoolSize;
            _collectionChecks = collectionChecks;
        }
        public override Effect CreatePooledItem()
        {
            Effect effect = GameObject.Instantiate(_prefab, _parent);
            var audioSoure = effect.gameObject.AddComponent<AudioSource>();
            effect.SetAudioSource(audioSoure);
            effect.SetAudioClip(_audioClips[Random.Range(0, _audioClips.Length)]);
            var returnToPool = effect.gameObject.AddComponent<ReturnEffectToBattlePool>();
            returnToPool.pool = Pool;
            return effect;
        }
    }
}