[System.Serializable]
public class TheData
{
    private bool LevelSedang;
    private bool LevelSusah;

    private int ScoreMudah;
    private int ScoreSedang;
    private int ScoreSusah;

    public TheData(bool a, bool b)
    {
        LevelSedang = a;
        LevelSusah = b;

        ScoreMudah = 0;
        ScoreSedang = 0;
        ScoreSusah = 0;
    }
    public TheData(TheData a)
    {
        LevelSedang = a.GetSedang();
        LevelSusah = a.GetSusah();

        ScoreMudah = a.GetScoreMudah();
        ScoreSedang = a.GetScoreSedang();
        ScoreSusah = a.GetScoreSusah();
    }

    public bool GetSedang()
    {
        return LevelSedang;
    }
    public void SetSedang(bool val)
    {
        LevelSedang = val;
    }
    public bool GetSusah()
    {
        return LevelSusah;
    }
    public void SetSusah(bool val)
    {
        LevelSusah = val;
    }

    public int GetScoreMudah()
    {
        return ScoreMudah;
    }
    public void SetScoreMudah(int val)
    {
        ScoreMudah = val;
    }
    public int GetScoreSedang()
    {
        return ScoreSedang;
    }
    public void SetScoreSedang(int val)
    {
        ScoreSedang = val;
    }
    public int GetScoreSusah()
    {
        return ScoreSusah;
    }
    public void SetScoreSusah(int val)
    {
        ScoreSusah = val;
    }
}