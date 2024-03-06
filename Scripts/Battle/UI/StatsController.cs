using System;
using UnityEngine;

/// <summary>
/// Controls gameobject's (e.g. spaceship, turrets) statistics (e.g. health)
/// </summary>
public class StatsController : MonoBehaviour
{

    public static event Action<StatsController> OnHealthAdded = delegate { };
    public static event Action<StatsController> OnHealthRemoved = delegate { };

    public string Name;
    public float MaxHealth { protected set; get; }


    public float CurrentHealth { private set; get; }

    public event Action<float,float> OnHealthChanged = delegate { };
    public event Action OnDie = delegate { };


    protected virtual void Awake()
    {
        MaxHealth = 100;
    }
    protected void OnEnable()
    {
        
        CurrentHealth = MaxHealth;
    }
    public void SetHealth(float value)
    {
        OnHealthAdded(this);
        MaxHealth = CurrentHealth = value;
        ChangeHealth(0);

    }

    public void ChangeHealth(float amount)
    {
        if (BattleManager.Instance.BattleState != BattleState.Battle) return;
        CurrentHealth += amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    protected virtual void Die()
    {
        OnDie?.Invoke();
    }
    protected void OnDisable()
    {
        OnHealthRemoved(this);    
    }
}
