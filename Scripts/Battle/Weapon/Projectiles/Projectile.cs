using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Projectile class
/// </summary>
namespace RoSS
{
    public class Projectile : MonoBehaviour
    {
        float _rotateSpeed = 100.0f;

        ProjectileStats _stats;
        EffectSO _effect;
        Vector3 _targetPoint;
        Vector3 _target;
        AudioSource _audioSource;
        AudioClip _audioClip;
        bool _isLaunched;
        bool _homing;

        Rigidbody _rb;

        public PoolManager Pool;
        public event Action OnShoot = delegate { };

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {

            if (_homing && _isLaunched)
            {
                _rb.velocity = transform.forward * _stats.speed;
                _target = BattleManager.Instance.GetTarget().position;
                RotateProjectile();
            }
        }

        private void RotateProjectile()
        {
            var heading = _target - transform.position;
            var rotation = Quaternion.LookRotation(heading);
            _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
        }

        public void Arm(WeaponItemSO weapon)
        {
            _stats.speed = weapon.Speed;
            _stats.damage = weapon.Damage;
            _stats.lifeTime = weapon.LifeTime;
            _stats.accuracy = weapon.Accuracy;

            _homing = weapon.Homing;
            _effect = weapon.EffectSO;

        }

        public void SetTarget(Transform spawner, Transform target)
        {

            Ray ray = new Ray(target.position, target.forward);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                _targetPoint = raycastHit.point;
                spawner.LookAt(_targetPoint);
                transform.SetPositionAndRotation(spawner.position, spawner.rotation);

            }
        }
        public void SetTarget(Transform spawner, Transform target, Vector3 offset)
        {

            Ray ray = new Ray(target.position, target.forward);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                _targetPoint = raycastHit.point + offset;
                spawner.LookAt(_targetPoint);
                transform.SetPositionAndRotation(spawner.position, spawner.rotation);

            }
        }

        public void SetPlayerTarget(Transform spawner, Transform target)
        {
            transform.SetPositionAndRotation(spawner.position, spawner.rotation);
            _target = target.position;
        }

        public void SetLayer(int layer)
        {
            gameObject.layer = layer;
            foreach (var collider in GetComponentsInChildren<Collider>())
            {
                collider.gameObject.layer = layer;
            }
        }

        public void Shoot()
        {
            _rb.velocity = transform.forward * _stats.speed;
            OnShoot?.Invoke();
            PlaySound();
            _isLaunched = true;

        }

        public void Hit(GameObject target)
        {
            if (!_isLaunched) return;
            foreach (var targetStatsController in FindStatsControllers(target))
            {
                targetStatsController.ChangeHealth(-_stats.damage);
                _isLaunched = false;
                SpawnEffect(_effect);
                if (Pool is PlayerPoolManager)
                    SpawnHitPoints(_stats.damage);
            }

        }

        public List<StatsController> FindStatsControllers(GameObject target)
        {
            var statsControllers = new List<StatsController>();

            while (target != null)
            {
                var targetStatsController = target.GetComponent<StatsController>();

                if (targetStatsController != null)
                    statsControllers.Add(targetStatsController);
                if (target.transform.parent == null) return statsControllers;
                target = target.transform.parent.gameObject;
            }
            return statsControllers;
        }


        void SpawnEffect(EffectSO type)
        {
            var effect = Pool.GetEffect(type);
            effect.Init(transform.position, transform.rotation);
        }

        void SpawnHitPoints(float value)
        {
            PlayerPoolManager enemyPoolManager = Pool as PlayerPoolManager;
            var hitPoints = enemyPoolManager.GetHitPoints();
            hitPoints.Spawn(transform.position, value);
        }

        public float GetLifeTime() => _stats.lifeTime;


        public bool IsPlayerHit() => Random.Range(0f, 1f) <= _stats.accuracy;
        public void PlayerHit()
        {
            _isLaunched = false;
            SpawnEffect(_effect);
            BattleManager.Instance.GetTarget().GetComponent<StatsController>().ChangeHealth(-_stats.damage);
        }


        public void SetAudioSource(AudioSource audioSource) => _audioSource = audioSource;

        public void SetAudioClip(AudioClip audioClip) => _audioClip = audioClip;

        void PlaySound()
        {
            if (_audioClip) _audioSource.PlayOneShot(_audioClip);
        }


    }


    public struct ProjectileStats
    {
        public float speed;
        public float damage;
        public float lifeTime;
        public float accuracy;

    }
}