/// <summary>
/// Controls enemy's statistics
/// </summary>
namespace RoSS
{
    public class EnemyStatsController : StatsController
    {
        protected override void Awake()
        {
            MaxHealth = BattleManager.Instance.GetEnemySpaceship().SpaceshipSO.Health;
        }

        protected override void Die()
        {
            BattleManager.Instance.GetEnemySpaceship().ActivateFullDestruction();
            base.Die();
        }

    }
}