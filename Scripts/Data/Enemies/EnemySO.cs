using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Scriptable Object class for the enemy
/// </summary>
namespace RoSS
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Data/Enemies")]
    public class EnemySO : SerializedScriptableObject
    {
        [SerializeField] SpaceshipSO _spaceshipSO;
        [SerializeField] string _name;
        [SerializeField] Sprite _image;

        public string Name { get => _name; private set => _name = value; }
        public Sprite Image { get => _image; private set => _image = value; }


        public SpaceshipSO GetSpaceshipSO() => _spaceshipSO;

    }
}