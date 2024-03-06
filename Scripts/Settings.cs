using UnityEngine;

/// <summary>
/// Game settings
/// </summary>
public static class Settings
{
    public static LayerMask enemyLayer = LayerMask.NameToLayer("ENEMY");
    public static LayerMask enemyProjectileLayer = LayerMask.NameToLayer("Enemy Projectiles");
    public static LayerMask playerProjectileLayer = LayerMask.NameToLayer("Player Projectiles");

}
