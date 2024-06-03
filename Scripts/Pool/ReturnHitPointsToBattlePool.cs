using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Returns hitpoints gameobjects to the pool
/// </summary>
namespace RoSS
{
    [RequireComponent(typeof(HitPoints))]
    public class ReturnHitPointsToBattlePool : ReturnToPool<HitPoints>
    {
        HitPoints _hitPoints;

        void Awake()
        {
            _hitPoints = GetComponent<HitPoints>();
            _hitPoints.OnSpawn += SpawnHandler;
        }


        void SpawnHandler()
        {
            Invoke("SelfRelease", _hitPoints.LifeTime);

        }


        void SelfRelease()
        {
            if (gameObject.activeSelf) pool.Release(_hitPoints);
        }
    }
}