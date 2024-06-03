using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the player's specific attack, reloading and spawning projectiles
/// </summary>
namespace RoSS
{
    public class AttackWeapon : MonoBehaviour
    {
        [SerializeField] Image _fillImage;
        float _maxFillAmount = 0.25f;
        float _reloadTime;
        bool _reloading = false;
        WeaponItemSO _weapon;
        public event Action OnWeaponReloaded;
        Transform _spawner;
        [SerializeField] Vector3 _gunsOffset = new Vector3(0.2f, 0, 0);


        private void Start()
        {
            StartReloading();
        }
        public void SetWeapon(WeaponItemSO weapon)
        {
            _weapon = weapon;

        }

        public void SetSpawner(Transform spawner) => _spawner = spawner;

        public void StartReloading()
        {

            _reloadTime = 0;
            UpdateFillImage();
            _reloading = true;
        }

        public bool IsReloading() => _reloading;

        void UpdateFillImage()
        {

            if (_fillImage == null) return;
            _fillImage.fillAmount = _reloadTime / _weapon.ReloadTime * _maxFillAmount;
        }

        void Update()
        {

            if (_reloading)
            {
                _reloadTime += Time.deltaTime;
                UpdateFillImage();
                if (_reloadTime >= _weapon.ReloadTime)
                {
                    _reloading = false;
                    OnWeaponReloaded?.Invoke();
                }
            }
        }

        public void SpawnProjectile(Transform _target)
        {

            Vector3 orgPosition = _spawner.position;
            Quaternion orgRotation = _spawner.rotation;

            for (int i = 0; i < _weapon.Guns; i++)
            {
                _spawner.SetPositionAndRotation(_spawner.position + _gunsOffset * i, _spawner.rotation);
                Projectile projectile = BattleManager.Instance.GetPlayerPool().GetProjectile(_weapon.ProjectileSO);
                projectile.Pool = BattleManager.Instance.GetPlayerPool();
                projectile.SetLayer(Settings.playerProjectileLayer);
                projectile.SetTarget(_spawner, _target, _gunsOffset * i);
                projectile.Arm(_weapon);
                projectile.Shoot();
            }
            _spawner.SetPositionAndRotation(orgPosition, orgRotation); ;

        }
    }
}