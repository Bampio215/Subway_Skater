
using System;
using UnityEngine;
[System.Serializable]
public class SaveState
{
    public int Highscore { set; get; }
    public int Fish { set; get; }
    public DateTime LastSaveTime { set; get; }
    public SaveState()
    {
        Highscore = 0;
        Fish = 0;
        LastSaveTime = DateTime.Now;
    }
}
