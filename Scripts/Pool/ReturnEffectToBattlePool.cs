using UnityEngine;

/// <summary>
/// A class that returns effects to the pool
/// </summary>
[RequireComponent(typeof(Effect))]
public class ReturnEffectToBattlePool : ReturnToPool<Effect>
{
    Effect _effect;

    void Start()
    {
        _effect = GetComponent<Effect>();

        ParticleSystem _system = GetComponent<ParticleSystem>();
        var main = _system.main;
        main.stopAction = ParticleSystemStopAction.Callback;

    }

    void OnParticleSystemStopped()
    {
        pool.Release(_effect);
    }

}
