using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls player and enemy UI in battle
/// </summary>
namespace RoSS
{
    public class CharacterUI : MonoBehaviour
    {
        [SerializeField] TMP_Text _characterName;
        [SerializeField] Image _characterImage;
        [SerializeField] TMP_Text _characterHealthText;
        [SerializeField] Slider _characterHealthSlider;
        [SerializeField] TMP_Text _characterLevel;
        [SerializeField] TMP_Text _characterWins;
        [SerializeField] Color _characterHealthTextColor;

        public void SetName(string name) => _characterName.text = name;

        public void SetImage(Sprite image) => _characterImage.sprite = image;

        public void SetHealth(float health)
        {
            _characterHealthText.text = health.ToString() + "<#" + ColorUtility.ToHtmlStringRGB(_characterHealthTextColor) + "> / " + health.ToString();
            _characterHealthSlider.value = 1;
        }

        public void SetLevel(int level) => _characterLevel.text = level.ToString();

        public void SetWins(int wins) => _characterWins.text = wins.ToString();


    }
}