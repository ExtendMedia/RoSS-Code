using UnityEngine;

/// <summary>
/// Game settings
/// </summary>
namespace RoSS
{
    public static class Settings
    {
        public static LayerMask enemyLayer = LayerMask.NameToLayer("ENEMY");
        public static LayerMask playerLayer = LayerMask.NameToLayer("PLAYER");
        public static LayerMask enemyProjectileLayer = LayerMask.NameToLayer("Enemy Projectiles");
        public static LayerMask playerProjectileLayer = LayerMask.NameToLayer("Player Projectiles");

    }
}