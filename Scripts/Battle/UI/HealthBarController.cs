using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Healthbar's controller
/// </summary>
namespace RoSS
{
    public class HealthBarController : MonoBehaviour
    {
        [SerializeField] TurretHealthBar _healthBarPrefab;
        [SerializeField] MainHealthBar _enemyHealthBar;
        [SerializeField] MainHealthBar _playerHealthBar;
        [SerializeField] Transform _healthBarsContainer;

        Dictionary<StatsController, HealthBar> _healthBars = new Dictionary<StatsController, HealthBar>();


        void Awake()
        {
            StatsController.OnHealthAdded += AddHealthBar;
            StatsController.OnHealthRemoved += RemoveHealthBar;
        }

        void AddHealthBar(StatsController statsController)
        {

            if (!_healthBars.ContainsKey(statsController))
            {
                if (statsController is EnemyStatsController)
                {
                    _healthBars.Add(statsController, _enemyHealthBar);
                    _enemyHealthBar.InitHealthBar(statsController);
                    return;
                }
                if (statsController is PlayerStatsController)
                {
                    _healthBars.Add(statsController, _playerHealthBar);
                    _playerHealthBar.InitHealthBar(statsController);
                    return;
                }

                TurretHealthBar healthBar = Instantiate(_healthBarPrefab, _healthBarsContainer.transform);
                _healthBars.Add(statsController, healthBar);
                healthBar.InitHealthBar(statsController);



            }
        }

        void RemoveHealthBar(StatsController statsController)
        {
            if (_healthBars.ContainsKey(statsController))
            {
                if (_healthBars[statsController] != null) Destroy(_healthBars[statsController].gameObject);
                _healthBars.Remove(statsController);
            }
        }

        private void OnDisable()
        {
            StatsController.OnHealthAdded -= AddHealthBar;
            StatsController.OnHealthRemoved -= RemoveHealthBar;
        }
    }
}