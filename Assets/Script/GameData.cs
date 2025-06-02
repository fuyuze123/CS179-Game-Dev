using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public int gold;
    public int playerHealth;
    public int currentLevel;
    public int enemyDefeated;
    public List<TowerSaveData> towers = new List<TowerSaveData>();
    public List<WallSaveData> walls = new List<WallSaveData>();
    public int selectedDifficulty;
}

[Serializable]
public class TowerSaveData
{
    public Vector3 position;
    public int typeID;
    public List<string> appliedPerks;
}

[Serializable]
public class WallSaveData
{
    public Vector2 position;
}
