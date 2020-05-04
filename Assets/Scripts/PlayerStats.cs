using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerStats
{
    private static int currentLevel;
    private static int[] levelsShootCount = new int[18];

    public static int CurrentLevel
    {
       get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
        }
    }
    public static void SetLevelShootCount(int lvl, int shoots)
    {
        levelsShootCount[lvl] = shoots;
    }
    public static int GetLevelShootCount(int lvl)
    {
        return levelsShootCount[lvl];
    }
    public static int[] GetLevelShootCountArray()
    {
        return levelsShootCount;
    }
}
