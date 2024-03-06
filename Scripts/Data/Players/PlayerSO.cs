using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable Object class for the player
/// </summary>
[CreateAssetMenu(fileName = "New Player", menuName = "Data/Player")]
public class PlayerSO : ScriptableObject
{

    [SerializeField] List<SpaceshipSO> _spaceships = new List<SpaceshipSO>();
    [SerializeField] string _name;
    [SerializeField] Sprite _image;

    [SerializeField] public int Experience { get; private set; }
    [SerializeField] public int Level { get; private set; }
    [SerializeField] public int Wins { get; private set; }

    public string Name { get => _name; private set => _name = value; }
    public Sprite Image { get => _image; private set => _image = value; }

    public SpaceshipSO DefaultSpaceship;

    public PlayerLevelsSO PlayerLevels;

    private void OnEnable()
    {
        if (DefaultSpaceship == null && _spaceships.Count > 0) DefaultSpaceship = _spaceships[0];
        if (Level < 1) Level = 1;
    }

    public void AddExp(int value)
    {
        Experience += value;
        UpdateLevel();
    }
    public void AddWin(int value)
    {
        Wins += value;
    }

    private void UpdateLevel()
    {
        Level = PlayerLevels.GetLevel(Experience);
    }
}

