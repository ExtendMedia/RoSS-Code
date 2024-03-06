using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
/*    Dictionary<GameObject, ParticlePool> _onDieEffects = new Dictionary<GameObject, ParticlePool>();

    [SerializeField] GameObject _poolManager;

    void Awake()
    {
        FindTurrets();
    }


    void FindTurrets()
    {
        var turrets = FindObjectsOfType<Turret>();
        foreach (var turret in turrets)
        {
            if (turret.TryGetComponent<StatsController>(out var statsController))
            {
                statsController.OnDie += TurretDieEffect;
                _onDieEffects.Add(turret.gameObject, _poolManager.GetComponent<BigExplosionParticlePool>());
            }
        }
    }

    void TurretDieEffect(StatsController statsController)
    {
        var turretGO = statsController.gameObject;
        if (_onDieEffects.ContainsKey(turretGO))
        {
            if (_onDieEffects[turretGO] != null)
            {
                ParticleSystem ps = _onDieEffects[turretGO].Pool.Get();
                ps.transform.position = turretGO.transform.position;
                ps.Play();
            }
                


        }

    }*/
}
