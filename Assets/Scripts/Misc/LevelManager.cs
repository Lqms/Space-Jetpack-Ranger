using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelManager
{
    public static int EnemiesCount = 0;
    public static float EnemiesMaxCount = 3;
    public static int DefeatedEnemies = 0;
    public static int CurrentWave = 1;
    public static float EnemiesToLevelUp = 10;

    public static float BonusHealthPerLevel = 10;
    public static float BossBonusHealthPerLevel = 100;
    public static float BonusDamagePerLevel = 5;
    public static float BossBonusDamagePerLevel = 25;
    public static int BonusBountyPerLevel = 5;

    public static int WaveMultiplicityToSave = 10;
    public static int WaveMultiplicityForBoss = 15;

}
