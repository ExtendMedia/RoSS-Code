/// <summary>
/// Controls Player's statistics
/// </summary>
namespace RoSS
{
    public class PlayerStatsController : StatsController
    {
        protected override void Awake()
        {
            MaxHealth = GameManager.Instance.Player.ActiveSpaceship.Health;
        }

    }
}