using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the look and behavior of the experience slider (after the battle)
/// </summary>
public class ExperienceSlider : MonoBehaviour
{
    int _level;
    int _experience;
    int _oldLevel;
    int _oldExperience;

    int _animSpeed = 1;
    float _animInterval = 0.03f;
    float _animTime = 3f;


    int[] _expLevels;

    [SerializeField] TMP_Text _levelText;
    [SerializeField] TMP_Text _expText;
    [SerializeField] Slider _expSlider;


    public void Init(int level, int experience) 
    {
        _level = level;
        _experience = experience;
        _expLevels = GameManager.Instance.Player.PlayerSO.PlayerLevels.GetLevelsExpArray();
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        _levelText.text = _level.ToString();
        _expSlider.value = CalculateSliderValue();
    }

    float CalculateSliderValue()
    {
        return (_experience - _expLevels[_level - 1] ) / (float)(_expLevels[_level] - _expLevels[_level - 1]);
    }


    public void StartSliderAnimation(int level, int experience)
    {
        _oldLevel = _level;
        _oldExperience = _experience;
        _level = level;
        _experience = experience;
        
        _expText.text = "+ "+(_experience- _oldExperience).ToString()+" XP";
        StartCoroutine(SliderAnimation());
    }


    IEnumerator SliderAnimation()
    {
        _animSpeed = Mathf.RoundToInt(_animInterval * (_experience - _oldExperience) / _animTime);
        while (_oldExperience <= _experience)
        {
            if (_oldExperience>= _expLevels[_oldLevel])
            {
                _oldLevel++;
                _levelText.text = _oldLevel.ToString();
                _expSlider.value = 0;
            }
            _expSlider.value = (_oldExperience - _expLevels[_oldLevel - 1]) / (float)(_expLevels[_oldLevel] - _expLevels[_oldLevel - 1]);
            _oldExperience += _animSpeed;
            yield return new WaitForSeconds(_animInterval);
        }
    }

}
