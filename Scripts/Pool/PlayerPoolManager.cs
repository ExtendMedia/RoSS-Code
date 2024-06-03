using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player's pools manager
/// </summary>
namespace RoSS
{
    public class PlayerPoolManager : PoolManager
    {
        [SerializeField] protected Transform _hitPointsPoolParent;

        [SerializeField] HitPoints _hitPointsPrefab;
        [SerializeField] int _hitPointsPoolSize = 20;

        HitPointsPool _hitPointsPool;
        void Start()
        {
            Dictionary<ProjectileSO, int> projectileDict = new Dictionary<ProjectileSO, int>();
            Dictionary<EffectSO, int> effectDict = new Dictionary<EffectSO, int>();
            GameManager.Instance.Player.ActiveSpaceship.GetWeaponProjectilesAndEffects(out projectileDict, out effectDict);
            InitPools(projectileDict, _projectilesPoolParent);
            InitPools(effectDict, _effectsPoolParent);
            _hitPointsPool = CreatePool(_hitPointsPrefab, _hitPointsPoolSize, _hitPointsPoolParent);

        }

        HitPointsPool CreatePool(HitPoints _hitPoints, int size, Transform _hitPointsPoolParent)
        {
            HitPointsPool hitPointsPool = new HitPointsPool(_hitPoints, _hitPointsPoolParent);
            hitPointsPool.PreAllocate(size);
            return hitPointsPool;
        }

        public HitPoints GetHitPoints()
        {
            return _hitPointsPool.Pool.Get();
        }


    }
}