using UnityEngine;

[System.Serializable]
public class RaceEventCompetitor
{
    public string Name;
    public bool IsPlayer = false;
    public int CurrentStage;
    public int BotDifficultLevel;
    public int TargetStage;
    public int Icon;
    public int Frame;

    public RaceEventCompetitor(string name, int currentBotStage, int targetStage, int botDifficultLevel, bool isPlayer)
    {
        Name = name;
        CurrentStage = currentBotStage;
        BotDifficultLevel = botDifficultLevel;
        TargetStage = targetStage;
        Icon = Random.Range(0, 6);
        Frame = Random.Range(0, 6);
        IsPlayer = isPlayer;
    }
    public void BotRandomWin()
    {
        CurrentStage += GetRandomBoolean(BotDifficultLevel) ? 1 : 0;
    }

    public bool IsCompleted()
    {
        return CurrentStage >= TargetStage;
    }
    private bool GetRandomBoolean(float rate)
    {
        // Ensure the rate is clamped between 0 and 100
        rate = Mathf.Clamp(rate, 0f, 100f);

        // Generate a random float between 0 and 100
        float randomValue = Random.Range(0f, 100f);

        // Return true if the random value is less than the rate
        return randomValue < rate;
    }
    public void Clear()
    {
        Name = "";
        CurrentStage = 0;
        BotDifficultLevel = 0;
        TargetStage = 0;
        Icon = 0;
        Frame = 0;
    }
}
