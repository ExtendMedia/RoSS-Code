using System;
using UnityEngine;

/*
[Obsolete]
public class DEL_Attack : MonoBehaviour
{
    float _nextShootTime;
    float _nextShootDelay = 0.1f;

    BulletPool _bulletPool;
    BigExplosionParticlePool bigExplosionParticlePool;


    [SerializeField] Transform _spawner;
    [SerializeField] GameObject _poolManager;


    
    void Awake()
    {

        _bulletPool = _poolManager.GetComponent<BulletPool>();
        _bulletPool.particlePool = _poolManager.GetComponent<ParticlePool>();
        _bulletPool.PreAllocate();
        _bulletPool.particlePool.PreAllocate();

        bigExplosionParticlePool = _poolManager.GetComponent<BigExplosionParticlePool>();
        bigExplosionParticlePool.PreAllocate();

    }
    private void SetTargetPoint()
    {
        Ray ray = new Ray(gameObject.transform.position, gameObject.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            _spawner.LookAt(raycastHit.point);
        }
    }

    public void Shoot()
    {
        if (CanShoot())
        {
            SetTargetPoint();

            var _bullet = _bulletPool.Pool.Get();
            _bullet.ResetPositionAndRotation(_spawner);
            _bullet.GetComponent<Rigidbody>().velocity = _spawner.forward * _bullet.speed;
            _nextShootTime = Time.time + _nextShootDelay;
        }
    }

    bool CanShoot()
    {
        return Time.time >= _nextShootTime;
    }
}
*/
