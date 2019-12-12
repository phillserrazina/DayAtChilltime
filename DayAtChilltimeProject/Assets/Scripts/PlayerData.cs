using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int playerTries;
    public float playerTime;

    public int[] cardSprites;
    public int[] cardStates;

    public void CreateNewData(string playerName) {
        this.playerName = playerName;
        playerTries = 0;
        playerTime = 0f;

        cardSprites = null;
        cardStates = null;
    }
}
