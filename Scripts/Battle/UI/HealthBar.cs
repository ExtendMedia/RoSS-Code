using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// An abstract class for healthbars used in battle
/// </summary>
public abstract class HealthBar : MonoBehaviour
{

    protected StatsController _statsController;
    protected Slider _healthSlider; 


    protected virtual void Awake()
    {
        _healthSlider = GetComponent<Slider>();
    }

    public virtual void InitHealthBar(StatsController statsController)
    {
        _statsController = statsController;
        _statsController.OnHealthChanged += ChangeHealth;
    }

    protected virtual void ChangeHealth(float value, float maxValue)
    {
        _healthSlider.value = value/maxValue;
    }



}
