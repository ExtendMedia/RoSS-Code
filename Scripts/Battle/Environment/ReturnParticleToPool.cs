using System;
using UnityEngine;
/*
[Obsolete]
[RequireComponent(typeof(ParticleSystem))]
public class ReturnParticleToPool : ReturnToPool<ParticleSystem>
{
    ParticleSystem _system;

    void Start()
    {
        _system = GetComponent<ParticleSystem>();
        var main = _system.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        pool.Release(_system);
    }
}

    */