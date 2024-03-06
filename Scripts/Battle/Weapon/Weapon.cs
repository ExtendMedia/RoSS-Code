using UnityEngine;

/// <summary>
/// Weapon's class
/// </summary>
public class Weapon : MonoBehaviour
{
    WeaponItemSO _weaponItemSO;
    ProjectileSpawner[] _projectileSpawners;

    float _reloadTime;
    bool _reloading = false;

    void Awake()
    {
        _projectileSpawners = GetComponentsInChildren<ProjectileSpawner>();

    }

    void Start()
    {
        StartReloading();

    }

    public void StartReloading()
    {
        _reloadTime = 0;
        _reloading = true;
    }

    public void SetWeaponItemSO(WeaponItemSO weapon) => _weaponItemSO = weapon;

    public void Destroy()
    {
        var effect = BattleManager.Instance.GetEnemyPool().GetEffect(_weaponItemSO.DestructionEffectSO);
        effect.Init(transform.position, transform.rotation);
        gameObject.SetActive(false);

    }

    public bool CanShoot() => _reloading == false;

    public void Shoot()
    {
        foreach (var spawner in _projectileSpawners)
        {
            SpawnProjectile(spawner.transform, BattleManager.Instance.GetTarget());
            StartReloading();
        }
    }

    public void SpawnProjectile(Transform spawner, Transform target)
    {
        Projectile projectile = BattleManager.Instance.GetEnemyPool().GetProjectile(_weaponItemSO.ProjectileSO);
        projectile.Pool = BattleManager.Instance.GetEnemyPool();
        projectile.SetLayer(Settings.enemyProjectileLayer);
        projectile.SetPlayerTarget(spawner, target);
        //projectile.Arm(_weaponItemSO.Speed, _weaponItemSO.Damage, _weaponItemSO.LifeTime, _weaponItemSO.Accuracy, _weaponItemSO.EffectSO);
        projectile.Arm(_weaponItemSO);
        projectile.Shoot();
    }
    void Update()
    {
        if (_reloading)
        {
            _reloadTime += Time.deltaTime;
            if (_reloadTime >= _weaponItemSO.ReloadTime)
            {
                _reloading = false;
            }
        }
    }

}
