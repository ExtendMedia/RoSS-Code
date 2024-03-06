using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The player's levels scriptable object
/// </summary>
[CreateAssetMenu(fileName = "New Player Levels", menuName = "Data/Player Levels")]
public class PlayerLevelsSO : SerializedScriptableObject
{
    public List<PlayerLevel> Levels = new List<PlayerLevel>();


    public int GetLevel(int exp)
    {
        int level = 0;
        foreach (PlayerLevel item in Levels)
        {
            if (exp < item.NextLevelExp)
                return level;
            level++;
        }
        return level;
    }
    public int GetNextLevelExp(int level)
    {
        return Levels[level].NextLevelExp;
    }
    public (int min, int max) GetLevelExpRange(int level)
    {
        return (Levels[level - 1].NextLevelExp, Levels[level].NextLevelExp);
    }

    public int[] GetLevelsExpArray()
    {
        int[] expArray = new int[Levels.Count];

        for (int i = 0; i < expArray.Length; i++)
        {
            expArray[i] = Levels[i].NextLevelExp;
        }

        return expArray;
    }

}
