using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Controls timer during battle
/// </summary>
namespace RoSS
{
    public class Timer : MonoBehaviour
    {
        float _time;
        bool _counting = false;
        [SerializeField] TMP_Text _timeText;

        public static event Action OnTimeIsUp;
        public void SetTimer(float timeValue)
        {
            _time = timeValue;
        }

        public void StartTimer()
        {
            _counting = true;
            gameObject.GetComponent<SmoothMove>().StartMove();
        }

        public void StopTimer() => _counting = false;

        void Update()
        {
            if (!_counting) return;

            _time -= Time.deltaTime;

            if (_time <= 0)
            {
                _time = 0;
                _counting = false;
                OnTimeIsUp?.Invoke();
            }
            UpdateTimeText();
        }

        void UpdateTimeText()
        {
            TimeSpan time = TimeSpan.FromSeconds(_time);
            _timeText.text = time.ToString("mm':'ss'.<size=42.21>'f");

        }

    }
}