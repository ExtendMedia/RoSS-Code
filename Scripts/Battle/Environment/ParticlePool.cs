using System;
using UnityEngine;

/*
[Obsolete]
public class ParticlePool : ComponentPool<ParticleSystem>
{

    public override ParticleSystem CreatePooledItem()
    {
        var go = Instantiate(_prefab, _parent);
        var ps = go.GetComponent<ParticleSystem>();
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        var main = ps.main;
        main.duration = 1;
        main.startLifetime = 1;
        main.loop = false;

        var returnToPool = go.gameObject.AddComponent<ReturnParticleToPool>();
        returnToPool.pool = Pool;

        return ps;
    }

}

*/