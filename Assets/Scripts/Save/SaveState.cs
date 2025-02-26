
using System;
[System.Serializable]
public class SaveState
{
    [NonSerialized] private const int HAT_COUNT = 31;
    public int Highscore { set; get; }
    public int Fish { set; get; }
    public DateTime LastSaveTime { set; get; }
    public int currentHatindex { set; get; }
    public byte[] UnlockedhatFlag { set; get; }
    public SaveState()
    {
        Highscore = 0;
        Fish = 10;
        LastSaveTime = DateTime.Now;
        currentHatindex = 0;
        UnlockedhatFlag = new byte[HAT_COUNT];
        UnlockedhatFlag[0] = 1;
    }
}
