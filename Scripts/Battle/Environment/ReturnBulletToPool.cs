using System;
using UnityEngine;
using UnityEngine.Pool;

/*
[Obsolete]
[RequireComponent(typeof(Bullet))]
public class ReturnBulletToPool : ReturnToPool<Bullet>
{
    Bullet _bullet;
    public IObjectPool<ParticleSystem> particlePool;

    void Start()
    {
        _bullet = GetComponent<Bullet>();

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == Settings.enemyLayer && particlePool != null)
        {
            ParticleSystem ps = particlePool.Get();
            ps.transform.position = _bullet.transform.position;
            ps.Play();

            _bullet.Hit(collision.collider.gameObject);
        }
        pool.Release(_bullet);

    }

}
*/