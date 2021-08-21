using System;
using System.Collections.Generic;

public class ProgressSave
{
    public List<LevelSave> levelSaves = new List<LevelSave>();

    public void AddLevelSave(LevelSave levelSave)
    {
        levelSaves.Add(levelSave);
    }
}
