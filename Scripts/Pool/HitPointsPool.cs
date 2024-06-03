using UnityEngine;

/// <summary>
/// Hitpoints pool
/// </summary>
namespace RoSS
{
    public class HitPointsPool : BattlePool<HitPoints>
    {
        public HitPointsPool(HitPoints prefab, Transform parent, int maxPoolSize = 100, bool collectionChecks = true)
        {
            if (!prefab) Debug.LogError("Prefab for Pool " + typeof(HitPoints) + " is null");
            _prefab = prefab;
            _parent = parent;
            _maxPoolSize = maxPoolSize;
            _collectionChecks = collectionChecks;
        }
        public override HitPoints CreatePooledItem()
        {
            HitPoints hitPoints = GameObject.Instantiate(_prefab, _parent);
            var returnToPool = hitPoints.gameObject.AddComponent<ReturnHitPointsToBattlePool>();
            returnToPool.pool = Pool;
            return hitPoints;
        }
    }

}