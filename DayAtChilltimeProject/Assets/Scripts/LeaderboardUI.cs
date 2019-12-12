using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab = null;

    private void Start() {
        LeaderboardData lbData = SaveManager.GetLeaderboardData();
        for (int i = 0; i < lbData.allScores.Count; i++) {
            GameObject go = Instantiate(linePrefab, transform) as GameObject;
            var line = go.GetComponent<LinePrefabUI>();

            line.placementText.text = "0" + (i+1).ToString();
            line.nameText.text = lbData.allScores[i].playerName;
            line.scoreText.text = lbData.allScores[i].playerScore.ToString("F0");
            line.timeText.text = FloatToMinutes(lbData.allScores[i].playerTime);
        }
    }

    private string FloatToMinutes(float time) {
        float minutes = Mathf.Floor(time / 60);
        float seconds = Mathf.RoundToInt(time % 60);
        
        string minutesText = minutes.ToString("F0");
        string secondsText = seconds.ToString("F0");

        if (seconds == 60) {
            minutes += 1;
            minutesText = minutes.ToString("F0");
            secondsText = "00";
        }

        if(seconds < 10) {
            secondsText = "0" + Mathf.RoundToInt(seconds).ToString();
        }

        return string.Format("{0}:{1}", minutesText, secondsText);
    }
}
