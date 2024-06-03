using UnityEngine;


/// <summary>
/// A class that returns projectiles to the pool
/// </summary>
namespace RoSS
{
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


        void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.layer == Settings.enemyLayer && gameObject.layer == Settings.playerProjectileLayer)
            {
                _projectile.Hit(collider.gameObject);
                if (gameObject.activeSelf) pool.Release(_projectile);

            }
            else if (collider.gameObject.layer == Settings.playerLayer && gameObject.layer == Settings.enemyProjectileLayer)
            {
                if (_projectile.IsPlayerHit()) _projectile.PlayerHit();
                if (gameObject.activeSelf) pool.Release(_projectile);

            }
        }



        void SelfRelease()
        {
            if (gameObject.activeSelf) pool.Release(_projectile);
        }
    }
}