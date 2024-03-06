using UnityEngine;

/// <summary>
/// Main player class
/// </summary>
public class Player : MonoBehaviour
{

    public PlayerSO PlayerSO;

    public SpaceshipSO ActiveSpaceship => PlayerSO.DefaultSpaceship;


}
