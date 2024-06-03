using TMPro;
using UnityEngine;

/// <summary>
/// Controls main healthbars for the player and for the enemy
/// </summary>
namespace RoSS
{
    public class MainHealthBar : HealthBar
    {
        protected TMP_Text _text;

        protected override void Awake()
        {
            base.Awake();
            _text = GetComponentInChildren<TMP_Text>();
        }
        protected override void ChangeHealth(float value, float maxValue)
        {
            base.ChangeHealth(value, maxValue);
            _text.text = Mathf.RoundToInt(value).ToString() + " / " + Mathf.RoundToInt(maxValue).ToString();
        }
    }
}