using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game pools manager
/// </summary>
namespace RoSS
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] protected Transform _projectilesPoolParent;
        [SerializeField] protected Transform _effectsPoolParent;


        Dictionary<ProjectileSO, ProjectilePool> _projectilesPools = new Dictionary<ProjectileSO, ProjectilePool>();
        Dictionary<EffectSO, EffectPool> _effectsPools = new Dictionary<EffectSO, EffectPool>();

        protected void InitPools(Dictionary<ProjectileSO, int> projectileDict, Transform parent)
        {
            foreach (var projectile in projectileDict)
            {
                CreatePool(projectile.Key, projectile.Value, parent);
            }
        }

        protected void InitPools(Dictionary<EffectSO, int> effectDict, Transform parent)
        {
            foreach (var effect in effectDict)
            {
                CreatePool(effect.Key, effect.Value, parent);
            }
        }

        void CreatePool(ProjectileSO _projectile, int size, Transform _projectilesPoolParent)
        {
            if (_projectilesPools.ContainsKey(_projectile)) return;
            ProjectilePool projectilePool = new ProjectilePool(_projectile.Prefab, _projectilesPoolParent, _projectile.AudioClips);
            projectilePool.PreAllocate(size);
            _projectilesPools.Add(_projectile, projectilePool);
        }
        void CreatePool(EffectSO _effect, int size, Transform _effectsPoolParent)
        {
            if (_effectsPools.ContainsKey(_effect)) return;
            EffectPool effectPool = new EffectPool(_effect.Prefab, _effectsPoolParent, _effect.AudioClips);
            effectPool.PreAllocate(size);
            _effectsPools.Add(_effect, effectPool);
        }

        public Projectile GetProjectile(ProjectileSO type)
        {
            if (_projectilesPools.TryGetValue(type, out var projectilePool))
            {
                return projectilePool.Pool.Get();
            }
            Debug.LogError("Pool of " + type.name + " objects not found");
            return null;
        }

        public Effect GetEffect(EffectSO type)
        {
            if (_effectsPools.TryGetValue(type, out var effectPool))
            {
                return effectPool.Pool.Get();
            }
            Debug.LogError("Pool of " + typeof(EffectSO) + " objects not found");
            return null;
        }


    }
}