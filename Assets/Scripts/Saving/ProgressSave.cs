using System;
using System.Collections.Generic;

public class ProgressSave
{
    public List<LevelSave> levelSaves = new List<LevelSave>();

    public void AddLevelSave(LevelSave levelSave)
    {
        levelSaves.Add(levelSave);
    }

    public void SetLevelSave(int levelNumber, float time)
    {
        levelSaves[levelNumber - 1].Time = time;
        levelSaves[levelNumber - 1].Completed = true;
    }
}
