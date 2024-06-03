using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object for the player's spaceships
/// </summary>

namespace RoSS
{

    [CreateAssetMenu(fileName = "New Spaceships List", menuName = "Data/Spaceships List")]

    public class SpaceshipsListSO : SerializedScriptableObject
    {
        [SerializeField] List<SpaceshipSO> _spaceshipsList;
        public List<SpaceshipSO> SpaceshipsList { get => _spaceshipsList; private set => _spaceshipsList = value; }
    }
}