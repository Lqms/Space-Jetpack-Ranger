using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelManager
{
    public static int EnemiesCount = 0;
    public static float EnemiesMaxCount = 3;
    public static int DefeatedEnemies = 0;
    public static int CurrentLevel = 1;
    public static int EnemiesToLevelUp = 10;

    public static float BonusHealthPerLevel = 10;
    public static float BonusDamagePerLevel = 5;
}
