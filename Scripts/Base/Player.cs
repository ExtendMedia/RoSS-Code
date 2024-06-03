using UnityEngine;

/// <summary>
/// Main player class
/// </summary>
namespace RoSS
{
    public class Player : MonoBehaviour
    {

        public PlayerSO PlayerSO;

        public SpaceshipSO ActiveSpaceship => PlayerSO.DefaultSpaceship;


    }
}