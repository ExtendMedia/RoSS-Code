using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Enemy's pools manager
/// </summary>
public class EnemyPoolManager : PoolManager
{
    void Start()
    {
        Dictionary<ProjectileSO, int> projectileDict = new Dictionary<ProjectileSO, int>();
        Dictionary<EffectSO, int> effectDict = new Dictionary<EffectSO, int>();
        BattleManager.Instance.GetEnemySpaceship().SpaceshipSO.GetWeaponProjectilesAndEffects(out projectileDict, out effectDict, false, true, true);
        InitPools(projectileDict, _projectilesPoolParent);
        InitPools(effectDict, _effectsPoolParent);
    }


}
