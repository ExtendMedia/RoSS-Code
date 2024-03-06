using UnityEngine;

/// <summary>
/// Projectile's pool
/// </summary>
public class ProjectilePool : BattlePool<Projectile>
{
    public ProjectilePool(Projectile prefab, Transform parent, AudioClip[] audioClips, int maxPoolSize = 100, bool collectionChecks = true)
    {
        if (!prefab) Debug.LogError("Prefab for Pool " + typeof(HitPointsPool) + " is null");
        _prefab = prefab;
        _parent = parent;
        _audioClips = audioClips;
        _maxPoolSize = maxPoolSize;
        _collectionChecks = collectionChecks;
    }
    public override Projectile CreatePooledItem()
    {
        Projectile projectile = GameObject.Instantiate(_prefab, _parent);
        var audioSoure = projectile.gameObject.AddComponent<AudioSource>();
        projectile.SetAudioSource(audioSoure);
        if (_audioClips.Length>0) projectile.SetAudioClip(_audioClips[Random.Range(0, _audioClips.Length)]);
        var returnToPool = projectile.gameObject.AddComponent<ReturnProjectileToBattlePool>();
        returnToPool.pool = Pool;
        return projectile;
    }
}
