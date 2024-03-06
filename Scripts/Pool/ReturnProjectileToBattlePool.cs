using UnityEngine;


/// <summary>
/// A class that returns projectiles to the pool
/// </summary>
[RequireComponent(typeof(Projectile))]
public class ReturnProjectileToBattlePool : ReturnToPool<Projectile>
{
    Projectile _projectile;

    void Awake()
    {
        _projectile = GetComponent<Projectile>();
        _projectile.OnShoot += ShootHandler;
    }



    void ShootHandler()
    {
        Invoke("SelfRelease", _projectile.GetLifeTime());

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == Settings.enemyLayer && gameObject.layer == Settings.playerProjectileLayer)
        {
            _projectile.Hit(collision.collider.gameObject);
            if (gameObject.activeSelf) pool.Release(_projectile);
        }
    }


    
    void SelfRelease()
    {

        if (gameObject.layer == Settings.enemyProjectileLayer && _projectile.IsPlayerHit()) _projectile.PlayerHit();

        if (gameObject.activeSelf) pool.Release(_projectile);

    }
}
