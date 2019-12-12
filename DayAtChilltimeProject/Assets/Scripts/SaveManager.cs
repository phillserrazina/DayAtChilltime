using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static PlayerData currentData;
    public static SaveManager instance;

    private void Awake() {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #region Save and Load Data Functions
    public static void SavePlayerData(PlayerData playerData) {
        string path = Application.persistentDataPath + "/Save_" + playerData.playerName + ".json";

        if (System.IO.File.Exists(path) == false)
            System.IO.File.Create(path).Close();
        
        string contents = JsonUtility.ToJson(playerData);
        System.IO.File.WriteAllText(path, contents);
    }

    public static PlayerData GetPlayerData(string playerName) {
        string path = Application.persistentDataPath + "/Save_" + playerName + ".json";
        PlayerData playerData;

        if (System.IO.File.Exists(path) == false) {
            playerData = new PlayerData();
            playerData.CreateNewData(playerName);
            SavePlayerData(playerData);
            return playerData;
        }

        string contents = System.IO.File.ReadAllText(path);
        return JsonUtility.FromJson<PlayerData>(contents);
    }

    public static void DeletePlayerData(string playerName) {
        string path = Application.persistentDataPath + "/Save_" + playerName + ".json";
        if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
    }

    public static void SaveLeaderboardData(LeaderboardData lbData) {
        string path = Application.persistentDataPath + "/Leaderboard.json";

        if (System.IO.File.Exists(path) == false)
            System.IO.File.Create(path).Close();
        
        string contents = JsonUtility.ToJson(lbData);
        System.IO.File.WriteAllText(path, contents);
    }

    public static LeaderboardData GetLeaderboardData() {
        string path = Application.persistentDataPath + "/Leaderboard.json";
        LeaderboardData leaderboardData;

        if (System.IO.File.Exists(path) == false) {
            leaderboardData = new LeaderboardData();
            SaveLeaderboardData(leaderboardData);
            return leaderboardData;
        }

        string contents = System.IO.File.ReadAllText(path);
        return JsonUtility.FromJson<LeaderboardData>(contents);
    }
    #endregion
}
