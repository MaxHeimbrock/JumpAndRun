using System;

[Serializable]
public class LevelSave
{
    public int LevelNumber;
    public bool Completed;
    public float Time;

    public LevelSave(int levelNumber, bool completed, float time)
    {
        LevelNumber = levelNumber;
        Completed = completed;
        Time = time;
    }
}
