using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LeaderboardScore {
    public string playerName;
    public float playerScore;
    public float playerTime;

    public LeaderboardScore(string playerName, float playerScore, float playerTime) {
        this.playerName = playerName;
        this.playerScore = playerScore;
        this.playerTime = playerTime;
    }
}

[System.Serializable]
public class LeaderboardData
{
    public List<LeaderboardScore> allScores = new List<LeaderboardScore>();
}
